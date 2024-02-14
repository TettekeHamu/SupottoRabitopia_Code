using UnityEditor;
using UnityEngine;

namespace PullAnimals.EditorExtension
{
    /// <summary>
    /// Vector2Intの最小・最大値を設定する用のプロパティー属性を実際に描画するクラス
    /// </summary>
    [CustomPropertyDrawer(typeof(Vector2IntRangeAttribute))]
    public class Vector2IntRangeAttributeDrawer : PropertyDrawer
    {
        /// <summary>
        /// 描画する処理
        /// </summary>
        /// <param name="position">描画する場所</param>
        /// <param name="property">描画する値</param>
        /// <param name="label">描画するラベル</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginChangeCheck();
            var val = EditorGUI.Vector2IntField(position, label, property.vector2IntValue);
            if (EditorGUI.EndChangeCheck())
            {
                var rangeAttribute = (Vector2IntRangeAttribute)attribute;
                val.x = Mathf.Clamp(val.x, rangeAttribute.MinX, rangeAttribute.MaxX);
                val.y = Mathf.Clamp(val.y, rangeAttribute.MinY, rangeAttribute.MaxY);
                property.vector2IntValue = val;
            }
        }
    }
}
