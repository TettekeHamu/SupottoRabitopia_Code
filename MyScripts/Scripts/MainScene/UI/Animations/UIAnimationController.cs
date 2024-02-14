using System;
using DG.Tweening;
using UnityEngine;
using UniRx;

namespace PullAnimals
{
    /// <summary>
    /// リズムに合わせてUIをアニメーションさせるクラス
    /// </summary>
    public class UIAnimationController : MonoBehaviour
    {
        /// <summary>
        /// 開始時の大きさ
        /// </summary>
        private Vector3 _startScale;

        private void Awake()
        {
            _startScale = transform.localScale;
        }

        private void Start()
        {
            //Awakeだと実行順でバグるのでStartでおこなう
            BeatController.Instance.OnRhythmObservable
                .Subscribe(_ => BeatAnimation())
                .AddTo(this);
        }
        
        /// <summary>
        /// アニメーションさせる処理
        /// </summary>
        private void BeatAnimation()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(_startScale * 0.75f, 0.05f))
                .Append(transform.DOScale(_startScale, 0.05f));
        }

    }
}
