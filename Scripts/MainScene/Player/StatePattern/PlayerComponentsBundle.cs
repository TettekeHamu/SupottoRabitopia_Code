namespace PullAnimals
{
    /// <summary>
    /// Stateが操作するコンポーネントをまとめたクラス
    /// </summary>
    public class PlayerComponentsBundle
    {
        /// <summary>
        /// プレイヤーのアニメーションを管理するクラス
        /// </summary>
        private readonly PlayerAnimatorController _animatorController;
        /// <summary>
        /// プレイヤーを移動させる処理
        /// </summary>
        private readonly PlayerMoveController _moveController;
        /// <summary>
        /// プレイヤーの手の役割（引っこ抜く・運ぶなど）を管理するクラス
        /// </summary>
        private readonly PlayerHandController _handController;
        /// <summary>
        /// ゲームのスコアを管理するクラス
        /// </summary>
        private readonly ScoreController _scoreController;
        /// <summary>
        /// カメラの管理をおこなうクラス
        /// </summary>
        private readonly CameraController _cameraController;
        /// <summary>
        /// プレイヤーのパーティクルを管理するクラス
        /// </summary>
        private readonly PlayerParticleController _particleController;
        /// <summary>
        /// プレイヤーのフィーバーを管理するクラス
        /// </summary>
        private readonly PlayerFeverController _feverController;
        /// <summary>
        /// アニメーションイベントとプレイヤーをつなぐクラス
        /// </summary>
        private readonly PlayerAnimationEventConnector _animationEventConnector; 

        /// <summary>
        /// プレイヤーのアニメーションを管理するクラス
        /// </summary>
        public PlayerAnimatorController AnimatorController => _animatorController;
        /// <summary>
        /// プレイヤーを移動させる処理
        /// </summary>
        public PlayerMoveController MoveController => _moveController;
        /// <summary>
        /// プレイヤーの手の役割（引っこ抜く・運ぶなど）を管理するクラス
        /// </summary>
        public PlayerHandController HandController => _handController;
        /// <summary>
        /// ゲームのスコアを管理するクラス
        /// </summary>
        public ScoreController PlayerScoreController => _scoreController;
        /// <summary>
        /// カメラの管理をおこなうクラス
        /// </summary>
        public CameraController PlayerCameraController => _cameraController;
        /// <summary>
        /// プレイヤーのパーティクルを管理するクラス
        /// </summary>
        public PlayerParticleController ParticleController => _particleController;
        /// <summary>
        /// プレイヤーのフィーバーを管理するクラス
        /// </summary>
        public PlayerFeverController FeverController => _feverController;
        /// <summary>
        /// アニメーションイベントとプレイヤーをつなぐクラス
        /// </summary>
        public PlayerAnimationEventConnector AnimationEventConnector => _animationEventConnector;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public PlayerComponentsBundle(PlayerAnimatorController ac, PlayerMoveController mc, PlayerHandController hc, ScoreController sc, CameraController cc, PlayerParticleController pc, PlayerFeverController pfc, PlayerAnimationEventConnector paec)
        {
            _animatorController = ac;
            _moveController = mc;
            _handController = hc;
            _scoreController = sc;
            _cameraController = cc;
            _particleController = pc;
            _feverController = pfc;
            _animationEventConnector = paec;
        }
    }
}
