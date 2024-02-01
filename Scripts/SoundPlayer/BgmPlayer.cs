using System.Collections.Generic;
using System.Linq;
using PullAnimals.Singleton;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// BGMの再生をおこなうシングルトンクラス
    /// </summary>
    public class BgmPlayer : DDMonoSingletonBase<BgmPlayer>
    {
        /// <summary>
        /// AudioSourceコンポーネント
        /// </summary>
        [SerializeField] private AudioSource _audioSource;
        /// <summary>
        /// ゲーム開始時に音を再生するかどうか
        /// </summary>
        [SerializeField] private bool _awakePlay;
        /// <summary>
        /// BGMをまとめたList
        /// </summary>
        [SerializeField] private List<AudioClip> _bgmList;
        /// <summary>
        /// BGM名とそれに対応した番号をまとめたDictionary
        /// </summary>
        private readonly Dictionary<string, int> _bgmDictionary = new Dictionary<string, int>();
        /// <summary>
        /// 現在再生中のBGMの番号
        /// </summary>
        private int _currentBgmNumber = -1;

        protected override void Awake()
        {
            //親クラスを初期化
            base.Awake();
            
            //ListからDictionaryに置き換える
            for (var i = 0; i < _bgmList.Count; i++)
            {
                var bgm = _bgmList[i];
                //名前被りを防ぐ
                if (_bgmDictionary.ContainsKey(bgm.name)) return;
                //追加
                _bgmDictionary.Add(bgm.name, i);
            }
            
            //AudioSourceの初期設定
            _audioSource.Stop();
            _audioSource.clip = null;
            _audioSource.playOnAwake = false;
            _audioSource.loop = true;
        }

        private void Start()
        {
            //AwakePlayがtrueなら一番上のBGMを再生
            if (_awakePlay) Play(0);
        }
        
        /// <summary>
        /// BGMを再生する処理
        /// </summary>
        /// <param name="bgmNum">Listの番号</param>
        private void Play(int bgmNum)
        {
            //BGMが１つ以上設定されているかチェック
            if (!_bgmList.Any())
            {
                Debug.LogWarning("BGMが１つもありません！");
                return;
            }

            //範囲外の番号を渡されてないかチェック
            if (bgmNum < 0 || _bgmList.Count <= bgmNum)
            {
                Debug.LogWarning("そんな番号はありません！");
                return;
            }

            //１つ前に再生していたBGMと同じBGMなら再開させる
            if (bgmNum == _currentBgmNumber)
            {
                if(_audioSource.isPlaying) return;
                _audioSource.Play();
            }
            //新規のBGMを流す
            else
            {
                var clip = _bgmList[bgmNum];
                if(_audioSource.isPlaying)_audioSource.Stop();
                _audioSource.clip = clip;
                _audioSource.Play();
                _audioSource.volume = 1f;
                _currentBgmNumber = bgmNum;
            }
        }
        
        /// <summary>
        /// BGMを再生する処理
        /// </summary>
        /// <param name="bgmName">BGM名</param>
        public void Play(string bgmName)
        {
            //BGM名があるかどうかチェック
            if (!_bgmDictionary.ContainsKey(bgmName))
            {
                Debug.LogWarning("そんなBGMは存在しません！");
                return;
            }
            
            //指定された名前のBGMを流す
            Play(_bgmDictionary[bgmName]);
        }

        /// <summary>
        /// 今流れているBGMを一時中断する処理
        /// </summary>
        public void Pause()
        {
            if(_audioSource.isPlaying) _audioSource.Pause();
        }

        /// <summary>
        /// BGMを完全に止める処理
        /// </summary>
        public void Stop()
        {
            _audioSource.Stop();
            _currentBgmNumber = -1;
        }

        /// <summary>
        /// 音量を変える処理
        /// 1が最大、0が最低
        /// </summary>
        public void ChangeVolume(float newVolume)
        {
            _audioSource.volume = newVolume;
        }
    }
}
