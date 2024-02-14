using System.Collections;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// プレイヤーのフィーバーを管理するクラス
    /// </summary>
    public class PlayerFeverController : MonoBehaviour
    {
        /// <summary>
        /// フィーバ中に画面演出をおこなうクラス
        /// </summary>
        [SerializeField] private FeverModeController _modeController;
        /// <summary>
        /// フィーバー中の時間
        /// </summary>
        [SerializeField] private float _feverTime;
        /// <summary>
        /// フィーバーを始めるのに必要な回数
        /// </summary>
        [SerializeField] private int _maxFiverCount;
        /// <summary>
        /// フィーバー中かどうかのフラグ
        /// </summary>
        private bool _isFever;
        /// <summary>
        /// 完全体のウサギを連続で引っこ抜いた回数
        /// </summary>
        private int _feverCount;
        /// <summary>
        /// フィーバー開始からの経過時間
        /// </summary>
        private float _timer;
        /// <summary>
        /// フィーバー中かどうかのフラグ
        /// </summary>
        public bool IsFever => _isFever;
        /// <summary>
        /// フィーバー中の時間
        /// </summary>
        public float FeverTime => _feverTime;

        private void Awake()
        {
            _isFever = false;
            _timer = 0;
        }

        /// <summary>
        /// フィーバー中はずっと呼ぶ処理
        /// </summary>
        public void FeverUpDate()
        {
            if(!_isFever) return;
            if (_timer > _feverTime)
            {
                _isFever = false;
                _modeController.StopFever(this);
            }
            _timer += Time.deltaTime;
        }

        /// <summary>
        /// フィーバーカウントを加算する処理
        /// </summary>
        /// <returns>フィーバー開始ならtrueを返す</returns>
        public bool AddFeverCount()
        {
            _feverCount++;
            if (_feverCount >= _maxFiverCount)
            {
                _feverCount = 0;
                _timer = 0;
                _isFever = true;
                _modeController.StartFever(this);
                return true;
            }

            return false;
        }

        /// <summary>
        /// フィーバーカウントを0に戻す処理
        /// </summary>
        public void ResetFeverCount()
        {
            _feverCount = 0;
        }
    }
}
