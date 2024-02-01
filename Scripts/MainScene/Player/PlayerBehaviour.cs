using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace PullAnimals
{
    /// <summary>
    /// Playerの管理をおこなうクラス
    /// </summary>
    public class PlayerBehaviour : MonoBehaviour
    {
        /// <summary>
        /// 現在のState（デバッグ用）
        /// </summary>
        [SerializeField] private string _currentStateDebug;
        /// <summary>
        /// Stateを確認する用のText（デバッグ用）
        /// </summary>
        [SerializeField] private Text _stateText;
        /// <summary>
        /// プレイヤーのアニメーションを管理するクラス
        /// </summary>
        [SerializeField] private PlayerAnimatorController _animatorController;
        /// <summary>
        /// プレイヤーを移動させる処理
        /// </summary>
        [SerializeField] private PlayerMoveController _playerMove;
        /// <summary>
        /// 動物を引っこ抜く処理
        /// </summary>
        [SerializeField] private PlayerHandController _handController;
        /// <summary>
        /// Playerのパーティクルを管理するクラス
        /// </summary>
        [SerializeField] private PlayerParticleController _particleController;
        /// <summary>
        /// プレイヤーのフィーバーを管理するクラス
        /// </summary>
        [SerializeField] private PlayerFeverController _feverController;
        /// <summary>
        /// アニメーションイベントとプレイヤーをつなぐクラス
        /// </summary>
        [SerializeField] private PlayerAnimationEventConnector _animationEventConnector;
        /// <summary>
        /// ゲームのスコアを管理するクラス
        /// </summary>
        [SerializeField] private ScoreController _scoreController;
        /// <summary>
        /// カメラの管理をおこなうクラス
        /// </summary>
        [SerializeField] private CameraController _cameraController;
        /// <summary>
        /// プレイヤーのStateMachine
        /// </summary>
        private PlayerStateMachine _stateMachine;

        private void Awake()
        {
            //StateMachineを生成
            var bundle = new PlayerComponentsBundle(_animatorController, _playerMove, _handController, _scoreController, _cameraController, _particleController, _feverController, _animationEventConnector);
            _stateMachine = new PlayerStateMachine(bundle);
            _stateMachine.OnChangeStateObservable
                .Subscribe(DebugState)
                .AddTo(this);
        }

        private void OnDestroy()
        {
            _stateMachine.Dispose();
        }

        private void DebugState(string str)
        {
            _currentStateDebug = str;
        }
        
        public void MyUpdate()
        {
            _stateMachine.MyUpdate();
        }

        public void MyFixedUpdate()
        {
            _stateMachine.MyFixedUpdate();
        }

        public void StopPlayer()
        {
            _stateMachine.StopPlayer();
        }
    }
}
