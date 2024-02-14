using System;
using UniRx;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// タイム似関するModelとViewをつなぐPresenter
    /// </summary>
    public class GameTimePresenter : MonoBehaviour
    {
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

        public void Awake()
        {
            //ゲームタイムを初期化
            //Awakeの順番でエラーが発生するのでPresenter側でおこなう
            _gameTimeController.Initialize();
            //UI側の初期化
            _clockUIView.Initialize(_gameTimeController.MaxTime);
            //ModelとViewをつなげる
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
        }
    }
}
