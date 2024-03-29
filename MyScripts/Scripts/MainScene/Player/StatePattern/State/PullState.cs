using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using PullAnimals.StatePattern;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// 動物を引っこ抜いているState
    /// </summary>
    public class PullState : IState,IDisposable
    {
        /// <summary>
        /// UniTaskキャンセル用のトークンソース
        /// </summary>
        private readonly CancellationTokenSource _tokenSource;
        /// <summary>
        /// Stateを変更する用のインターフェース
        /// </summary>
        private readonly ITransitionState _transitionState;
        /// <summary>
        /// Stateが操作するコンポーネントをまとめたクラス
        /// </summary>
        private readonly PlayerComponentsBundle _componentsBundle;
        /// <summary>
        /// Stateを集めたクラス
        /// </summary>
        private PlayerStateContainer _playerStateContainer;
        /// <summary>
        /// 引っ張る回数
        /// </summary>
        private int _pullCount;
        
        /// <summary>
        /// 動物を引っこ抜く処理
        /// </summary>
        private async UniTaskVoid AsyncPullAnimal(CancellationToken token)
        {
            //動物側に引っこ抜かれ始めたことを通知する
            _componentsBundle.HandController.StartPulling(_componentsBundle.FeverController.IsFever);
            
            //動物のステータスを取得
            var status = _componentsBundle.HandController.CarryingRabbit.Status;

            //カメラを移動
            _componentsBundle.PlayerCameraController.ChangeCamera();

            //フィーバー中か判断する
            if (!_componentsBundle.FeverController.IsFever)
            {
                //完全体のウサギかどうかを判断する
                if (_componentsBundle.HandController.CarryingRabbit.GetRabbitState() is BloomState)
                {
                    //Joycon接続中かどうかで区別する
                    if (JoyconInputController.Instance.IsConnectingJoycon)
                    {
                        //Joycon接続時
                        //振っているスピード
                        var speed = MyInputController.Instance.GetJoyconSwingSpeed();
                        while (_pullCount < status.ShakeCount)
                        {
                            //振っているスピードが一定基準を超えるまで待機
                            while (speed < _componentsBundle.HandController.SwingData.UpperLimit)
                            {
                                await UniTask.Yield();
                                speed = MyInputController.Instance.GetJoyconSwingSpeed();
                            };
                            
                            if (_componentsBundle.HandController.CanPullRabbit)
                            {
                                //カウントを増加
                                _pullCount++;
                                //ウサギを引き延ばす
                                _componentsBundle.HandController.PullRabbit(_pullCount, status.ShakeCount);
                            }
                
                            //振るスピードが一定以下になるまで待つ
                            //連続で振ったことにならないようにするため
                            while (speed > _componentsBundle.HandController.SwingData.LowerLimit)
                            {
                                await UniTask.Yield();
                                speed = MyInputController.Instance.GetJoyconSwingSpeed();
                            }
                        }
                    }
                    else
                    {
                        //Joycon非接続時
                        //重さによって連打をおこなう
                        while (_pullCount < status.MashCount)
                        {
                            if (MyInputController.Instance.GetHandActionKeyDown())
                            {
                                SePlayer.Instance.Play("SE_Pulling");
                                
                                if (_componentsBundle.HandController.CanPullRabbit)
                                {
                                    //カウントを増加
                                    _pullCount++;
                                    //ウサギを引き延ばす
                                    _componentsBundle.HandController.PullRabbit(_pullCount, status.ShakeCount);
                                }
                            }
                
                            await UniTask.Yield();
                        }     
                    }
                
                    //フィーバーのカウントを加算
                    var isFever =　_componentsBundle.FeverController.AddFeverCount();
                    if(isFever) _componentsBundle.ParticleController.PlayFeverParticle(_componentsBundle.FeverController.FeverTime);
                }
                else
                {
                    //完全体じゃないならカウントをリセット
                    _componentsBundle.FeverController.ResetFeverCount();
                }
            }
            
            //スコアを加算する
            _componentsBundle.PlayerScoreController.AddScore(_componentsBundle.HandController.CarryingRabbit.GetScore());
            //アニメーションを変更
            _componentsBundle.AnimatorController.MyAnimator.SetTrigger(_componentsBundle.AnimatorController.EndPullTriggerHash);
            //引っこ抜く瞬間のアニメーションまで待機
            await UniTask.WaitUntil(() => _componentsBundle.AnimationEventConnector.IsPullRabbitAnimation, cancellationToken: token);
            //コントローラを揺らす（完全体のみ）
            if (_componentsBundle.HandController.CarryingRabbit.GetRabbitState() is BloomState)
            {
                MyInputController.Instance.AsyncShakeController(status.GamepadShakeStrength, status.GamepadShakeTime, 
                    status.JoyconShakeStrength, status.JoyconShakeTime, 
                    token).Forget();   
            }
            //パーティクルを再生
            _componentsBundle.ParticleController.PlayDustParticle(_componentsBundle.HandController.CarryingRabbit.transform.position);
            //上に投げ飛ばす
            _componentsBundle.HandController.ThrowRabbit();
            //音を鳴らす
            SePlayer.Instance.Play("SE_EndPull");
            //カメラを元に戻す
            _componentsBundle.PlayerCameraController.ChangeCamera();
            //引っこ抜くアニメーションが終わるまで待機
            await UniTask.WaitUntil(() => _componentsBundle.AnimationEventConnector.IsPullingAnimation == false, cancellationToken: token);
            //IdleStateに戻す
            //新しくCarryStateにするとCarryFallやCarryJumpを作る必要があるのでIdleにする
            _transitionState.TransitionState(_playerStateContainer.IdleState);
        }
        
        void IState.Initialize(StateContainerBase stateContainer)
        {
            _playerStateContainer = stateContainer as PlayerStateContainer;
        }

        void IState.Enter()
        {
            _pullCount = 0;
            SePlayer.Instance.Play("SE_StartPull");
            _componentsBundle.AnimatorController.MyAnimator.SetTrigger(_componentsBundle.AnimatorController.StartPullTriggerHash);
            _componentsBundle.RabbitUIController.HideArrowImage();
            AsyncPullAnimal(_tokenSource.Token).Forget();
        }

        void IState.MyUpdate()
        {
            _componentsBundle.FeverController.FeverUpDate();
        }

        void IState.MyFixedUpdate()
        {
            
        }

        void IState.Exit()
        {
            
        }
        
        /// <summary>
        /// コンストラクター
        /// </summary>
        public PullState(ITransitionState ts, PlayerComponentsBundle bundle)
        {
            _transitionState = ts;
            _componentsBundle = bundle;
            _tokenSource = new CancellationTokenSource();
        }
        
        public void Dispose()
        {
            _tokenSource?.Dispose();
        }
    }
}
