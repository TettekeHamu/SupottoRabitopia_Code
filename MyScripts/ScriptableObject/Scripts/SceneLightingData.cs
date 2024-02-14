using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// ゲーム開始時と終了時のライト情報を格納するScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "SceneLightingData", menuName = "ScriptableObjects/SceneLightingData")]
    public class SceneLightingData : ScriptableObject
    {
        /// <summary>
        /// ゲーム開始時のDirectionalLightのカラーコード
        /// </summary>
        [Header("ゲーム開始時のDirectionalLightのカラーコード(#忘れないように！！)")]
        [SerializeField] private string _startLightColorCode;
        /// <summary>
        /// ゲーム開始時のSkyBoxのカラーコード
        /// </summary>
        [Header("ゲーム開始時のSkyBoxのカラーコード(#忘れないように！！)")]
        [SerializeField] private string _startSkyBoxColorCode;
        /// <summary>
        /// ゲーム終了時のDirectionalLightのカラーコード
        /// </summary>
        [Header("ゲーム終了時のDirectionalLightのカラーコード(#忘れないように！！)")]
        [SerializeField] private string _endLightColorCode;
        /// <summary>
        /// ゲーム終了時のSkyBoxのカラーコード
        /// </summary>
        [Header("ゲーム終了時のSkyBoxのカラーコード(#忘れないように！！)")]
        [SerializeField] private string _endSkyBoxColorCode;
        /// <summary>
        /// ゲーム開始時のLightのカラーコード
        /// </summary>
        public string StartLightColorCode => _startLightColorCode;
        /// <summary>
        /// ゲーム開始時のSkyBoxのカラーコード
        /// </summary>
        public string StartSkyBoxColorCode => _startSkyBoxColorCode;
        /// <summary>
        /// ゲーム終了時のLightのカラーコード
        /// </summary>
        public string EndLightColorCode => _endLightColorCode;
        /// <summary>
        /// ゲーム終了時のSkyBoxのカラーコード
        /// </summary>
        public string EndSkyBoxColorCode => _endSkyBoxColorCode;
    }
}
