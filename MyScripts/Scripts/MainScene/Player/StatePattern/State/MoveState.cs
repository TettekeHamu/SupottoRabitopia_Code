using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using PullAnimals.StatePattern;

namespace PullAnimals
{
    /// <summary>
    /// 移動中のState
    /// </summary>
    public class MoveState : IState, IDisposable
    {
        /// <summary>
        /// UniTaskキャンセル用のトークンソース
        /// </summary>
        private CancellationTokenSource _tokenSource;
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
        /// 移動中のパーティクルを生成する処理
        /// </summary>
        private async UniTaskVoid AsyncPlayParticle(CancellationToken token)
        {
            while (true)
            {
                _componentsBundle.ParticleController.PlayDashParticle();
                await UniTask.Delay(TimeSpan.FromSeconds(0.05f), cancellationToken: token);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        /// <summary>
        /// 移動音を鳴らす処理
        /// </summary>
        /// <param name="token"></param>
        private async UniTaskVoid AsyncMoveSound(CancellationToken token)
        {
            while (true)
            {
                SePlayer.Instance.Play("SE_Move");
                await UniTask.Delay(TimeSpan.FromSeconds(SePlayer.Instance.GetSeLength("SE_Move")), cancellationToken: token);
            }
            // ReSharper disable once FunctionNeverReturns
        }
        
        void IState.Initialize(StateContainerBase stateContainer)
        {
            _playerStateContainer = stateContainer as PlayerStateContainer;
        }

        void IState.Enter()
        {
            //トークンソースの生成
            //移動中止時に破棄するのでEnter時に生成
            _tokenSource = new CancellationTokenSource();
            //ジャンプからの遷移時挙動がおかしくなるのでEnter()でも移動をおこなう
            _componentsBundle.MoveController.MovePlayer(_componentsBundle.AnimatorController);
            //パーティクルを再生
            AsyncPlayParticle(_tokenSource.Token).Forget();
            //音を再生
            AsyncMoveSound(_tokenSource.Token).Forget();
        }

        void IState.MyUpdate()
        {
            //入力があれば引っこ抜く or 投げ飛ばす
            if (MyInputController.Instance.GetHandActionKeyDown())
            {
                var canPull = _componentsBundle.HandController.PullAnimal();
                if (canPull)
                {
                    _transitionState.TransitionState(_playerStateContainer.PullState);   
                    return;
                }
            }
            

            /*
            //接地していなければ下に落とす
            if (!_componentsBundle.MoveController.OnGround)
            {
                _transitionState.TransitionState(_playerStateContainer.FallState);
                return;
            }
            //入力があればジャンプさせる
            if (MyInputController.Instance.JumpKeyDown)
            {
                _transitionState.TransitionState(_playerStateContainer.JumpState);
                return;
            }
            */
            
            //入力がなければIdleStateに変更
            if (MyInputController.Instance.GetMoveInputVector2().magnitude <= 0.01f)
            {
                _transitionState.TransitionState(_playerStateContainer.IdleState);
                return;
            }
            
            _componentsBundle.RabbitUIController.ShowTargetRabbit();

            //移動させる
            _componentsBundle.MoveController.MovePlayer(_componentsBundle.AnimatorController);
            
            _componentsBundle.FeverController.FeverUpDate();
        }

        void IState.MyFixedUpdate()
        {
            
        }

        void IState.Exit()
        {
            //エフェクトの生成を中止させる
            _tokenSource.Cancel();
        }
        
        /// <summary>
        /// コンストラクター
        /// </summary>
        public MoveState(ITransitionState ts, PlayerComponentsBundle bundle)
        {
            _transitionState = ts;
            _componentsBundle = bundle;
        }
        
        public void Dispose()
        {
            _tokenSource?.Dispose();
        }
    }
}