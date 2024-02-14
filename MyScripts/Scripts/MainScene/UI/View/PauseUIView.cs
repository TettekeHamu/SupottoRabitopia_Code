using System;
using UnityEngine;
using UnityEngine.UI;

namespace PullAnimals
{
    /// <summary>
    /// ポーズ中のUIを表示するクラス
    /// </summary>
    public class PauseUIView : MonoBehaviour
    {
        /// <summary>
        /// ポーズ中に表示するUI
        /// </summary>
        [SerializeField] private Image _pauseImage;

        private void Awake()
        {
            _pauseImage.gameObject.SetActive(false);
        }

        /// <summary>
        /// ポーズ中のUIの表示を切り替える処理
        /// </summary>
        /// <param name="canView">trueなら表示させる</param>
        public void ChangePauseView(bool canView)
        {
            _pauseImage.gameObject.SetActive(canView);
        }
    }
}
