using System;
using UnityEngine;
using UnityEngine.UI;

namespace PullAnimals
{
    /// <summary>
    /// ゲームの残り時間を表示するUI
    /// </summary>
    public class GameTimeUIView : MonoBehaviour
    {
        /// <summary>
        /// カウントダウンのUIを表示させるText
        /// </summary>
        [SerializeField] private Text _gameTimeText;
        /// <summary>
        /// ゲーム終了時のUI
        /// </summary>
        [SerializeField] private Text _endText;

        private void Awake()
        {
            _endText.gameObject.SetActive(false);   
        }

        /// <summary>
        /// UIを更新する処理
        /// </summary>
        /// <param name="currentTime">更新する値</param>
        public void UpdateGameTimeText(float currentTime)
        {
            if (currentTime <= 0f)
            {
                _gameTimeText.text = "のこり時間：00:00";
                _endText.gameObject.SetActive(true);
            }
            else
            {
                _gameTimeText.text = "のこり時間：" + currentTime.ToString("F2");   
            }
        }
    }
}
