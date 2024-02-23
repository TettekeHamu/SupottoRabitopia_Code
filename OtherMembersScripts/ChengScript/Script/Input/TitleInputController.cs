using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using PullAnimals.Singleton;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PullAnimals
{
 /// <summary>
 /// タイトルの入力を管理するクラス
 /// </summary>
    public class TitleInputController : MonoSingletonBase<TitleInputController>
    {
        /// <summary>
        /// InputSystemを使用するためのコンポーネント
        /// </summary>
        [SerializeField] private PlayerInput _playerInput;

        /// <summary>
        /// ゲームを開始させる入力
        /// </summary>
        /// <returns></returns>
        public bool GetChangeSceneKeyDown()
        {
            //joyconと接続中ならJoycon、なければInputSystemの入力を返す
            if(JoyconInputController.Instance.IsConnectingJoycon)
            {
                return JoyconInputController.Instance.IsZrKey && (JoyconInputController.Instance.GetJoyconSwingSpeed() >= 4);
            }
            else
            {
                return _playerInput.actions["ChangeScene"].WasPressedThisFrame();
            }
        }
        
        /// <summary>
        /// チームメンバーのクレジット表記
        /// </summary>
        /// <returns></returns>
        public bool GetChangeMembersPanelKeyDown()
        {
            //joyconと接続中ならJoycon、なければInputSystemの入力を返す
            if(JoyconInputController.Instance.IsConnectingJoycon)
            {
                return JoyconInputController.Instance.IsUpKeyDown;
            }
            else
            {
                return _playerInput.actions["ShowMembers"].WasPressedThisFrame();
            }
        }
        
        /// <summary>
        /// リソースのクレジット表記
        /// </summary>
        /// <returns></returns>
        public bool GetChangeResourcePanelKeyDown()
        {
            //joyconと接続中ならJoycon、なければInputSystemの入力を返す
            if(JoyconInputController.Instance.IsConnectingJoycon)
            {
                return JoyconInputController.Instance.IsRightKeyDown;
            }
            else
            {
                return _playerInput.actions["ShowResource"].WasPressedThisFrame();
            }
        }
        
        /// <summary>
        /// コントローラを揺らす処理
        /// </summary>
        /// <param name="padStrength">パッドを揺らす強さ</param>
        /// <param name="padTime">パッドを揺らす時間</param>
        /// <param name="joyconStrength">Joyconを揺らす強さ</param>
        /// <param name="joyconTime">Joyconを揺らす時間(msなので注意)</param>
        /// <param name="token"></param>
        public async UniTaskVoid AsyncShakeController(float padStrength, float padTime, float joyconStrength, int joyconTime, CancellationToken token)
        {
            //Joycon用の振動
            if (JoyconInputController.Instance.IsConnectingJoycon)
            {
                JoyconInputController.Instance.ShakeJoycon(joyconStrength, joyconTime);
                return;
            }
            
            //ゲームパッド用の振動
            var gamepad = Gamepad.current;
            if (gamepad != null)
            {
                gamepad.SetMotorSpeeds(padStrength, padStrength);
                await UniTask.Delay(TimeSpan.FromSeconds(padTime), cancellationToken: token);
                gamepad.SetMotorSpeeds(0.0f, 0.0f);   
            }
        }
    }
}


