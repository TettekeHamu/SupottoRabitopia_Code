using System;
using UniRx;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// プレイヤーが動物を引っこ抜ける範囲を管理するクラス
    /// </summary>
    public class PullRangeObject : MonoBehaviour
    {
        /// <summary>
        /// 近くにウサギがいるときに表示するUI
        /// </summary>
        [SerializeField] private GameObject[] _pullUIObjects;
        /// <summary>
        /// 現在選択中の動物
        /// </summary>
        private RabbitBehaviour _currentSelectRabbit;
        /// <summary>
        /// ウサギを引っこ抜いているかどうか
        /// </summary>
        /// <returns></returns>
        private bool _isPulling;
        /// <summary>
        /// 現在選択中の動物
        /// </summary>
        public RabbitBehaviour CurrentSelectAnimal => _currentSelectRabbit;

        private void Awake()
        {
            _isPulling = false;
        }

        private void OnTriggerStay(Collider other)
        {
            //近くに埋まってるウサギがいるかどうかチェックする
            if (other.TryGetComponent<ISelected>(out var selectedAnimal))
            {
                _currentSelectRabbit = selectedAnimal.SelectAnimal();
                
                if (!_isPulling && _currentSelectRabbit != null)
                {
                    foreach (var pullUI in _pullUIObjects)
                    {
                        pullUI.SetActive(true);
                    }   
                }
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            //いなくなったらfalseにする
            //Exit()は判定を取られる側（ウサギ）が範囲外に出たのではなく、消えた（Destroy）場合処理が走らないので注意する
            //この場合_currentSelectRabbitがDestroyによってnullになるので正しく動作する
            if (other.TryGetComponent<ISelected>(out var selectedAnimal))
            {
                foreach (var pullUI in _pullUIObjects)
                {
                    pullUI.SetActive(false);
                }
                _currentSelectRabbit = null;
            }
        }

        /// <summary>
        /// UIを隠す処理
        /// </summary>
        public void HideUI()
        {
            foreach (var pullUI in _pullUIObjects)
            {
                pullUI.SetActive(false);
            }
        }

        /// <summary>
        /// ウサギを引っこ抜いているかどうかを切り替える処理
        /// </summary>
        public void ChangePulling(bool isPull)
        {
            _isPulling = isPull;
        }
    }
}
