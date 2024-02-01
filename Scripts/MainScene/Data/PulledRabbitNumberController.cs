using System;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// 引っこ抜かれたウサギを集計するクラス
    /// </summary>
    public class PulledRabbitNumberController : MonoBehaviour
    {
        /// <summary>
        /// ノーマルウサギの数
        /// </summary>
        private int _normalCount;
        /// <summary>
        /// 銀色ウサギの数
        /// </summary>
        private int _silverCount;
        /// <summary>
        /// 金色ウサギの数
        /// </summary>
        private int _goldCount;

        private void Awake()
        {
            //カウントをリセット
            _normalCount = 0;
            _silverCount = 0;
            _goldCount = 0;
            PlayerPrefs.SetInt("NormalRabbit", _normalCount);
            PlayerPrefs.SetInt("SilverRabbit", _silverCount);
            PlayerPrefs.SetInt("GoldRabbit", _goldCount);
        }

        /// <summary>
        /// カウントを追加する処理
        /// </summary>
        public void AddCount(RabbitStateMachine rabbit, RabbitStatusData status)
        {
            var state = rabbit.GetCurrentState();
            if (state is BloomState)
            {
                switch (status.RabbitType)
                {
                    case RabbitType.Normal:
                        _normalCount++;
                        PlayerPrefs.SetInt("NormalRabbit", _normalCount);
                        break;
                    case RabbitType.Silver:
                        _silverCount++;
                        PlayerPrefs.SetInt("SilverRabbit", _silverCount);
                        break;
                    case RabbitType.Gold:
                        _goldCount++;
                        PlayerPrefs.SetInt("GoldRabbit", _goldCount);
                        break;
                    default:
                        Debug.LogWarning("そんなウサギは存在しません！");
                        break;
                }
            }
        }
    }
}
