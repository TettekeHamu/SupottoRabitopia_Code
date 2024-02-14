using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using PullAnimals.Singleton;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PullAnimals
{
    /// <summary>
    /// 入力を管理するシングルトンクラス
    /// </summary>
    public class MyInputController : MonoSingletonBase<MyInputController>
    {
        /// <summary>
        /// InputSystemを使用するためのコンポーネント
        /// </summary>
        [SerializeField] private PlayerInput _playerInput;
        
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

        /// <summary>
        /// シーンを切り替える入力
        /// </summary>
        /// <returns></returns>
        public bool GetChangeSceneKeyDown()
        {
            //joyconと接続中ならJoycon、なければInputSystemの入力を返す
            if(JoyconInputController.Instance.IsConnectingJoycon)
            {
                //ボタン全押しにする
                return JoyconInputController.Instance.IsUpKeyDown && JoyconInputController.Instance.IsRightKeyDown;
            }
            else
            {
                return _playerInput.actions["ChangeScene"].WasPressedThisFrame();
            }
        }

        /// <summary>
        /// チュートリアルをスキップする入力
        /// </summary>
        /// <returns></returns>
        public bool GetSkipTutorialKeyDown()
        {
            //joyconと接続中ならJoycon、なければInputSystemの入力を返す
            if(JoyconInputController.Instance.IsConnectingJoycon)
            {
                //ボタン全押しにする
                return JoyconInputController.Instance.IsUpKeyDown && JoyconInputController.Instance.IsRightKeyDown;
            }
            else
            {
                return _playerInput.actions["SkipTutorial"].WasPressedThisFrame();
            }
        }

        /// <summary>
        /// 移動に関する入力を返す処理
        /// </summary>
        public Vector2 GetMoveInputVector2()
        {
            //joyconと接続中ならJoycon、なければInputSystemの入力を返す
            if(JoyconInputController.Instance.IsConnectingJoycon)
            {
                return JoyconInputController.Instance.GetMoveInputVector2();
            }
            else
            {
                return _playerInput.actions["PlayerMove"].ReadValue<Vector2>(); 
            }
        }

        /// <summary>
        /// ウサギを引っこ抜く入力を返す処理
        /// </summary>
        /// <returns></returns>
        public bool GetHandActionKeyDown()
        {
            //joyconと接続中ならJoycon、なければInputSystemの入力を返す
            if(JoyconInputController.Instance.IsConnectingJoycon)
            {
                return JoyconInputController.Instance.IsZrKeyDown;
            }
            else
            {
                return _playerInput.actions["PlayerHandAction"].WasPressedThisFrame();
            }
        }

        /// <summary>
        /// 歩くかどうかの入力を返す処理
        /// </summary>
        /// <returns></returns>
        public bool GetWalkKey()
        {
            //joyconと接続中ならJoycon、なければInputSystemの入力を返す
            if(JoyconInputController.Instance.IsConnectingJoycon)
            {
                return JoyconInputController.Instance.IsRKey;
            }
            else
            {
                return _playerInput.actions["PlayerWalk"].WasPressedThisFrame();
            }
        }

        /// <summary>
        /// チュートリアルの会話を進める入力を返す処理
        /// </summary>
        /// <returns></returns>
        public bool GetCarryStoryKeyDown()
        {
            //joyconと接続中ならJoycon、なければInputSystemの入力を返す
            if(JoyconInputController.Instance.IsConnectingJoycon)
            {
                return JoyconInputController.Instance.IsZrKeyDown;
            }
            else
            {
                return _playerInput.actions["CarryStory"].WasPressedThisFrame();
            }
        }

        public bool GetChangeGameModeKeyDown()
        {
            //joyconと接続中ならJoycon、なければInputSystemの入力を返す
            if(JoyconInputController.Instance.IsConnectingJoycon)
            {
                return JoyconInputController.Instance.IsPlusButtonKeyDown;
            }
            else
            {
                return _playerInput.actions["ChangeGameMode"].WasPressedThisFrame();
            }
        }

        /// <summary>
        /// Joyconを振ってるスピードを返す処理
        /// </summary>
        public float GetJoyconSwingSpeed()
        {
            return JoyconInputController.Instance.GetJoyconSwingSpeed();
        }
    }
}