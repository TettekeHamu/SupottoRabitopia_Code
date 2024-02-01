using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// どこに・いつ・どのくらいウサギを生成・表示するかを管理するScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "RabbitCreateData", menuName = "ScriptableObjects/RabbitCreateData")]
    public class RabbitCreateData : ScriptableObject
    {
        /// <summary>
        /// ウサギの生成位置を管理するデータの配列
        /// </summary>
        [Header("ウサギの生成位置を管理するデータ")]
        [SerializeField] private RabbitCreatePosData[] _rabbitCreatePosDataArray;
        /// <summary>
        /// ウサギの生成をおこなう時間
        /// </summary>
        [Header("ウサギの生成をおこなう時間")]
        [SerializeField] private int _createTime;
        /// <summary>
        /// ウサギを見せる時間（苗・緑ウサギ・完成体）
        /// </summary>
        [Header("芽/緑ウサギ/完全体ウサギをそれぞれ何秒フィールドに出すかの時間")]
        [SerializeField] private int[] _showRabbitTime = new int[3];
        /// <summary>
        /// ウサギの生成位置を管理するデータの配列
        /// </summary>
        public RabbitCreatePosData[] RabbitCreatePosDataArray => _rabbitCreatePosDataArray;
        /// <summary>
        /// ウサギの生成をおこなう時間
        /// </summary>
        public int CreateTime => _createTime;
        /// <summary>
        /// ウサギを見せる時間（苗・緑ウサギ・完成体）
        /// </summary>
        public int[] ShowRabbitTime => _showRabbitTime;
    }
}
