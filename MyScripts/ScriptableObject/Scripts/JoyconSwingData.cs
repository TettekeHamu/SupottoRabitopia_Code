using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// Joyconを振った/振ってないとみなすスピードを管理するScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "JoyconSwingData", menuName = "ScriptableObjects/JoyconSwingData")]
    public class JoyconSwingData : ScriptableObject
    {
        /// <summary>
        /// 振ったとみなすスピード
        /// </summary>
        [Header("振ったとみなすスピード")]
        [SerializeField] private int _upperLimit;
        /// <summary>
        /// 振ってないとみなすスピード
        /// </summary>
        [Header("振ってないとみなすスピード")]
        [SerializeField] private int _lowerLimit;
        /// <summary>
        /// 振ったとみなすスピード
        /// </summary>
        public int UpperLimit => _upperLimit;
        /// <summary>
        /// 振ってないとみなすスピード
        /// </summary>
        public int LowerLimit => _lowerLimit;
    }
}
