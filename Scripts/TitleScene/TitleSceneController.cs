using PullAnimals.SceneLoader;
using UnityEngine;
using UnityEngine.UI;

namespace PullAnimals
{
    /// <summary>
    /// タイトルシーンを管理するクラス
    /// </summary>
    public class TitleSceneController : MonoBehaviour,ISetUpScene
    {
        [SerializeField] private Text _testText;
        /// <summary>
        /// シーンを遷移中かどうか
        /// </summary>
        private bool _isChangingScene;

        private void Update()
        {
            if (MyInputController.Instance.ChangeSceneKeyDown && !_isChangingScene)
            {
                _isChangingScene = true;
                _testText.gameObject.SetActive(true);
                SceneLoadController.Instance.LoadNextScene("MainSceneVer1.0");
            }
        }

        void ISetUpScene.SetUpScene()
        {
            //Debug.Log("シーン開始！！");
            _testText.gameObject.SetActive(false);
            _isChangingScene = false;
        }
    }
}
