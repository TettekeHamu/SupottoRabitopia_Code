using System;
using UniRx;

namespace PullAnimals.StatePattern
{
    /// <summary>
    /// StateMachineの親クラス
    /// </summary>
    public abstract class StateMachineBase : ITransitionState
    {
        /// <summary>
        /// 現在のState
        /// </summary>
        protected IState _currentState;
        /// <summary>
        /// ひとつ前のState
        /// </summary>
        protected IState _beforeState;
        /// <summary>
        /// Stateが変更された際に変更先のState名を発行するSubject
        /// </summary>
        private readonly Subject<string> _onChangeStateSubject = new Subject<string>();
        /// <summary>
        /// Stateが変更された際に変更先のState名を発行するSubjectの購読する用のObservable
        /// </summary>
        public IObservable<string> OnChangeStateObservable => _onChangeStateSubject;
        
        void ITransitionState.TransitionState(IState newState)
        {
            _currentState.Exit();
            _currentState = newState;
            _onChangeStateSubject.OnNext(newState.GetType().Name);
            _currentState.Enter();
        }
        
        /// <summary>
        /// StateMachineの初期化処理
        /// </summary>
        /// <param name="initializeState">最初に設定したいState</param>
        protected void Initialize(IState initializeState)
        {
            _currentState = initializeState;
            _onChangeStateSubject.OnNext(initializeState.GetType().Name);
            _currentState.Enter();
        }

        /// <summary>
        /// 毎フレームおこなう処理
        /// </summary>
        public void MyUpdate()
        {
            _currentState.MyUpdate();
        }

        /// <summary>
        /// 一定間隔でおこなう処理（物理演算用）
        /// </summary>
        public void MyFixedUpdate()
        {
            _currentState.MyFixedUpdate();
        }
    }
}