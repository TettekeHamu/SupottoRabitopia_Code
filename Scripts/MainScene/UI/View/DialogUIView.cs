using System;
using UnityEngine;
using UnityEngine.UI;

namespace PullAnimals
{
    /// <summary>
    /// ダイアログを管理するクラス
    /// </summary>
    public class DialogUIController : MonoBehaviour
    {
        /// <summary>
        /// ダイアログのイメージ
        /// </summary>
        [SerializeField] private Image _dialogImage;
        /// <summary>
        /// ダイアログのText
        /// </summary>
        [SerializeField] private Text _dialogText;

        private void Awake()
        {
            _dialogImage.gameObject.SetActive(false);
        }

        /// <summary>
        /// ダイアログを表示させる処理
        /// </summary>
        public void ShowDialog(string str)
        {
            if (str == null)
            {
                _dialogImage.gameObject.SetActive(false);
            }
            else
            {
                _dialogImage.gameObject.SetActive(true);
                _dialogText.text = str;
                // "\n"が含まれていればそこで改行する
                if (str.Contains("\\n"))
                {
                    _dialogText.text = _dialogText.text.Replace(@"\n", Environment.NewLine);
                }
            }
        }
    }
}
