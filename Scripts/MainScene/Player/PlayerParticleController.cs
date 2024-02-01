using System.Collections;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// プレイヤーのパーティクルを管理するクラス
    /// </summary>
    public class PlayerParticleController : MonoBehaviour
    {
        /// <summary>
        /// 走るときのパーティクル
        /// </summary>
        [SerializeField] private ParticleSystem _dashParticle;
        /// <summary>
        /// 引っこ抜いたときに出すパーティクル
        /// </summary>
        [SerializeField] private GameObject _dustParticleObjectPrefab;
        /// <summary>
        /// フィーバー中に出すパーティクル
        /// </summary>
        [SerializeField] private GameObject _feverParticlePrefab;

        /// <summary>
        /// 走った時に再生するパーティクルを再生する処理
        /// </summary>
        public void PlayDashParticle()
        {
            var dashParticle = Instantiate(_dashParticle, transform.position + new Vector3(0, 0.4f, 0), Quaternion.identity);
            dashParticle.Play();
        }

        /// <summary>
        /// 引っこ抜いたパーティクルを生成する処理
        /// </summary>
        /// <param name="rabbitPos">生成する位置</param>
        public void PlayDustParticle(Vector3 rabbitPos)
        {
            Instantiate(_dustParticleObjectPrefab, rabbitPos, Quaternion.identity);
        }

        /// <summary>
        /// フィーバー中のパーティクルを生成する処理
        /// </summary>
        public void PlayFeverParticle(float timer)
        {
            var feverParticleObj = Instantiate(_feverParticlePrefab, transform);
            Destroy(feverParticleObj, timer);
        }
    }
}
