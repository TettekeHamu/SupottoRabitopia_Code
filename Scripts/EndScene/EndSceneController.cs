using PullAnimals.SceneLoader;
using UnityEngine;
using UnityEngine.UI;

namespace PullAnimals
{
    public class EndSceneController : MonoBehaviour,ISetUpScene
    {
        /// <summary>
        /// シーンを遷移中かどうか
        /// </summary>
        private bool _isChangingScene;

        /// <summary>
        /// NextSceneTextを格納する
        /// </summary>
        [SerializeField] private Text _nextSceneText;

        private void Awake()
        {
            _isChangingScene = false;
        }

        private void Update()
        {
            if (MyInputController.Instance.ChangeSceneKeyDown && !_isChangingScene && _nextSceneText.gameObject.activeInHierarchy)
            {
                BgmPlayer.Instance.Pause();
                _isChangingScene = true;
                SceneLoadController.Instance.LoadNextScene("TitleSceneVer1.0");
            }
        }

        void ISetUpScene.SetUpScene()
        {
            BgmPlayer.Instance.Play("BGM_Ending");
        }
    }
}
