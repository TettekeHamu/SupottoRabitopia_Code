using System;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PullAnimals
{
    /// <summary>
    /// フィールド外にいるウサギの描画をおこなうクラス
    /// </summary>
    public class RabbitRenderController : MonoBehaviour
    {
        /// <summary>
        /// ウサギのマテリアル
        /// </summary>
        [SerializeField] private Material _material;
        /// <summary>
        /// ウサギのメッシュ
        /// </summary>
        [SerializeField] private Mesh _mesh;
        /// <summary>
        /// 描画するウサギ軍団（数匹で１つ）の数
        /// </summary>
        [SerializeField] private int _rabbitsNum;
        /// <summary>
        /// 経過時間
        /// </summary>
        private float _timer;
        /// <summary>
        /// 描画するY座標
        /// </summary>
        private float _posY;
        /// <summary>
        /// 加算するY座標
        /// </summary>
        private float _addPosY;
        /// <summary>
        /// ジャンプしてるかどうかのフラグ
        /// </summary>
        private bool _isJumping;
        /// <summary>
        /// ウサギの生成位置
        /// </summary>
        private Vector2[] _randomPos;

        private void Awake()
        {
            _timer = 0;
            _posY = 1.48f;
            _addPosY = 0;
            _isJumping = false;
            _randomPos = new Vector2[_rabbitsNum];
            
            for (var i = 0; i < _randomPos.Length; ++i)
            {
                //上側
                if (i % 4 == 0) _randomPos[i] = new Vector2(Random.Range(-25.5f, 25.5f), Random.Range(32.5f, 37.5f));
                //右側
                else if(i % 4 == 1)　_randomPos[i] = new Vector2(Random.Range(23f, 28f), Random.Range(-12.5f, 32.5f));
                //下側
                else if(i % 4 == 2) _randomPos[i] = new Vector2(Random.Range(-25.5f, 25.5f), Random.Range(-7.5f, -12.5f));
                //左側
                else _randomPos[i] = new Vector2(Random.Range(-23f, -28f), Random.Range(-12.5f, 32.5f));
            }
        }

        private void Start()
        {
            //Awakeだと実行順でバグるのでStartでおこなう
            BeatController.Instance.OnRhythmObservable
                .Subscribe(_ => StartJumping())
                .AddTo(this);
        }

        private void Update()
        {
            //ジャンプ運動させる処理
            if (_isJumping)
            {
                _timer += Time.deltaTime;
                _addPosY = Mathf.Sin(Mathf.PI * _timer * 2f) * 2f;
                if (_addPosY <= 0)
                {
                    _isJumping = false;
                }
            }
            else
            {
                _addPosY = 0;
            }
            
            //描画に使う行列の配列を生成
            var instData = new Matrix4x4[_rabbitsNum * 3];
            //Scaleを決定
            var sc = new Vector3(3.6f, 3.6f, 3.6f);
            //マテリアルを決定
            var rp = new RenderParams(_material);
            //ウサギの数分繰り返す
            for (var i = 0; i < _rabbitsNum; i++)
            {
                for (var j = -1; j < 2; ++j)
                {
                    //座標を決定
                    var f = j / 2f;
                    var pos = new Vector3(Mathf.Sin(f * Mathf.PI) * 1.5f + _randomPos[i].x, _posY + _addPosY, Mathf.Cos(f * Mathf.PI) *1.5f  + _randomPos[i].y);
                    //向きを決定
                    var centerPos = new Vector3(0, 0, 10);
                    var vec = centerPos - pos;
                    var lookRotation = Quaternion.LookRotation(vec, Vector3.up);
                    //var qua = Quaternion.Euler(new Vector3(0, 180, 0));
                    //行列に変換
                    instData[i] = Matrix4x4.TRS(pos, lookRotation, sc);
                    //描画処理
                    Graphics.RenderMeshInstanced(rp, _mesh, 0, instData);   
                }
            }
        }

        /// <summary>
        /// ジャンプを開始させる処理
        /// </summary>
        private void StartJumping()
        {
            //ジャンプ中に変更
            _isJumping = true;
            _timer = 0;
        }
    }
}
