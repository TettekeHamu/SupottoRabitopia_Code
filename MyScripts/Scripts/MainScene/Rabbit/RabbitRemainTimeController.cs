using PullAnimals.StatePattern;
using UnityEngine;
using Object = System.Object;

namespace PullAnimals
{
    /// <summary>
    /// ウサギの生存時間を管理するクラス
    /// </summary>
    public class RabbitRemainTimeController : MonoBehaviour
    {
        /// <summary>
        /// ウサギを表示する時間をまとめた配列
        /// </summary>
        private int[] _remainTimer;

        /// <summary>
        /// ウサギを表示する時間を設定する処理
        /// </summary>
        /// <param name="time">時間をまとめた配列</param>
        public void SetRemainTime(int[] time)
        {
            _remainTimer = time;
        }

        /// <summary>
        /// 生存時間を返す処理
        /// </summary>
        /// <param name="obj">Stateを表すクラス</param>
        /// <returns>生存時間</returns>
        public int GetRemainTime(IState obj)
        {
            if (obj is SproutState) return _remainTimer[0];
            if (obj is GrowingState) return _remainTimer[1];
            if (obj is BloomState) return _remainTimer[2];
            else
            {
                Debug.Log("RemainTimeが存在しません");
                return 0;
            }
        }
    }
}
