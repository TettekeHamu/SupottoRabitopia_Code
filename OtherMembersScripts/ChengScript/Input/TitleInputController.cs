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
                return JoyconInputController.Instance.IsZrKeyDown;
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
    }
}


