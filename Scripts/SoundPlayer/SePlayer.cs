using System.Collections.Generic;
using System.Linq;
using PullAnimals.Singleton;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// SEの再生をおこなうシングルトンクラス
    /// </summary>
    public class SePlayer : DDMonoSingletonBase<SePlayer>
    {
        /// <summary>
        /// SEのAudioSourceの管理をおこなうクラス
        /// </summary>
        [SerializeField] private AudioSourceController _audioSourceController;
        /// <summary>
        /// SEをまとめたList
        /// </summary>
        [SerializeField] private List<AudioClip> _seList;
        /// <summary>
        /// SE名とそれに対応した番号をまとめたDictionary
        /// </summary>
        private readonly Dictionary<string, int> _seDictionary = new Dictionary<string, int>();

        protected override void Awake()
        {
            //親クラスを初期化
            base.Awake();
            //ListからDictionaryに置き換える
            for (var i = 0; i < _seList.Count; ++i)
            {
                var se = _seList[i];
                if (_seDictionary.ContainsKey(se.name)) return;
                _seDictionary.Add(se.name, i);
            }
            
        }

        /// <summary>
        /// SEを再生する処理
        /// </summary>
        /// <param name="seName">SE名</param>
        public void Play(string seName)
        {
            //SE名があるかどうかチェック
            if (!_seDictionary.ContainsKey(seName))
            {
                Debug.LogWarning("そんなSEは存在しません！");
                return;
            }
            
            //指定された名前のBGMを流す
            Play(_seDictionary[seName]);
        }
        
        /// <summary>
        /// SEを再生する処理
        /// </summary>
        /// <param name="seNum">Listの番号</param>
        private void Play(int seNum)
        {
            //BGMが１つ以上設定されているかチェック
            if (!_seList.Any())
            {
                Debug.LogWarning("SEが１つもありません！");
                return;
            }

            //範囲外の番号を渡されてないかチェック
            if (seNum < 0 || _seList.Count <= seNum)
            {
                Debug.LogWarning("そんな番号はありません！");
                return;
            }

            //空いているAudioSourceを取得
            var audioSource = _audioSourceController.GetFreeAudioSource();

            //空いてなかったら警告
            if (audioSource == null)
            {
                Debug.LogWarning("SEの同時再生数が上限に達してます");
                return;
            }

            //再生をおこなう
            var se = _seList[seNum];
            audioSource.clip = se;
            audioSource.Play();
        }

        /// <summary>
        /// SEの長さを返す処理
        /// </summary>
        /// <param name="seName">SEの名前</param>
        /// <returns>長さ</returns>
        public float GetSeLength(string seName)
        {
            //SE名があるかどうかチェック
            if (!_seDictionary.ContainsKey(seName))
            {
                //なければ0を返す
                Debug.LogWarning("そんなSEは存在しません！");
                return 0;
            }

            var num = _seDictionary[seName];
            var se = _seList[num];
            return se.length;
        }
    }
}
