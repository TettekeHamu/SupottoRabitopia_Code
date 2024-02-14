using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PullAnimals.EditorExtension
{
    /// <summary>
    /// Hierarchy上にツリー構造を表示するEditor拡張
    /// </summary>
    public static class TreeStructureEditorExtension
    {
        /// <summary>
        /// 画像のフォルダまでのパス
        /// </summary>
        private const string ImagePath = "Assets/@HosoiAssets/Editor/Images/";
        /// <summary>
        /// アイコンの幅
        /// </summary>
        private const int Width = 16;
        /// <summary>
        /// アイコンの水平のオフセット
        /// </summary>
        private const int StartOffsetX = -22;
        /// <summary>
        /// アイコンの垂直のオフセット
        /// </summary>
        private const int OffsetY = -14;
        /// <summary>
        /// Hierarchy上の灰色のカラーコード
        /// </summary>
        private static readonly Color Color = new Color32( 104, 104, 104, 255 );
        /// <summary>
        /// テクスチャのパスとそのテクスチャをキャッシュするためのDictionary
        /// </summary>
        private static readonly Dictionary<string, Texture> TextureTable = new Dictionary<string, Texture>();

        /// <summary>
        /// UnityEditorが起動した際に処理するようにする
        /// </summary>
        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            EditorApplication.hierarchyWindowItemOnGUI += DrawTreeStructureOnHierarchy;
        }

        /// <summary>
        /// Hierarchy場にツリー構造を描画する処理
        /// </summary>
        /// <param name="instanceID">GameObjectに割り当てられたID</param>
        /// <param name="selectionRect">HierarchyのGameObjectの矩形</param>
        private static void DrawTreeStructureOnHierarchy(int instanceID, Rect selectionRect)
        {
            //instanceIdからGameObjectを取得
            var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (gameObject == null) return;

            //親オブジェクトを取得
            var parent = gameObject.transform.parent;
            if (parent == null) return;

            //HierarchyのGameObjectの矩形
            var rectPosition = selectionRect;

            //描画する位置を決定
            rectPosition.width =  Width;
            rectPosition.x += StartOffsetX;

            //色を決定
            var oldColor = GUI.color;
            GUI.color = Color;

            //親オブジェクトの最後の子オブジェクトのとき、または親オブジェクトに子が1つしかないとき
            if (parent.childCount == 1 || parent.GetChild( parent.childCount - 1) == gameObject.transform )
            {
                DrawTexture(rectPosition, ImagePath + "HierarchyLine02.png");
            }
            //それ以外の時
            else
            {
                DrawTexture(rectPosition,  ImagePath + "HierarchyLine01.png");
            }

            //親が存在するとき
            while (parent != null)
            {
                var parentParent = parent.parent;
                if ( parentParent == null ) break;

                if (parent == parentParent.GetChild( parentParent.childCount - 1))
                {
                    parent     =  parentParent;
                    rectPosition.x += OffsetY;
                    continue;
                }

                rectPosition.x += OffsetY;
                DrawTexture(rectPosition,  ImagePath + "HierarchyLine03.png");
                parent = parentParent;
            }

            //色を元に戻す
            GUI.color = oldColor;
        }
        
        /// <summary>
        /// 画像を描画する処理
        /// </summary>
        /// <param name="position">描画する位置</param>
        /// <param name="path">画像のパス</param>
        private static void DrawTexture(Rect position, string path)
        {
            var image = LoadTexture(path);
            GUI.DrawTexture(position, image, ScaleMode.ScaleToFit);
        }

        /// <summary>
        /// 指定された画像を読み込む処理
        /// </summary>
        /// <param name="path">画像のパス</param>
        /// <returns>読み込んだ画像</returns>
        private static Texture LoadTexture(string path)
        {
            //キャッシュされていた場合はそれを返す
            if (TextureTable.TryGetValue(path, out var result )) return result;
            //されていなかった場合はDictionaryに追加してそれを返す
            var texture = (Texture) EditorGUIUtility.Load(path);
            TextureTable[path] = texture;
            return texture;
        }
    }
}
