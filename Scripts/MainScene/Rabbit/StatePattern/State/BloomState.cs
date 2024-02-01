using Cysharp.Threading.Tasks;
using PullAnimals.StatePattern;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// 白ウサギ（開花中）のState
    /// </summary>
    public class BloomState : IState
    {
        /// <summary>
        /// Stateを変更する用のインターフェース
        /// </summary>
        private ITransitionState _transitionState;
        /// <summary>
        /// AnimalのStateを集めたクラス
        /// </summary>
        private RabbitStateContainer _stateContainer;
        /// <summary>
        /// ウサギの見た目（実体）を管理するクラス
        /// </summary>
        private readonly RabbitViewController _viewController;
        /// <summary>
        /// ウサギの物理演算を管理するクラス
        /// </summary>
        private readonly RabbitPullController _pullController;
        /// <summary>
        /// 自身を破棄するクラス
        /// </summary>
        private readonly RabbitDestroyer _destroyer;
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
            _viewController.ShowView(2);
            _needTime = _remainTimeController.GetRemainTime(this);
            _currentTime = 0;
        }

        void IState.MyUpdate()
        {
            //もし成長に必要な時間を経過 & Playerに持たれていないなら
            if (_currentTime >= _needTime && !_pullController.IsPulled)
            {
                //自身を破棄する
                _particleController.PlayDestroyParticle();
                _destroyer.DestroyRabbit();
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
        public BloomState(ITransitionState ts, RabbitViewController rvc, RabbitPullController rrc, RabbitDestroyer rd, RabbitRemainTimeController rrtc, RabbitParticleController pc)
        {
            _transitionState = ts;
            _viewController = rvc;
            _pullController = rrc;
            _destroyer = rd;
            _remainTimeController = rrtc;
            _particleController = pc;
        }
    }
}
