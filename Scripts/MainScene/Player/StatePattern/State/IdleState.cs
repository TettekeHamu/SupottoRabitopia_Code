using PullAnimals.StatePattern;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// 待機中のState
    /// </summary>
    public class IdleState : IState
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
        
        void IState.Initialize(StateContainerBase stateContainer)
        {
            _playerStateContainer = stateContainer as PlayerStateContainer;
        }

        void IState.Enter()
        {
            //下方向に速度を持たせる
            _componentsBundle.MoveController.IdlePlayer(_componentsBundle.AnimatorController);
        }

        void IState.MyUpdate()
        {
            //入力があれば引っこ抜く or 投げ飛ばす
            if (MyInputController.Instance.HandActionKeyDown)
            {
                var canPull = _componentsBundle.HandController.PullAnimal();
                if (canPull)
                {
                    _transitionState.TransitionState(_playerStateContainer.PullState);   
                    return;
                }
            }
            
            /*
            //下に何もなければ落下させる
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
            
            //入力があれば移動させる
            if (MyInputController.Instance.MoveInputVector2.magnitude >= 0.1f)
            {
                _transitionState.TransitionState(_playerStateContainer.MoveState);   
                return;
            }

            //下方向に速度を持たせる
            _componentsBundle.MoveController.IdlePlayer(_componentsBundle.AnimatorController);
            
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
        public IdleState(ITransitionState ts, PlayerComponentsBundle bundle)
        {
            _transitionState = ts;
            _componentsBundle = bundle;
        }
    }
}