using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PullAnimals
{
    /// <summary>
    /// カメラの管理をおこなうクラス
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        /// <summary>
        /// トップダウン用のカメラ
        /// </summary>
        [SerializeField] private CinemachineVirtualCamera _topdownCamera;
        /// <summary>
        /// プレイヤーを凝視するカメラ
        /// </summary>
        [SerializeField] private CinemachineVirtualCamera _playerLookCamera;

        private void Awake()
        {
            //初期設定
            _topdownCamera.Priority = 1;
            _playerLookCamera.Priority = -1;
        }

        /// <summary>
        /// カメラを切り替える処理
        /// </summary>
        public void ChangeCamera()
        {
            _topdownCamera.Priority *= -1;
            _playerLookCamera.Priority *= -1;
        }
    }
}
