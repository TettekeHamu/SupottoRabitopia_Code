using System;
using PullAnimals.StatePattern;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// ウサギ生成時のパーティクルを管理するクラス
    /// </summary>
    public class RabbitParticleController : MonoBehaviour
    {
        /// <summary>
        /// ウサギの生成時・成長時に出すパーティクル
        /// </summary>
        [SerializeField] private GameObject[] _createParticlesPrefabs;
        /// <summary>
        /// 消失時に出すパーティクル
        /// </summary>
        [SerializeField] private GameObject _destroyParticlePrefab;

        /// <summary>
        /// 指定した生成時のパーティクルを再生する処理
        /// </summary>
        /// <param name="state">ウサギの状態</param>
        public void PlayCreatingParticle(IState state)
        {
            if (state is SproutState)
            {
                Instantiate(_createParticlesPrefabs[0], transform.position, Quaternion.identity);
            }
            else if (state is GrowingState)
            {
                Instantiate(_createParticlesPrefabs[1], transform.position, Quaternion.identity);
            }
            else if(state is BloomState)
            {
                Instantiate(_createParticlesPrefabs[2], transform.position, Quaternion.identity);
            }
        }

        /// <summary>
        /// 消失時のパーティクルを生成する処理
        /// </summary>
        public void PlayDestroyParticle()
        {
            Instantiate(_destroyParticlePrefab, transform.position, Quaternion.identity);
        }
    }
}
