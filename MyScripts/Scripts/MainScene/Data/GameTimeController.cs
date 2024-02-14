using System;
using UniRx;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// ゲームに関する時間を管理するクラス
    /// </summary>
    public class GameTimeController : MonoBehaviour
    {
        /// <summary>
        /// 時間に関するデータのScriptableObject
        /// </summary>
        [SerializeField] private GameTimeData _gameTimeData;
        /// <summary>
        /// ゲーム開始時に使うカウントダウンの時間
        /// </summary>
        private IntReactiveProperty _countdownTimeProperty;
        /// <summary>
        /// ゲームの残り時間
        /// </summary>
        private FloatReactiveProperty _gameTimeProperty;
        /// <summary>
        /// 時間切れ時に発行するSubject
        /// </summary>
        private readonly Subject<Unit> _onEndGameSubject = new Subject<Unit>();
        /// <summary>
        /// ゲームを一時中断時したときに発効するSubject
        /// </summary>
        private readonly Subject<bool> _onStopGameSubject = new Subject<bool>();
        /// <summary>
        /// ゲームの最大時間
        /// </summary>
        public int MaxTime => _gameTimeData.MaxGameTime;
        /// <summary>
        /// ゲーム開始時に使うカウントダウンの時間(購読用)
        /// </summary>
        public IReadOnlyReactiveProperty<int> CountdownTimeProperty => _countdownTimeProperty;
        /// <summary>
        /// ゲームの残り時間(購読用)
        /// </summary>
        public IReadOnlyReactiveProperty<float> GameTimeProperty => _gameTimeProperty;
        /// <summary>
        /// 時間切れ時に発行するSubjectのIObservable
        /// </summary>
        public IObservable<Unit> OnEndGameObservable => _onEndGameSubject;
        /// <summary>
        /// ゲームを一時中断時したときに発効するSubjectのIObservable
        /// </summary>
        public IObservable<bool> OnStopGameObservable => _onStopGameSubject;

        /// <summary>
        /// 初期化処理
        /// </summary>
        public void Initialize()
        {
            //Presenterが+1することでゲーム開始時にTextが表示されないようにしてあげる
            _countdownTimeProperty = new IntReactiveProperty(_gameTimeData.CountDownTime + 1);
            _gameTimeProperty = new FloatReactiveProperty(_gameTimeData.MaxGameTime);
        }

        /// <summary>
        /// カウントダウンの時間を減らす処理
        /// </summary>
        public void ReduceCountdownTime()
        {
            _countdownTimeProperty.Value--;
        }

        /// <summary>
        /// ゲーム時間を減らす処理
        /// </summary>
        public void ReduceGameTime()
        {
            //時間を減らす
            _gameTimeProperty.Value -= Time.deltaTime;
            //ゲーム終了の処理
            if (_gameTimeProperty.Value <= 0f)
            {
                _onEndGameSubject.OnNext(Unit.Default);
            }
        }

        /// <summary>
        /// ゲームを一時中断・再開させる処理
        /// </summary>
        /// <param name="isStop">trueなら中断させる</param>
        public void ChangeGameMode(bool isStop)
        {
            _onStopGameSubject.OnNext(isStop);
        }
    }
}
