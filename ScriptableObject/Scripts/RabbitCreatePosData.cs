using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// ウサギの生成位置を管理するデータ
    /// </summary>
    [CreateAssetMenu(fileName = "RabbitCreatePosData", menuName = "ScriptableObjects/RabbitCreatePosData")]
    public class RabbitCreatePosData : ScriptableObject
    {
        /// <summary>
        /// 通常のウサギの生成をおこなう場所
        /// </summary>
        [Header("ノーマルウサギを生成する箇所")]
        [Vector2IntRange(0,5,0,3)]
        [SerializeField] private Vector2Int[] _normalCreatePos;
        /// <summary>
        /// 銀色のウサギの生成をおこなう場所
        /// </summary>
        [Header("銀色ウサギを生成する箇所")]
        [Vector2IntRange(0,5,0,3)]
        [SerializeField] private Vector2Int[] _silverCreatePos;
        /// <summary>
        /// 金色のウサギの生成をおこなう場所
        /// </summary>
        [Header("金色ウサギを生成する箇所")]
        [Vector2IntRange(0,5,0,3)]
        [SerializeField] private Vector2Int[] _goldCreatePos;
        /// <summary>
        /// 通常のウサギの生成をおこなう場所
        /// </summary>
        public Vector2Int[] NormalCreatePos => _normalCreatePos;
        /// <summary>
        /// 銀色のウサギの生成をおこなう場所
        /// </summary>
        public Vector2Int[] SilverCreatePos => _silverCreatePos;
        /// <summary>
        /// 金色のウサギの生成をおこなう場所
        /// </summary>
        public Vector2Int[] GoldCreatePos => _goldCreatePos;
    }
}
