using System;
using UnityEngine;
using UniRx;

namespace PullAnimals
{
    /// <summary>
    /// スコアに関するModelとViewをつなぐPresenter
    /// </summary>
    public class ScorePresenter : MonoBehaviour
    {
        /// <summary>
        /// ゲームのスコアを管理するクラス(Model)
        /// </summary>
        [SerializeField] private ScoreController _scoreController;
        /// <summary>
        /// スコアの表示をおこなうクラス
        /// </summary>
        [SerializeField] private ScoreUIView _scoreUIView;

        private void Awake()
        {
            _scoreController.Initialize();
            _scoreController.ScoreProperty
                .Subscribe(x => _scoreUIView.UpdateScoreUI(x))
                .AddTo(this);
            _scoreController.OnAddScoreObservable
                .Subscribe(x => _scoreUIView.ShowAddScore(x))
                .AddTo(this);
        }
    }
}
