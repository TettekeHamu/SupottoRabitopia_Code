using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// ダイアログに使うキャラクターに関するデータをまとめたクラス
    /// </summary>
    [CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData")]
    public class CharacterData : ScriptableObject
    {
        /// <summary>
        /// キャラクター名
        /// </summary>
        [Header("キャラクターの名前")]
        [SerializeField] private string _characterName;
        /// <summary>
        /// キャラクター画像
        /// </summary>
        [Header("表示するキャラクター")] 
        [SerializeField] private Sprite _characterSprite;
        /// <summary>
        /// キャラクター名
        /// </summary>
        public string CharacterName => _characterName;
        /// <summary>
        /// キャラクター画像
        /// </summary>
        public Sprite CharacterSprite => _characterSprite;
    }
}
