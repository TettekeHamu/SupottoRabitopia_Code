using UnityEngine;
using UnityEngine.UI;

namespace PullAnimals
{
    /// <summary>
    /// タイトル画像Smokeのパーツにアタッチするクラス
    /// </summary>
    public class SmokeImage : MonoBehaviour
    {
        /// <summary>
        /// Imageコンポーネントを外部に参照用
        /// </summary>
        [SerializeField] private Image _image;

        public Image MyImage => _image;
    }

}
