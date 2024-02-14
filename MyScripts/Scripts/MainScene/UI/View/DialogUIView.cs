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
        /// <summary>
        /// 右上に出すキャラクターイメージ
        /// </summary>
        [SerializeField] private Image _characterImage;
        /// <summary>
        /// 左上の名前のText
        /// </summary>
        [SerializeField] private Text _nameText;

        private void Awake()
        {
            _dialogImage.gameObject.SetActive(false);
            _characterImage.gameObject.SetActive(false);
        }

        /// <summary>
        /// ダイアログを表示させる処理
        /// </summary>
        /// <param name="data">ダイアログに関するデータ</param>
        public void ShowDialog(DialogData data)
        {
            if (data == null)
            {
                //nullが送られてきたら閉じる
                _dialogImage.gameObject.SetActive(false);
                _characterImage.gameObject.SetActive(false);
            }
            else
            {
                //ダイアログを表示
                _dialogImage.gameObject.SetActive(true);
                _dialogText.text = data.Message;
                // "\n"が含まれていればそこで改行する
                if (data.Message.Contains("\\n"))
                {
                    _dialogText.text = _dialogText.text.Replace(@"\n", Environment.NewLine);
                }
                
                //キャラクターを表示
                if (data.CharacterData.CharacterSprite == null)
                {
                    //真っ白な画像が表示されるのを防ぐ
                    _characterImage.gameObject.SetActive(false);
                }
                else
                {
                    _characterImage.gameObject.SetActive(true);
                    _characterImage.sprite = data.CharacterData.CharacterSprite;   
                }
                
                //キャラクター名を表示
                if (data.CharacterData.CharacterName == null)
                {
                    //空欄に
                    _nameText.text = "";
                }
                else
                {
                    _nameText.text = data.CharacterData.CharacterName;
                }
            }
        }
    }
}
