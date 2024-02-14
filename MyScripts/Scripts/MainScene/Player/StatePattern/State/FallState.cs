using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using PullAnimals.StatePattern;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// 落下中のState
    /// </summary>
    public class FallState : IState,IDisposable
    {
        /// <summary>
        /// UniTaskキャンセル用のトークンソース
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource;
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
        /// 落下スピード
        /// </summary>
        private float _fallSpeed;
        /// <summary>
        /// 着地してるかどうか
        /// </summary>
        private bool _isLanding;
        
        /// <summary>
        /// 着地した時の処理
        /// </summary>
        private async UniTaskVoid AsyncLandGround(CancellationToken token)
        {
            //着地フラグの変更
            _isLanding = true;
            //着地のアニメーション分少し待つ
            await UniTask.WaitForSeconds(0.1f, cancellationToken: token);
            _transitionState.TransitionState(_playerStateContainer.IdleState);
        }
        
        void IState.Initialize(StateContainerBase stateContainer)
        {
            _playerStateContainer = stateContainer as PlayerStateContainer;
        }

        void IState.Enter()
        {
            //トークンソースの作成
            _cancellationTokenSource = new CancellationTokenSource();
            //着地してないことにする
            _isLanding = false;
            //落下速度を最初から持たせてあげる
            _fallSpeed = 0f;
            //アニメーターに通知
            //_animatorController.MyAnimator.SetBool(_animatorController.IsGroundedHash,false);
            //落下させる
            _componentsBundle.MoveController.JumpMovePlayer(_componentsBundle.AnimatorController, _fallSpeed);
        }

        void IState.MyUpdate()
        {
            //着地していたら落下させない
            if(_isLanding) return;

            //接地してるかどうか判定する
            if (_componentsBundle.MoveController.OnGround)
            {
                //アニメーターに通知
                //_animatorController.MyAnimator.SetBool(_animatorController.IsGroundedHash,true);
                AsyncLandGround(_cancellationTokenSource.Token).Forget();
                return;
            }

            //落下速度を更新
            _fallSpeed += Physics.gravity.y * Time.deltaTime * 3;
            //落下させる
            _componentsBundle.MoveController.JumpMovePlayer(_componentsBundle.AnimatorController, _fallSpeed);
            
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
        public FallState(ITransitionState ts, PlayerComponentsBundle bundle)
        {
            _transitionState = ts;
            _componentsBundle = bundle;
        }
        
        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
        }
    }
}