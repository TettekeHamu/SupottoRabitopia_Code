using System;
using UniRx;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace PullAnimals
{
    public class PostProcessController : MonoBehaviour
    {
        /// <summary>
        /// GlobalVolumeコンポーネント
        /// </summary>
        [SerializeField] private Volume _volume;
        /// <summary>
        /// フィーバー中に変更させるパラメータ
        /// </summary>
        private ChromaticAberration _chromaticAberration;
        /// <summary>
        /// フィーバー中に変更させるパラメータ
        /// </summary>
        private LensDistortion _lensDistortion;

        private void Awake()
        {
            _volume.profile.TryGet(out _chromaticAberration);
            _volume.profile.TryGet(out _lensDistortion);
            _chromaticAberration.intensity.value = 0f;
            _lensDistortion.intensity.value = 0f;

            FeverModeController.OnStartFeverObservable
                .Subscribe(_ => ChangeFeverPostProcess())
                .AddTo(this);

            FeverModeController.OnStopFeverObservable
                .Subscribe(_ => StopFeverPostProcess())
                .AddTo(this);
        }

        /// <summary>
        /// フィーバー中のポストプロセスに変える処理
        /// </summary>
        private void ChangeFeverPostProcess()
        {
            _chromaticAberration.intensity.value = 1f;
            _lensDistortion.intensity.value = -0.2f;
        }

        /// <summary>
        /// フィーバ中のポストプロセスを止める処理
        /// </summary>
        private void StopFeverPostProcess()
        {
            _chromaticAberration.intensity.value = 0f;
            _lensDistortion.intensity.value = 0f;
        }
    }
}
