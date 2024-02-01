using PullAnimals.StatePattern;

namespace PullAnimals
{
    /// <summary>
    /// アニマルのStateを管理するクラス
    /// </summary>
    public class RabbitStateMachine : StateMachineBase
    {
        /// <summary>
        /// アニマルのStateをまとめたクラス
        /// </summary>
        private readonly RabbitStateContainer _stateContainer;
        
        /// <summary>
        /// コンストラクター
        /// </summary>
        public RabbitStateMachine(RabbitViewController rvc, RabbitPullController rrc, RabbitDestroyer rd, RabbitRemainTimeController rrtc, RabbitParticleController pc)
        {
            _stateContainer = new RabbitStateContainer(this, rvc, rrc, rd, rrtc, pc);
            _stateContainer.Initialize();
            Initialize(_stateContainer.SproutState);
        }

        /// <summary>
        /// 現在のStateを返す処理
        /// </summary>
        /// <returns></returns>
        public IState GetCurrentState()
        {
            return _currentState;
        }
    }
}
