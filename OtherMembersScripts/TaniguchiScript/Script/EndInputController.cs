using UnityEngine;
using PullAnimals.Singleton;
using UnityEngine.InputSystem;

namespace PullAnimals
{
    public class EndInputController : MonoSingletonBase<EndInputController>
    {
        /// <summary>
        /// InputSystemを使用するためのコンポーネント
        /// </summary>
        [SerializeField] private PlayerInput _playerInput;

        /// <summary>
        /// タイトルに戻る入力
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

        public bool GetChangeTextKeyDown()
        {
            //joyconと接続中ならJoycon、なければInputSystemの入力を返す
            if(JoyconInputController.Instance.IsConnectingJoycon)
            {
                return JoyconInputController.Instance.IsPlusButtonKeyDown;
            }
            else
            {
                return _playerInput.actions["ChangeText"].WasPressedThisFrame();
            }
        }
    }
}

