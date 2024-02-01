using System;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// 動物が引っこ抜かれる機能を管理するクラス
    /// </summary>
    public class RabbitPullController : MonoBehaviour
    {
        /// <summary>
        /// 当たり判定をおこなうコンポーネント
        /// </summary>
        [SerializeField] private CapsuleCollider _collider;
        /// <summary>
        /// 引っこ抜いているかどうか
        /// </summary>
        private bool _isPulled;
        /// <summary>
        /// 引っこ抜いているかどうか
        /// </summary>
        public bool IsPulled => _isPulled;

        private void Awake()
        {
            //最初は持てないようにする
            _collider.enabled = false;
            //引っこ抜かれてない
            _isPulled = false;
        }

        /// <summary>
        /// プレイヤーが引っこ抜けるようにする処理
        /// </summary>
        public void CanPulled()
        {
            _collider.enabled = true;
        }

        /// <summary>
        /// 引っこ抜かれてることにする処理
        /// </summary>
        public void StartPulled()
        {
            _isPulled = true;
        }
    }
}
