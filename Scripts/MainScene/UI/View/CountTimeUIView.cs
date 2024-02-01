using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PullAnimals
{
    /// <summary>
    /// 時間のUIを管理するクラス
    /// </summary>
    public class CountTimeUIView : MonoBehaviour
    {
        /// <summary>
        /// カウントダウンのUIを表示させるText
        /// </summary>
        [SerializeField] private Text _countText;

        private void Awake()
        {
            //ゲーム開始時は非表示に
            _countText.gameObject.SetActive(false);
        }

        /// <summary>
        /// 1秒だけGameStartと表記をおこなうコルーチン
        /// </summary>
        /// <returns></returns>
        private IEnumerator ShowGameStartCoroutine()
        {
            _countText.text = "GameStart!!";
            yield return new WaitForSeconds(1);
            _countText.gameObject.SetActive(false);
        }

        /// <summary>
        /// カウントダウンTextを更新させる処理
        /// </summary>
        /// <param name="countNum">カウント</param>
        public void UpdateCountText(int countNum)
        {
            _countText.gameObject.SetActive(true);
            if (countNum == 0)
            {
                StartCoroutine(ShowGameStartCoroutine());
            }
            else
            {
                _countText.text = countNum.ToString();
            }
        }
    }
}
