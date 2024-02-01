using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// アニメーションの長さと再生倍率をまとめたScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "AnimationData", menuName = "ScriptableObjects/AnimationData")]
    public class AnimationData : ScriptableObject
    {
        /// <summary>
        /// アニメーションのフレーム数
        /// </summary>
        [Header("アニメーションのフレーム数")]
        [SerializeField] private int _flameCount;
        /// <summary>
        /// アニメーションのサンプルフレーム数
        /// </summary>
        [Header("アニメーションのサンプルフレーム数")]
        [SerializeField] private int _sampleFlameCount;
        /// <summary>
        /// アニメーションの再生速度
        /// </summary>
        [Header("アニメーションの再生速度")]
        [SerializeField] private float _playbackSpeed;
        /// <summary>
        /// アニメーションの長さ
        /// </summary>
        public float AnimationLength => (float)_flameCount / _sampleFlameCount * _playbackSpeed;
    }
}
