using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// インターフェースをGetComponentする用のクラス
    /// </summary>
    public static class GetComponentInterface
    {
        /// <summary>
        /// 指定されたインターフェイスを実装したコンポーネントを持つオブジェクトを探す処理
        /// </summary>
        public static T FindObjectOfInterface<T>() where T : class
        {
            var components = Object.FindObjectsOfType<Component>();
            foreach (var n in components)
            {
                if (n is T component)
                {
                    return component;
                }
            }

            return null;
        }
    }
}
