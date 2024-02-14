using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// 時間に関するデータのScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "GameTimeData", menuName = "ScriptableObjects/GameTimeData")]
    public class GameTimeData : ScriptableObject
    {
        /// <summary>
        /// ゲームをおこなう時間
        /// </summary>
        [Header("ゲームをおこなう時間")]
        [SerializeField] private int _maxGameTime;
        /// <summary>
        /// ゲーム開始前のカウントダウンの時間
        /// </summary>
        [Header("カウントダウンをおこなう時間")]
        [SerializeField] private int _countDownTime;
        /// <summary>
        /// ゲームをおこなう時間
        /// </summary>
        public int MaxGameTime => _maxGameTime;
        /// <summary>
        /// ゲーム開始前のカウントダウンの時間
        /// </summary>
        public int CountDownTime => _countDownTime;
    }
}
