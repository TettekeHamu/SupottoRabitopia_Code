using PullAnimals.SceneLoader;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// ゲーム実行時の一度しか開かないシーンを管理するクラス
    /// </summary>
    public class FirstSetupController : MonoBehaviour
    {
        private void Start()
        {
            //すぐタイトルシーンに遷移させる
            //Awake()だと何故かビルド時動かないのでStart()で実行する
            SceneLoadController.Instance.FlashLoadScene(SceneNameContainer.Title);
        }
    }
}
