using PullAnimals.SceneLoader;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// メインシーンの管理をおこなうクラス
    /// </summary>
    public class MainSceneBehaviour : MonoBehaviour,ISetUpScene
    {
        /// <summary>
        /// Inspector上からデバッグを実行するためのフラグ
        /// </summary>
        public bool _isDebug;
        /// <summary>
        /// ゲームに関する時間を管理するクラス
        /// </summary>
        [SerializeField] private GameTimeController _gameTimeController;
        /// <summary>
        /// プレイヤー
        /// </summary>
        [SerializeField] private PlayerBehaviour _player;
        /// <summary>
        /// フィールドを管理するクラス
        /// </summary>
        [SerializeField] private FieldController _fieldController;
        /// <summary>
        /// チュートリアルを管理するクラス
        /// </summary>
        [SerializeField] private TutorialController _tutorialController;
        /// <summary>
        /// シーンを管理するStateMachine
        /// </summary>
        private MainSceneStateMachine _stateMachine;

        private void MyUpdate()
        {
            _stateMachine.MyUpdate();
        }

        private void MyFixedUpdate()
        {
            _stateMachine.MyFixedUpdate();
        }

        private void OnDestroy()
        {
            _stateMachine?.Dispose();
        }

        private void Update()
        {
            //Inspector上から入力があればSetup()をおこなう
            if (_isDebug)
            {
#if UNITY_EDITOR
                _isDebug = false;
                ISetUpScene setUpScene = this;
                setUpScene.SetUpScene();          
#endif
            }
        }
        
        /// <summary>
        /// ここで初期化をおこなう
        /// </summary>
        void ISetUpScene.SetUpScene()
        {
            //StateMachineを生成
            _stateMachine = new MainSceneStateMachine(_player, _gameTimeController, _fieldController, _tutorialController);

            this.UpdateAsObservable()
                .Subscribe(_ => MyUpdate())
                .AddTo(this);
            this.FixedUpdateAsObservable()
                .Subscribe(_ => MyFixedUpdate())
                .AddTo(this);

            _gameTimeController.OnEndGameObservable
                .First()
                .Subscribe(_ => _stateMachine.TransitionEndState())
                .AddTo(this);
        }
    }
}
