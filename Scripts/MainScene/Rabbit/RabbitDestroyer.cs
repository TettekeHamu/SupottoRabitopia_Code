using System.Collections;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// 自身を破棄するクラス
    /// </summary>
    public class RabbitDestroyer : MonoBehaviour
    {
        /// <summary>
        /// 自身を破棄する処理
        /// </summary>
        public void DestroyRabbit()
        {
            Destroy(gameObject);
        }
    }
}
