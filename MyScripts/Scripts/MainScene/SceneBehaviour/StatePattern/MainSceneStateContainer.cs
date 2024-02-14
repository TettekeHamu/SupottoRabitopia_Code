using System;
using PullAnimals.StatePattern;

namespace PullAnimals
{
    /// <summary>
    /// Stateをまとめたクラス
    /// </summary>
    public class MainSceneStateContainer : StateContainerBase,IDisposable
    {
        /// <summary>
        /// ゲーム開始前の準備中のState
        /// </summary>
        private readonly PreparationState _preparationState;
        /// <summary>
        /// ゲーム再生中のState
        /// </summary>
        private readonly PlayState _playState;
        /// <summary>
        /// ゲーム終了時のState
        /// </summary>
        private readonly EndState _endState;
        /// <summary>
        /// ポーズ中のState
        /// </summary>
        private readonly PauseState _pauseState;
        /// <summary>
        /// ゲーム開始前の準備中のState(State変更用)
        /// </summary>
        public PreparationState PreparationState => _preparationState;
        /// <summary>
        /// ゲーム再生中のState(State変更用)
        /// </summary>
        public PlayState PlayState => _playState;
        /// <summary>
        /// ゲーム終了時のState
        /// </summary>
        public EndState EndState => _endState;
        /// <summary>
        /// ポーズ中のState
        /// </summary>
        public PauseState PauseState => _pauseState;

        /// <summary>
        /// コンストラクー
        /// </summary>
        public MainSceneStateContainer(ITransitionState ts, PlayerBehaviour player, GameTimeController gtc, FieldController fc, TutorialController tc)
        {
            _preparationState = new PreparationState(ts, gtc, tc);
            _playState = new PlayState(ts, player, gtc, fc);
            _endState = new EndState(player);
            _pauseState = new PauseState(ts, gtc);
        }
        
        public override void Initialize()
        {
            ((IState)_preparationState).Initialize(this);
            ((IState)_playState).Initialize(this);
            ((IState)_endState).Initialize(this);
            ((IState)_pauseState).Initialize(this);
        }

        public void Dispose()
        {
            _preparationState?.Dispose();
            _endState?.Dispose();
        }
    }
}
