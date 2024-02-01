using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// SEのAudioSourceの管理をおこなうクラス
    /// </summary>
    public class AudioSourceController : MonoBehaviour
    {
        /// <summary>
        /// 同時に鳴らせる数の最大の個数
        /// </summary>
        [SerializeField] private int _maxAudioSourceNum;
        /// <summary>
        /// AudioSourceをまとめたList
        /// </summary>
        private readonly List<AudioSource> _audioSourceList = new List<AudioSource>();

        private void Awake()
        {
            //最大の個数分AudioSourceを追加する
            for (var i = 0; i < _maxAudioSourceNum; ++i)
            {
                var audioSource = gameObject.AddComponent<AudioSource>();
                //初期設定
                audioSource.playOnAwake = false;
                audioSource.loop = false;
                audioSource.volume = 0.5f;
                _audioSourceList.Add(audioSource);
            }
        }

        /// <summary>
        /// 空いているAudioSourceを返す処理
        /// </summary>
        /// <returns>空いているAudioSource</returns>
        public AudioSource GetFreeAudioSource()
        {
            return _audioSourceList.FirstOrDefault(source => !source.isPlaying);
        }
    }
}
