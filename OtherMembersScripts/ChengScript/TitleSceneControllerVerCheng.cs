using PullAnimals.SceneLoader;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// タイトルシーンを管理するクラス
    /// </summary>
    public class TitleSceneControllerVerCheng : MonoBehaviour, ISetUpScene
    {
        /// <summary>
        /// CanvasControllerのオブジェクト
        /// </summary>
        [SerializeField] private CanvasController _canvasController;

        /// <summary>
        /// 初期セットアップ用フラグ
        /// </summary>
        [SerializeField] private bool _play = true;

        /// <summary>
        /// BGMの音量(%)
        /// </summary>
        [SerializeField] private float _bgmVolume;

        /// <summary>
        /// 動作確認(開発用)
        /// </summary>
        //[SerializeField] private bool _test_action = false;

        /// <summary>
        /// シーンを遷移中かどうか
        /// </summary>
        private bool _isChangingScene;

        private void Start()
        {
            _canvasController.SetTitleImage();
            BgmPlayer.Instance.Play("BGM_Title");
            BgmPlayer.Instance.ChangeVolume(0.5f);
        }

        private void Update()
        {
            if (_play)
            {
                _canvasController.SetTitleAction();
                _play = false;
            }

            if(_canvasController.SetComplete())
            {
                GetComponent<ParticleSystem>().Play();
            }

            //開発用のためコメントアウト
            //if(_test_action)
            //{
            //    _canvasController.testAction();
            //    _test_action = false;
            //}

            _canvasController.LoopTitleAction();

            if (TitleInputController.Instance.GetChangeSceneKeyDown() && !_isChangingScene)
            {
                SePlayer.Instance.Play("SE_Decide");
                _isChangingScene = true;
                //_canvasController.gameObject.SetActive(true);
                BgmPlayer.Instance.Stop();
                
                SceneLoadController.Instance.LoadNextScene("MainSceneVer3.0");
            }
        }

        void ISetUpScene.SetUpScene()
        {
            //Debug.Log("シーン開始！！");
            _isChangingScene = false;
        }
    }
}
