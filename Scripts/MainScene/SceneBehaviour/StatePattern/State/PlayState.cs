using PullAnimals.StatePattern;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// プレイヤーが操作可能なState
    /// </summary>
    public class PlayState : IState
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
        /// Playerの管理をおこなうクラス
        /// </summary>
        private readonly PlayerBehaviour _player;
        /// <summary>
        /// ゲームの時間を管理するクラス
        /// </summary>
        private readonly GameTimeController _gameTimeController;
        /// <summary>
        /// フィールドを管理するクラス
        /// </summary>
        private readonly FieldController _fieldController;
        
        void IState.Initialize(StateContainerBase stateContainer)
        {
            _stateContainer = stateContainer as MainSceneStateContainer;
        }

        void IState.Enter()
        {
            BgmPlayer.Instance.Play("BGM_Main");
            BgmPlayer.Instance.ChangeVolume(0.5f);
            BeatController.Instance.StartPlaying();
            _fieldController.Initialize();
        }

        void IState.MyUpdate()
        {
            //入力があればポーズ中にする
            if (MyInputController.Instance.ChangeGameModeKeyDown)
            {
                _transitionState.TransitionState(_stateContainer.PauseState);
                return;
            }
            
            //プレイヤーを動かす
            _player.MyUpdate();
            //時間を減らす
            _gameTimeController.ReduceGameTime();
            //フィールドの更新とウサギの成長をおこなう
            _fieldController.MyUpdate();
        }

        void IState.MyFixedUpdate()
        {
            //プレイヤーの物理演算周りをおこなう
            _player.MyFixedUpdate();
        }

        void IState.Exit()
        {
            
        }
        
        /// <summary>
        /// コンストラクター
        /// </summary>
        public PlayState(ITransitionState ts, PlayerBehaviour player, GameTimeController gtc, FieldController fc)
        {
            _transitionState = ts;
            _player = player;
            _gameTimeController = gtc;
            _fieldController = fc;
        }
    }
}
