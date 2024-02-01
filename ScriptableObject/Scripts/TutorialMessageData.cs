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
        /// ウサギの生成位置を管理するデータの配列
        /// </summary>
        [Header("チュートリアルに出す文字列")]
        [SerializeField] private string[] _messageArray;
        /// <summary>
        /// ウサギの生成位置を管理するデータの配列
        /// </summary>
        public string[] MessageArray => _messageArray;
    }
}
