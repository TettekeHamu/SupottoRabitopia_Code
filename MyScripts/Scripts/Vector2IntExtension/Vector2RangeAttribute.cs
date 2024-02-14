using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// Vector2Intに最小・最大値を設定する用のカスタムプロパティー属性を追加する用のクラス
    /// </summary>
    public class Vector2IntRangeAttribute : PropertyAttribute
    {
        /// <summary>
        /// x軸の最小値
        /// </summary>
        private readonly int _minX;
        /// <summary>
        /// x軸の最大値
        /// </summary>
        private readonly int _maxX;
        /// <summary>
        /// y軸の最小値
        /// </summary>
        private readonly int _minY;
        /// <summary>
        /// y軸の最大値
        /// </summary>
        private readonly int _maxY;

        /// <summary>
        /// x軸の最小値
        /// </summary>
        public int MinX => _minX;
        /// <summary>
        /// x軸の最大値
        /// </summary>
        public int MaxX => _maxX;
        /// <summary>
        /// y軸の最小値
        /// </summary>
        public int MinY => _minY;
        /// <summary>
        /// y軸の最大値
        /// </summary>
        public int MaxY => _maxY;
        
        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="minX">xの最小値</param>
        /// <param name="maxX">xの最大値</param>
        /// <param name="minY">yの最小値</param>
        /// <param name="maxY">yの最大値</param>
        public Vector2IntRangeAttribute(int minX, int maxX, int minY, int maxY)
        {
            _minX = minX;
            _maxX = maxX;
            _minY = minY;
            _maxY = maxY;
        }
    }
}
