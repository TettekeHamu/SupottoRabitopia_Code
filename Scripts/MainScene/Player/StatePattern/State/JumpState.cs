using PullAnimals.StatePattern;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// ジャンプ時のState
    /// </summary>
    public class JumpState : IState
    {
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
        /// 現在の回転スピード
        /// 入力があるときのみ回転させるのでここで宣言
        /// </summary>
        private float _rotationCurrentVelocity;
        /// <summary>
        /// 現在の落下上昇速度
        /// </summary>
        private float _currentFallVelocity;
        
        void IState.Initialize(StateContainerBase stateContainer)
        {
            _playerStateContainer = stateContainer as PlayerStateContainer;
        }

        void IState.Enter()
        {
            //上方向に速度を入れる
            _currentFallVelocity = _componentsBundle.MoveController.StartJumpPower;
            //移動
            _componentsBundle.MoveController.JumpMovePlayer(_componentsBundle.AnimatorController, _currentFallVelocity);
            //アニメーターに通知
            //_animatorController.MyAnimator.SetBool(_animatorController.IsGroundedHash, false);
        }

        void IState.MyUpdate()
        {
            /*
            //入力を取得
            var isJumpKeyUp = MyInputController.Instance.JumpKeyUp;

            //FallStateに移行するかチェックする
            //ボタンを離す
            //ジャンプの上昇速度がマイナスになる
            if (isJumpKeyUp || _currentFallVelocity <= 0)
            {
                _transitionState.TransitionState(_playerStateContainer.FallState);
                return;
            }

            //ジャンプ速度を更新
            _currentFallVelocity += Physics.gravity.y * Time.deltaTime;
            //移動
            _componentsBundle.MoveController.JumpMovePlayer(_componentsBundle.AnimatorController, _currentFallVelocity);
            */
            
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
        public JumpState(ITransitionState ts, PlayerComponentsBundle bundle)
        {
            _transitionState = ts;
            _componentsBundle = bundle;
        }
    }
}