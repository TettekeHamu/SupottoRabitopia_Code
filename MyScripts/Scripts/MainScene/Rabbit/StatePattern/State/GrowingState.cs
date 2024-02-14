using PullAnimals.StatePattern;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// 緑ウサギ（成長中）の状態
    /// </summary>
    public class GrowingState : IState
    {
        /// <summary>
        /// Stateを変更する用のインターフェース
        /// </summary>
        private readonly ITransitionState _transitionState;
        /// <summary>
        /// AnimalのStateを集めたクラス
        /// </summary>
        private RabbitStateContainer _stateContainer;
        /// <summary>
        /// ウサギの見た目（実体）を管理するクラス
        /// </summary>
        private readonly RabbitViewController _viewController;
        /// <summary>
        /// 動物が引っこ抜かれる機能を管理するクラス
        /// </summary>
        private readonly RabbitPullController _pullController;
        /// <summary>
        /// ウサギの生存時間を管理するクラス
        /// </summary>
        private readonly RabbitRemainTimeController _remainTimeController;
        /// <summary>
        /// ウサギ生成時のパーティクルを管理するクラス
        /// </summary>
        private readonly RabbitParticleController _particleController;
        /// <summary>
        /// 成長に必要な時間
        /// </summary>
        private float _needTime;
        /// <summary>
        /// 現在の時間
        /// </summary>
        private float _currentTime;
        
        void IState.Initialize(StateContainerBase stateContainer)
        {
            _stateContainer = stateContainer as RabbitStateContainer;
        }

        void IState.Enter()
        {
            _particleController.PlayCreatingParticle(this);
            _viewController.ShowView(1);
            _needTime = _remainTimeController.GetRemainTime(this);
            _currentTime = 0;
        }

        void IState.MyUpdate()
        {
            //もし成長に必要な時間を経過したら
            if (_currentTime >= _needTime && !_pullController.IsPulled)
            {
                //Stateを変更
                _transitionState.TransitionState(_stateContainer.BloomState);
            }
            
            //時間を加算
            _currentTime += Time.deltaTime;
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
        public GrowingState(ITransitionState ts, RabbitViewController rvc, RabbitPullController rpc, RabbitRemainTimeController rrtc, RabbitParticleController pc)
        {
            _transitionState = ts;
            _viewController = rvc;
            _pullController = rpc;
            _remainTimeController = rrtc;
            _particleController = pc;
        }
    }
}
