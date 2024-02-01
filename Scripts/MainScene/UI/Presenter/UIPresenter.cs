using UniRx;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// 時間のViewとModelをつなぐPresenter
    /// </summary>
    public class UIPresenter : MonoBehaviour
    {
        ///時間関係
        /// <summary>
        /// ゲーム時間のModel
        /// </summary>
        [SerializeField] private GameTimeController _gameTimeController;
        /// <summary>
        /// カウントダウンのView
        /// </summary>
        [SerializeField] private CountTimeUIView _countTimeUIView;
        /// <summary>
        /// ゲームの残り時間のView
        /// </summary>
        [SerializeField] private GameTimeUIView _gameTimeUIView;
        /// <summary>
        /// 時間表示のUIのViewクラス
        /// </summary>
        [SerializeField] private ClockUIView _clockUIView;
        /// <summary>
        /// ポーズ中のUIを表示するクラス
        /// </summary>
        [SerializeField] private PauseUIView _pauseUIView;
        ///スコア関係
        /// <summary>
        /// ゲームのスコアを管理するクラス(Model)
        /// </summary>
        [SerializeField] private ScoreController _scoreController;
        /// <summary>
        /// スコアの表示をおこなうクラス
        /// </summary>
        [SerializeField] private ScoreUIView _scoreUIView;
        ///引っこ抜き関係
        /// <summary>
        /// プレイヤーの手の役割（引っこ抜く・運ぶなど）を管理するクラス
        /// </summary>
        [SerializeField] private PlayerHandController _handController;
        /// <summary>
        /// ウサギを引っこ抜く際にUIを表示するクラス
        /// </summary>
        [SerializeField] private PullUIView _pullUIView;
        /// <summary>
        /// チュートリアルを管理するクラス
        /// </summary>
        [SerializeField] private TutorialController _tutorialController;
        /// <summary>
        /// ダイアログを管理するクラス
        /// </summary>
        [SerializeField] private DialogUIController _dialogUIController;

        private void Awake()
        {
            //時間に関する処理
            _gameTimeController.Initialize();
            _clockUIView.Initialize(_gameTimeController.MaxTime);
            _gameTimeController.CountdownTimeProperty
                .SkipLatestValueOnSubscribe()
                .Subscribe(x => _countTimeUIView.UpdateCountText(x))
                .AddTo(this);
            _gameTimeController.GameTimeProperty
                .Subscribe(x => _gameTimeUIView.UpdateGameTimeText(x))
                .AddTo(this);
            _gameTimeController.GameTimeProperty
                .Subscribe(x => _clockUIView.UpdateUI(x))
                .AddTo(this);
            _gameTimeController.OnStopGameObservable
                .Subscribe(b => _pauseUIView.ChangePauseView(b))
                .AddTo(this);
            //スコアに関する処理
            _scoreController.Initialize();
            _scoreController.ScoreProperty
                .Subscribe(x => _scoreUIView.UpdateScoreUI(x))
                .AddTo(this);
            _scoreController.OnAddScoreObservable
                .Subscribe(x => _scoreUIView.ShowAddScore(x))
                .AddTo(this);
            //引っ張りに関する処理
            _handController.OnPullingRabbitObservable
                .Subscribe(b => _pullUIView.ChangePullText(b))
                .AddTo(this);
            //チュートリアルに関する処理
            _tutorialController.MessageProperty
                .Subscribe(s => _dialogUIController.ShowDialog(s))
                .AddTo(this);
        }
    }
}
