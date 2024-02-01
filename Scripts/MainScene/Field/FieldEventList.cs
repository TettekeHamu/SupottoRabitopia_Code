using System;
using System.Collections.Generic;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// フィールドイベントを管理するList
    /// </summary>
    [Serializable]
    public class FieldEventList
    {
        /// publicにしないとInspector上から設定できないので注意する
        /// <summary>
        /// ウサギの生成を管理するデータをまとめたList
        /// </summary>
        [Header("ウサギの生成をおこなう位置をまとめたScriptableObject")]
        public List<RabbitCreatePosData> RabbitCreatePosDataList = new List<RabbitCreatePosData>();
        /// <summary>
        /// 生成をおこなう時間
        /// </summary>
        [Header("ウサギの生成をおこなう時間")]
        public int CreateTime;
        /// <summary>
        /// ウサギを見せる時間（苗・緑ウサギ・完成体）
        /// </summary>
        [Header("芽/緑ウサギ/完全体ウサギをそれぞれ何秒フィールドに出すかの時間")]
        public int[] ShowRabbitTime = new int[3];
    }
}
