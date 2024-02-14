using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// joyconからの入力のテスト用クラス
    /// </summary>
    public class JoyconInputSample : MonoBehaviour
    {
        [SerializeField] private float _highFreq;
        [SerializeField] private float _lowFreq;
        [SerializeField] private float _amp;
        [SerializeField] private int _time;
        
        [SerializeField] private float _accelNum;
        /// <summary>
        /// Joycon.ButtonのEnumを配列に変換したもの
        /// </summary>
        private static readonly Joycon.Button[] Buttons = Enum.GetValues( typeof( Joycon.Button ) ) as Joycon.Button[];
        /// <summary>
        /// 接続中のjoyconを格納したList
        /// </summary>
        private List<Joycon>　_joyconList;
        /// <summary>
        /// 左側のjoycon
        /// </summary>
        private Joycon　_joyconL = null;
        /// <summary>
        /// 右側のjoycon
        /// </summary>
        private Joycon　_joyconR = null;
        /// <summary>
        /// 左側のjoyconで押されているボタン
        /// </summary>
        private Joycon.Button?　_pressedButtonL;
        /// <summary>
        /// 右側のjoyconで押されているボタン
        /// </summary>
        private Joycon.Button?　_pressedButtonR;

        private void Start()
        {
            //joyconを取得
            _joyconList = JoyconManager.Instance.j;

            //joyconが1つもなければreturn
            if ( _joyconList == null || _joyconList.Count <= 0 ) return;

            //左と右に割り振る
            _joyconL = _joyconList.Find( c =>  c.isLeft );
            _joyconR = _joyconList.Find( c => !c.isLeft );

            var result = SwingJoyconTest();
        }

        private void Update()
        {
            //とりあえず押されてないことに
            _pressedButtonL = null;
            _pressedButtonR = null;

            //joyconが1つもなければreturn
            if ( _joyconList == null || _joyconList.Count <= 0 ) return;

            //押されているボタンを設定
            foreach (var button in Buttons)
            {
                if (_joyconL != null)
                {
                    if (_joyconL.GetButton(button)) _pressedButtonL = button;
                }
                if (_joyconR != null)
                {
                    if (_joyconR.GetButton(button)) _pressedButtonR = button;
                }
            }

            //振動機能のテスト
            if ( Input.GetKeyDown( KeyCode.Z ) )
            {
                _joyconL?.SetRumble( 160, 320, 0.6f, 200 );
            }
            if ( Input.GetKeyDown( KeyCode.X ) )
            {
                _joyconR?.SetRumble( _lowFreq, _highFreq, _amp, _time );
            }
            
            //AccelTest();
        }

        /// <summary>
        /// デバッグを画面に表示
        /// </summary>
        private void OnGUI()
        {
            var style = GUI.skin.GetStyle( "label" );
            style.fontSize = 24;

            if ( _joyconList == null || _joyconList.Count <= 0 )
            {
                GUILayout.Label( "Joy-Con が接続されていません" );
            }

            if (_joyconList.All( c => c.isLeft ))
            {
                GUILayout.Label( "Joy-Con (L) が接続されていません" );
            }

            if (_joyconList.All(c => c.isLeft))
            {
                GUILayout.Label( "Joy-Con (R) が接続されていません" );
            }

            GUILayout.BeginHorizontal( GUILayout.Width( 960 ) );

            foreach ( var joycon in _joyconList )
            {
                var isLeft      = joycon.isLeft;
                var name        = isLeft ? "Joy-Con (L)" : "Joy-Con (R)";
                var key         = isLeft ? "Z キー" : "X キー";
                var button      = isLeft ? _pressedButtonL : _pressedButtonR;
                var stick       = joycon.GetStick();
                var gyro        = joycon.GetGyro();
                var accel       = joycon.GetAccel();
                var orientation = joycon.GetVector();

                GUILayout.BeginVertical( GUILayout.Width( 480 ) );
                GUILayout.Label( name );
                GUILayout.Label( key + "：振動" );
                GUILayout.Label( "押されているボタン：" + button );
                GUILayout.Label($"スティック：({stick[0]}, {stick[1]})");
                GUILayout.Label( "ジャイロ：" + gyro );
                GUILayout.Label( "加速度：" + accel );
                GUILayout.Label("加速度ベクトルの大きさ：" + accel.magnitude);
                GUILayout.Label( "傾き：" + orientation );
                GUILayout.EndVertical();
            }

            GUILayout.EndHorizontal();
        }

        private void AccelTest()
        {
             var accel = _joyconR.GetAccel();

             if (Mathf.Abs(accel.x) > _accelNum)
             {
                 Debug.Log("X方向に振られました!!");
             }
             if (Mathf.Abs(accel.y) > _accelNum)
             {
                 Debug.Log("Y方向に振られました!!");
             }
             if (Mathf.Abs(accel.z) > _accelNum)
             {
                 Debug.Log("Z方向に振られました!!");
             }
        }

        private async UniTask<bool> SwingJoyconTest()
        {
            Debug.Log("計測開始！");
            //振った回数
            var count = 0;
            //振っているスピード
            var speed = _joyconR.GetAccel().magnitude;
            while (count < 10)
            {
                //振っているスピードが一定基準を超えるまで待機
                while (speed < 5f)
                {
                    await UniTask.Yield();
                    speed = _joyconR.GetAccel().magnitude;
                };

                //カウントを増加
                Debug.Log("振りました！！");
                count++;
                
                //振るスピードが一定以下になるまで待つ
                //連続で振ったことにならないようにするため
                while (speed > 2f)
                {
                    await UniTask.Yield();
                    speed = _joyconR.GetAccel().magnitude;
                }
            }

            //振り終えたらtrueを返す
            Debug.Log("引っこ抜き完了！！");
            return true;
        }
    }
}
