using UnityEngine;
using UnityEngine.UI;

namespace PullAnimals
{
    /// <summary>
    /// タイトル画像Characterのパーツにアタッチするクラス
    /// </summary>
    
    public class CharacterImage : MonoBehaviour
    {
        /// <summary>
        /// Imageコンポーネントを外部に参照用
        /// </summary>
        [SerializeField] private Image _image;

        public Image MyImage => _image;

        /// <summary>
        /// StarパーツのPrefab
        /// </summary>
        [SerializeField] private StarImage _starImage;

        public void SetStarImage()
        {
            /// Starパーツの初期化
            {
                _starImage.gameObject.SetActive(false);
            }
        }

        public void ShowStar()
        {
            _starImage.gameObject.SetActive(true);
        }
    }

}
