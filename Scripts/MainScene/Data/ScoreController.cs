using System;
using UniRx;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// ゲームのスコアを管理するクラス
    /// </summary>
    public class ScoreController : MonoBehaviour
    {
        /// <summary>
        /// スコア
        /// </summary>
        private IntReactiveProperty _scoreProperty;
        /// <summary>
        /// スコア加算がおこなわれたときに発効するSubject
        /// </summary>
        private readonly Subject<int> _onAddScoreSubject = new Subject<int>();
        /// <summary>
        /// スコア
        /// </summary>
        public IReadOnlyReactiveProperty<int> ScoreProperty => _scoreProperty;
        /// <summary>
        /// スコア加算がおこなわれたときに発効するSubjectのObservable
        /// </summary>
        public IObservable<int> OnAddScoreObservable => _onAddScoreSubject;

        /// <summary>
        /// 初期化処理
        /// </summary>
        public void Initialize()
        {
            _scoreProperty = new IntReactiveProperty(0);
            PlayerPrefs.SetInt("TotalScore", _scoreProperty.Value);
        }

        /// <summary>
        /// スコアを加算する処理
        /// </summary>
        /// <param name="value">加算する量</param>
        public void AddScore(int value)
        {
            _onAddScoreSubject.OnNext(value);
            _scoreProperty.Value += value;
            PlayerPrefs.SetInt("TotalScore", _scoreProperty.Value);
        }
    }
}
