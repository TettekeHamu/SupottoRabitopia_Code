using System;
using System.Collections;
using PullAnimals.Singleton;
using UniRx;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// BPMに合わせて発火するクラス
    /// </summary>
    public class BeatController : MonoSingletonBase<BeatController>
    {
        /// <summary>
        /// BGMのBPM
        /// </summary>
        private int _bpm;
        /// <summary>
        /// 待ち時間
        /// </summary>
        private float _waitTime;
        /// <summary>
        /// 経過時間
        /// </summary>
        private float _timer;
        /// <summary>
        /// プレイ中かどうか
        /// </summary>
        private bool _isPlaying;
        /// <summary>
        /// BPMに合わせて発火するSubject
        /// </summary>
        private readonly Subject<Unit> _onRhythmSubject = new Subject<Unit>();
        /// <summary>
        /// BPMに合わせて発火するSubjectのObservable
        /// </summary>
        public IObservable<Unit> OnRhythmObservable => _onRhythmSubject;

        protected override void Awake()
        {
            base.Awake();
            _bpm = 138;
            //タイマーをリセット
            _timer = 0;
            //待ち時間
            //60BPMなら1秒ごとに発火するようにする
            _waitTime = 60f / _bpm;
            //再生中じゃないようにする
            _isPlaying = false;
        }

        private void Update()
        {
            if(!_isPlaying) return;
            //BPMに合わせて発火させる
            _timer += Time.deltaTime;
            if (_timer >= _waitTime)
            {
                _timer = 0;
                _onRhythmSubject.OnNext(Unit.Default);
                //Debug.Log("発火時間 is" + Time.time);
            }
        }

        /// <summary>
        /// BGMが開始したときに呼ぶ処理
        /// </summary>
        public void StartPlaying()
        {
            _isPlaying = true;
        }
    }
}
