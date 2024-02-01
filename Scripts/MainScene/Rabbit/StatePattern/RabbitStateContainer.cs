using PullAnimals.StatePattern;

namespace PullAnimals
{
    /// <summary>
    /// AnimalのStateを集めたクラス
    /// </summary>
    public class RabbitStateContainer : StateContainerBase
    {
        /// <summary>
        /// 芽の状態のState
        /// </summary>
        private readonly SproutState _sproutState;
        /// <summary>
        /// 緑ウサギ（成長中）の状態
        /// </summary>
        private readonly GrowingState _growingState;
        /// <summary>
        /// 白ウサギ（開花中）のState
        /// </summary>
        private readonly BloomState _bloomState;
        /// <summary>
        /// 芽の状態のStateのGetter
        /// </summary>
        public SproutState SproutState => _sproutState;
        /// <summary>
        /// 緑ウサギ（成長中）の状態のGetter
        /// </summary>
        public GrowingState GrowingState => _growingState;
        /// <summary>
        /// 白ウサギ（開花中）のStateのGetter
        /// </summary>
        public BloomState BloomState => _bloomState;
        
        /// <summary>
        /// コンストラクター
        /// </summary>
        public RabbitStateContainer(ITransitionState ts, RabbitViewController rvc, RabbitPullController rrc, RabbitDestroyer rd, RabbitRemainTimeController rrtc, RabbitParticleController pc)
        {
            _sproutState = new SproutState(ts, rvc, rrc, rrtc, pc);
            _growingState = new GrowingState(ts, rvc, rrc, rrtc, pc);
            _bloomState = new BloomState(ts, rvc, rrc, rd, rrtc, pc);
        }
        
        /// <summary>
        /// 初期化用処理
        /// </summary>
        public override void Initialize()
        {
            ((IState)_sproutState).Initialize(this);
            ((IState)_growingState).Initialize(this);
            ((IState)_bloomState).Initialize(this);
        }
    }
}
