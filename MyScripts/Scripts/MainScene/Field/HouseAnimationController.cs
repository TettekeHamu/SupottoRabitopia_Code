using DG.Tweening;
using UniRx;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// フィールド外の家のアニメーションをおこなうクラス
    /// </summary>
    public class HouseAnimationController : MonoBehaviour
    {
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
            var startScale = transform.localScale;
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(startScale * 1.5f, 0.05f))
                .Append(transform.DOScale(startScale, 0.05f))
                .SetLink(gameObject);
        }
    }
}
