using System;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// プレイヤーが動物を引っこ抜ける範囲を管理するクラス
    /// </summary>
    public class PullRangeObject : MonoBehaviour
    {
        /// <summary>
        /// 現在選択中の動物
        /// </summary>
        [SerializeField]
        private RabbitBehaviour _currentSelectRabbit;
        /// <summary>
        /// 現在選択中の動物
        /// </summary>
        public RabbitBehaviour CurrentSelectAnimal => _currentSelectRabbit;

        private void OnTriggerStay(Collider other)
        {
            //近くに埋まってるウサギがいるかどうかチェックする
            if (other.TryGetComponent<ISelected>(out var selectedAnimal))
            {
                _currentSelectRabbit = selectedAnimal.SelectAnimal();
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            //いなくなったらfalseにする
            //Exit()は判定を取られる側（ウサギ）が範囲外に出たのではなく、消えた（Destroy）場合処理が走らないので注意する
            //この場合_currentSelectRabbitがDestroyによってnullになるので正しく動作する
            if (other.TryGetComponent<ISelected>(out var selectedAnimal))
            {
                _currentSelectRabbit = null;
            }
        }
    }
}
