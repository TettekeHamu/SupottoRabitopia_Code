using System;
using System.Collections;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// フィールドのグリッドにアタッチするクラス
    /// </summary>
    public class FieldTileController : MonoBehaviour
    {
        /// <summary>
        /// Tileについている穴のオブジェクト
        /// </summary>
        [SerializeField] private GameObject _holeObject;
        /// <summary>
        /// ウサギのPrefabを格納した配列
        /// </summary>
        [SerializeField] private RabbitBehaviour[] _rabbitPrefabs;
        /// <summary>
        /// グリッド上にいるウサギ
        /// </summary>
        private RabbitBehaviour _rabbitObject;

        private void Awake()
        {
            _rabbitObject = null;
        }

        private void Start()
        {
            //Awakeだと実行順でバグるのでStartでおこなう
            if(BeatController.Instance == null) return;
            BeatController.Instance.OnRhythmObservable
                .Subscribe(_ => BeatAnimation())
                .AddTo(this);
        }
        
        /// <summary>
        /// リズムに合わせてアニメーションさせる処理
        /// </summary>
        private void BeatAnimation()
        {
            var startScale = _holeObject.transform.localScale;
            var sequence = DOTween.Sequence();
            sequence.Append(_holeObject.transform.DOScale(startScale * 1.25f, 0.05f))
                .Append(_holeObject.transform.DOScale(startScale, 0.05f));
        }

        /// <summary>
        /// グリッドにウサギを生成する処理
        /// </summary>
        public void CreateRabbit(int[] time, RabbitType rabbitType)
        {
            switch (rabbitType)
            {
                case RabbitType.Normal:
                    _rabbitObject = Instantiate(_rabbitPrefabs[0], transform.position, Quaternion.identity);
                    break;
                case RabbitType.Silver:
                    _rabbitObject = Instantiate(_rabbitPrefabs[1], transform.position, Quaternion.identity);
                    break;
                case RabbitType.Gold:
                    _rabbitObject = Instantiate(_rabbitPrefabs[2], transform.position, Quaternion.identity);
                    break;
                default:
                    Debug.LogWarning("そんなウサギは存在しません！！");
                    break;
            }

            if (_rabbitObject != null)
            {
                _rabbitObject.Initialize(time);
                //最大スコアを加算
                ScoreController.MaxScore += _rabbitObject.Status.Score;
                PlayerPrefs.SetInt("MaxScore", ScoreController.MaxScore);
            }
        }

        /// <summary>
        /// 毎フレームおこなう処理
        /// </summary>
        public void MyUpdate()
        {
            //グリッド上にウサギがいたら成長させる
            if(_rabbitObject == null) return;
            _rabbitObject.MyUpdate();
        }
    }
}
