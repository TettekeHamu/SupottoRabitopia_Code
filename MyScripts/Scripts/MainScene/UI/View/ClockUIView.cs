using System;
using UnityEngine;
using UnityEngine.UI;

namespace PullAnimals
{
    /// <summary>
    /// 時間表示のUIのViewクラス
    /// </summary>
    public class ClockUIView : MonoBehaviour
    {
        /// <summary>
        /// 時計の針のImage
        /// </summary>
        [SerializeField] private Image _clockHands;
        /// <summary>
        /// 時間の最大値
        /// </summary>
        private int _maxTime;
        
        /// <summary>
        /// 初期化処理
        /// </summary>
        public void Initialize(int time)
        {
            _maxTime = time;
        }
        
        /// <summary>
        /// 時計の針を進める処理
        /// </summary>
        /// <param name="currentTime">現在の時間</param>
        public void UpdateUI(float currentTime)
        {
            //currentTimeが90 →　-90度回転
            //currentTimeが60 →　-180度回転
            //currentTimeが30 →　-270度回転
            _clockHands.rectTransform.rotation = Quaternion.Euler(0, 0, (currentTime - _maxTime) * 3f);
        }
    }
}
