using UnityEngine;
using UnityEngine.UI;

namespace PullAnimals
{
    /// <summary>
    /// ウサギを引っこ抜く際にUIを表示するクラス
    /// </summary>
    public class PullUIView : MonoBehaviour
    {
        /// <summary>
        /// 引っこ抜く際に表示するUI
        /// </summary>
        [SerializeField] private Text _pullText;
        /// <summary>
        /// 引っこ抜く際に表示する画像
        /// </summary>
        [SerializeField] private Image _pullImageRed;
        /// <summary>
        /// 引っこ抜く際に表示する画像
        /// </summary>
        [SerializeField] private Image _pullImageBlue;
 
        private void Awake()
        {
            _pullText.gameObject.SetActive(false);
            _pullImageRed.gameObject.SetActive(false);
            _pullImageBlue.gameObject.SetActive(false);
        }

        /// <summary>
        /// UIの表示を切り替える処理
        /// </summary>
        /// <param name="canView">trueなら表示</param>
        public void ChangePullText(bool canView)
        {
            _pullText.gameObject.SetActive(canView);
            _pullImageRed.gameObject.SetActive(canView);
            _pullImageBlue.gameObject.SetActive(canView);
        }
    }
}
