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
        
        ///共通
        /// <summary>
        /// ゲームを開始させる入力
        /// </summary>
        public bool ChangeSceneKeyDown => _playerInput.actions["ChangeScene"].WasPressedThisFrame();
        
        ///タイトル
        /// <summary>
        /// チームメンバーのクレジット表記
        /// </summary>
        public bool ChangeMembersPanelKeyDown => _playerInput.actions["ShowMembers"].WasPressedThisFrame();

        ///タイトル
        /// <summary>
        /// リソースのクレジット表記
        /// </summary>
        public bool ChangeResourcePanelKeyDown => _playerInput.actions["ShowResource"].WasPressedThisFrame();

        ///メインシーン
        /// <summary>
        /// 移動に関する入力
        /// </summary>
        public Vector2 MoveInputVector2 => _playerInput.actions["PlayerMove"].ReadValue<Vector2>();
        /// <summary>
        /// 走るに関する入力
        /// </summary>
        public bool WalkKey => _playerInput.actions["PlayerWalk"].IsPressed();
        /// <summary>
        /// ジャンプを開始する入力
        /// </summary>
        ///public bool JumpKeyDown => _playerInput.actions["PlayerJump"].WasPressedThisFrame();
        /// <summary>
        /// ジャンプをやめる入力
        /// </summary>
        ///public bool JumpKeyUp => _playerInput.actions["PlayerJump"].WasReleasedThisFrame();
        /// <summary>
        /// 動物を引っこ抜く入力
        /// </summary>
        public bool HandActionKeyDown => _playerInput.actions["PlayerHandAction"].WasPressedThisFrame();
        /// <summary>
        /// ポーズ中かどうかを切り替える入力
        /// </summary>
        public bool ChangeGameModeKeyDown => _playerInput.actions["ChangeGameMode"].WasPressedThisFrame();
        
        /// <summary>
        /// ゲームパッドを揺らす処理
        /// </summary>
        /// <param name="strength">振動の強さ</param>
        /// <param name="time">振動の時間</param>
        /// <param name="token"></param>
        public async UniTaskVoid AsyncShakeGamePad(float strength, float time, CancellationToken token)
        {
            var gamepad = Gamepad.current;
            if (gamepad == null) return;

            gamepad.SetMotorSpeeds(strength, strength);
            await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: token);
            gamepad.SetMotorSpeeds(0.0f, 0.0f);
        }
    }
}