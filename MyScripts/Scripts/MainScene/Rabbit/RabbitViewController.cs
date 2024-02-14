using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// ウサギの見た目（実体）を管理するクラス
    /// </summary>
    public class RabbitViewController : MonoBehaviour
    {
        /// <summary>
        /// 見た目（実体）を格納した配列
        /// </summary>
        [SerializeField] private GameObject[] _rabbitViews;

        private void Awake()
        {
            //一旦すべてのウサギをオフにする
            foreach (var rabbit in _rabbitViews)
            {
                rabbit.SetActive(false);
            }
        }

        /// <summary>
        /// 番号に応じたウサギを表示する処理
        /// </summary>
        /// <param name="num">番号</param>
        public void ShowView(int num)
        {
            for (var i = 0; i < _rabbitViews.Length; ++i)
            {
                _rabbitViews[i].SetActive(i == num);
            }
        }
    }
}
