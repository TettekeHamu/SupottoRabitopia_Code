using PullAnimals.SceneLoader;
using PullAnimals.StatePattern;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// ポーズ中のState
    /// </summary>
    public class PauseState : IState
    {
        /// <summary>
        /// Stateを変更させるメソッドを持つInterface
        /// </summary>
        private readonly ITransitionState _transitionState;
        /// <summary>
        /// Stateをまとめたクラス
        /// </summary>
        private MainSceneStateContainer _stateContainer;
        /// <summary>
        /// ゲームの時間を管理するクラス
        /// </summary>
        private readonly GameTimeController _gameTimeController;
        
        void IState.Initialize(StateContainerBase stateContainer)
        {
            _stateContainer = stateContainer as MainSceneStateContainer;
        }

        void IState.Enter()
        {
            _gameTimeController.ChangeGameMode(true);
        }

        void IState.MyUpdate()
        {
            //入力があれば再開する
            if (MyInputController.Instance.GetChangeGameModeKeyDown())
            {
                _transitionState.TransitionState(_stateContainer.PlayState);
                return;
            }
            
            //入力があればタイトルに戻す
            if (MyInputController.Instance.GetChangeSceneKeyDown())
            {
                SceneLoadController.Instance.LoadNextScene("TitleSceneVer1.0");
            }
        }

        void IState.MyFixedUpdate()
        {
            
        }

        void IState.Exit()
        {
            _gameTimeController.ChangeGameMode(false);
        }
        
        /// <summary>
        /// コンストラクター
        /// </summary>
        public PauseState(ITransitionState ts, GameTimeController gtc)
        {
            _transitionState = ts;
            _gameTimeController = gtc;
        }
    }
}
