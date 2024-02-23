using System;
using System.Collections.Generic;
using PullAnimals.Singleton;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// joyconからの入力を受け取るクラス
    /// </summary>
    public class JoyconInputController : DDMonoSingletonBase<JoyconInputController>
    {
        /// <summary>
        /// Joycon.ButtonのEnumを配列に変換したもの
        /// </summary>
        private static readonly Joycon.Button[] Buttons = Enum.GetValues( typeof( Joycon.Button ) ) as Joycon.Button[];
        /// <summary>
        /// 接続中のjoyconを格納したList
        /// </summary>
        private List<Joycon>　_joyconList = new List<Joycon>();
        /// <summary>
        /// 右側のjoycon
        /// </summary>
        private Joycon　_joyconR;
        /// <summary>
        /// 左側のjoycon
        /// </summary>
        private Joycon _joyconL;
        /// <summary>
        /// Joyconと接続中かどうかを返すbool値
        /// </summary>
        public bool IsConnectingJoycon => !(_joyconList == null || _joyconList.Count <= 0);
        /// <summary>
        /// +-ボタンの入力を返す
        /// </summary>
        public bool IsPlusButtonKeyDown => (_joyconR?.GetButtonDown(Joycon.Button.PLUS)).GetValueOrDefault() || (_joyconL?.GetButtonDown(Joycon.Button.MINUS)).GetValueOrDefault();
        /// <summary>
        /// Zrの入力を返す
        /// </summary>
        public bool IsZrKeyDown => (_joyconR?.GetButtonDown(Joycon.Button.SHOULDER_2)).GetValueOrDefault() || (_joyconL?.GetButtonDown(Joycon.Button.SHOULDER_2)).GetValueOrDefault();
        /// <summary>
        /// Zrの押しっぱなしの入力を返す
        /// </summary>
        public bool IsZrKey => (_joyconR?.GetButton(Joycon.Button.SHOULDER_2)).GetValueOrDefault() || (_joyconL?.GetButton(Joycon.Button.SHOULDER_2)).GetValueOrDefault();
        /// <summary>
        /// 上ボタンの入力を返す
        /// </summary>
        public bool IsUpKeyDown => (_joyconR?.GetButtonDown(Joycon.Button.DPAD_UP)).GetValueOrDefault() || (_joyconL?.GetButtonDown(Joycon.Button.DPAD_UP)).GetValueOrDefault();
        /// <summary>
        /// 上ボタンの押しっぱなしの入力を返す
        /// </summary>
        public bool IsUpKey =>(_joyconR?.GetButton(Joycon.Button.DPAD_UP)).GetValueOrDefault() || (_joyconL?.GetButton(Joycon.Button.DPAD_UP)).GetValueOrDefault();
        /// <summary>
        /// 右ボタンの入力を返す
        /// </summary>
        public bool IsRightKeyDown => (_joyconR?.GetButtonDown(Joycon.Button.DPAD_RIGHT)).GetValueOrDefault() || (_joyconL?.GetButtonDown(Joycon.Button.DPAD_RIGHT)).GetValueOrDefault();
        /// <summary>
        /// 右ボタンの押しっぱなしの入力を返す
        /// </summary>
        public bool IsRightKey => (_joyconR?.GetButton(Joycon.Button.DPAD_RIGHT)).GetValueOrDefault() || (_joyconL?.GetButton(Joycon.Button.DPAD_RIGHT)).GetValueOrDefault();
        /// <summary>
        /// 下ボタンの入力を返す
        /// </summary>
        public bool IsDownKeyDown => (_joyconR?.GetButtonDown(Joycon.Button.DPAD_DOWN)).GetValueOrDefault() || (_joyconL?.GetButtonDown(Joycon.Button.DPAD_DOWN)).GetValueOrDefault();
        /// <summary>
        /// 左ボタンの入力を返す
        /// </summary>
        public bool IsLeftKeyDown => (_joyconR?.GetButtonDown(Joycon.Button.DPAD_LEFT)).GetValueOrDefault() || (_joyconL?.GetButtonDown(Joycon.Button.DPAD_LEFT)).GetValueOrDefault();
        /// <summary>
        /// Rの入力を返す(押しっぱなしなので注意！！)
        /// </summary>
        public bool IsRKey => (_joyconR?.GetButton(Joycon.Button.SHOULDER_1)).GetValueOrDefault() || (_joyconL?.GetButton(Joycon.Button.SHOULDER_1)).GetValueOrDefault();

        private void Start()
        {
            //joyconを取得
            //Awake()だとうまくいかない時があるので注意する
            _joyconList = JoyconManager.Instance.j;

            //joyconが1つもなければreturn
            if (_joyconList == null || _joyconList.Count <= 0)
            {
                Debug.LogWarning("Joyconとの接続がありません！！");
                return;
            }
            
            //右と左に割り当てる
            _joyconR = _joyconList.Find( c => !c.isLeft );
            _joyconL = _joyconList.Find(c => c.isLeft);
        }
        

        /// <summary>
        /// Joyconを揺らす処理
        /// </summary>
        /// <param name="strength">揺らす強さ</param>
        /// <param name="time">Joyconを揺らす時間(msなので注意)</param>
        public void ShakeJoycon(float strength, int time)
        {
            _joyconR?.SetRumble(160, 320, strength, time);
            _joyconL?.SetRumble(160, 320, strength, time);
        }

        /// <summary>
        /// 移動の入力を返す処理
        /// </summary>
        /// <returns></returns>
        public Vector2 GetMoveInputVector2()
        {
            //左右のJoyconのうち大きい入力の方をとる
            var rVec = new Vector2((_joyconR?.GetStick()[0]).GetValueOrDefault(), (_joyconR?.GetStick()[1]).GetValueOrDefault());
            var lVec = new Vector2((_joyconL?.GetStick()[0]).GetValueOrDefault(), (_joyconL?.GetStick()[1]).GetValueOrDefault());
            if (rVec.magnitude >= lVec.magnitude)
            {
                return rVec;
            }
            else
            {
                return lVec;
            }
        }

        /// <summary>
        /// Joyconの振るスピードを返す処理
        /// </summary>
        /// <returns></returns>
        public float GetJoyconSwingSpeed()
        {
            /*
            //左右のJoyconのうち大きい入力の方をとる
            //静止中はだいたい1.0fになる
            if (_joyconR?.GetAccel().magnitude >= _joyconL?.GetAccel().magnitude)
            {
                return (_joyconR?.GetAccel().magnitude).GetValueOrDefault();
            }
            else
            {
                return (_joyconL?.GetAccel().magnitude).GetValueOrDefault();
            }
            */
            
            //静止中はだいたい1.0fになる
            //右側のJoyconを採用する（左手はダミー）
            //左手のみ接続時は左側のJoyconを採用
            if (_joyconR != null)
            {
                return (_joyconR?.GetAccel().magnitude).GetValueOrDefault();   
            }
            else
            {
                return (_joyconL?.GetAccel().magnitude).GetValueOrDefault();  
            }
        }
    }
}
