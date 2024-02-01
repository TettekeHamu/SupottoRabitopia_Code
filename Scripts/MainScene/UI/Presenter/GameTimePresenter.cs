using System;
using UniRx;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// ゲームタイムのModelとViewをつなぐPresenter
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

        public void Initialize()
        {
            //時間に関する処理
            _gameTimeController.Initialize();
            _clockUIView.Initialize(_gameTimeController.MaxTime);
            _gameTimeController.CountdownTimeProperty
                .Skip(1)
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
