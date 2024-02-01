using System;
using PullAnimals.StatePattern;

namespace PullAnimals
{
    /// <summary>
    /// PlayerのStateを集めたクラス
    /// </summary>
    public class PlayerStateContainer : StateContainerBase,IDisposable
    {
        /// <summary>
        /// 静止中のState
        /// </summary>
        private readonly IdleState _idleState;
        /// <summary>
        /// 移動中のState
        /// </summary>
        private readonly MoveState _moveState;
        /// <summary>
        /// ジャンプ中のState
        /// </summary>
        private readonly JumpState _jumpState;
        /// <summary>
        /// 落下中のState
        /// </summary>
        private readonly FallState _fallState;
        /// <summary>
        /// 動物を引っこ抜いているState
        /// </summary>
        private readonly PullState _pullState;
        /// <summary>
        /// 静止中のState
        /// </summary>
        public IdleState IdleState => _idleState;
        /// <summary>
        /// 移動中のState
        /// </summary>
        public MoveState MoveState => _moveState;
        /// <summary>
        /// ジャンプ中のState
        /// </summary>
        public JumpState JumpState => _jumpState;
        /// <summary>
        /// 落下中のState
        /// </summary>
        public FallState FallState => _fallState;
        /// <summary>
        /// 動物を引っこ抜いているState
        /// </summary>
        public PullState PullState => _pullState;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public PlayerStateContainer(ITransitionState transitionState, PlayerComponentsBundle bundle)
        {
            _idleState = new IdleState(transitionState, bundle);
            _moveState = new MoveState(transitionState, bundle);
            _jumpState = new JumpState(transitionState, bundle);
            _fallState = new FallState(transitionState, bundle);
            _pullState = new PullState(transitionState, bundle);
        }
        
        /// <summary>
        /// 各Stateを初期化させる
        /// </summary>
        public override void Initialize()
        {
            //各Stateを初期化
            ((IState)_idleState).Initialize(this);
            ((IState)_moveState).Initialize(this);
            ((IState)_jumpState).Initialize(this);
            ((IState)_fallState).Initialize(this);
            ((IState)_pullState).Initialize(this);
        }

        public void Dispose()
        {
            //Stateのキャンセルをおこなう
            _fallState?.Dispose();
            _pullState?.Dispose();
            _moveState?.Dispose();
        }
    }
}
