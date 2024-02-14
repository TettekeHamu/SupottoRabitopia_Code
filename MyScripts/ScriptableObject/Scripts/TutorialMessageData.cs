using System.Collections.Generic;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// チュートリアル時に表示する文字列のデータのScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "TutorialMessageData", menuName = "ScriptableObjects/TutorialMessageData")]
    public class TutorialMessageData : ScriptableObject
    {
        /// <summary>
        /// チュートリアルに出す文字列とキャラクター画像をまとめたクラスのList
        /// </summary>
        [Header("ダイアログに出す文章・キャラクター")]
        [SerializeField] private List<DialogData> _dialogDataList = new List<DialogData>();
        /// <summary>
        /// チュートリアルに出す文字列とキャラクター画像をまとめたクラスのList
        /// </summary>
        public List<DialogData> DialogDataList => _dialogDataList;
    }

    /// <summary>
    /// ダイアログ表示に使うデータをまとめたクラス
    /// </summary>
    [System.SerializableAttribute]
    public class DialogData
    {
        /// <summary>
        /// 表示する文字列
        /// </summary>
        [Header("ダイアログに出す文章")] 
        public string Message;
        /// <summary>
        /// キャラクター
        /// </summary>
        [Header("キャラクター")]
        public CharacterData CharacterData;
    }
}
