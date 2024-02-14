using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// 動物のデータを管理するScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "RabbitStatusData", menuName = "ScriptableObjects/RabbitStatusData")]
    public class RabbitStatusData : ScriptableObject
    {
        /// <summary>
        /// 名前
        /// </summary>
        [SerializeField] private string _name;
        /// <summary>
        /// ウサギの種類（引っこ抜くための時間に影響）
        /// </summary>
        [SerializeField] private RabbitType _rabbitType;
        /// <summary>
        /// 引っこ抜いたときに入る点数
        /// </summary>
        [SerializeField] private int _score;
        /// <summary>
        /// ゲームパッドを揺らす強さ
        /// </summary>
        [SerializeField] private float _gamepadShakeStrength;
        /// <summary>
        /// ゲームパッドを揺らす時間
        /// </summary>
        [SerializeField] private float _gamepadShakeTime;
        /// <summary>
        /// Joyconを揺らす強さ
        /// </summary>
        [SerializeField] private float _joyconShakeStrength;
        /// <summary>
        /// Joyconを揺らす時間(msなので注意！！)
        /// </summary>
        [SerializeField] private int _joyconShakeTime;
        /// <summary>
        /// 引っこ抜くのに必要な連打回数
        /// </summary>
        [SerializeField] private int _mashCount;
        /// <summary>
        /// 引っこ抜くのに必要なJoyconを振る回数
        /// </summary>
        [SerializeField] private int _shakeCount;
        /// <summary>
        /// 名前
        /// </summary>
        public string Name => _name;
        /// <summary>
        /// ウサギの種類（引っこ抜くための時間に影響）
        /// </summary>
        public RabbitType RabbitType => _rabbitType;
        /// <summary>
        /// 引っこ抜いたときに入る点数
        /// </summary>
        public int Score => _score;
        /// <summary>
        /// ゲームパッドを揺らす強さ
        /// </summary>
        public float GamepadShakeStrength => _gamepadShakeStrength;
        /// <summary>
        /// ゲームパッドを揺らす時間
        /// </summary>
        public float GamepadShakeTime => _gamepadShakeTime;
        /// <summary>
        /// Joyconを揺らす強さ
        /// </summary>
        public float JoyconShakeStrength => _joyconShakeStrength;
        /// <summary>
        /// Joyconを揺らす時間(msなので注意！！)
        /// </summary>
        public int JoyconShakeTime => _joyconShakeTime;
        /// <summary>
        /// 引っこ抜くのに必要な連打回数
        /// </summary>
        public int MashCount => _mashCount;
        /// <summary>
        /// 引っこ抜くのに必要なJoyconを振る回数
        /// </summary>
        public int ShakeCount => _shakeCount;
    }
}
