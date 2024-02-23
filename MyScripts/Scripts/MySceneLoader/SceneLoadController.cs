using System.Collections;
using DG.Tweening;
using PullAnimals.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PullAnimals.SceneLoader
{
    /// <summary>
    /// アニメーション付きでシーンの遷移をおこなうクラス
    /// </summary>
    public class SceneLoadController : DDMonoSingletonBase<SceneLoadController>
    {
        /// <summary>
        /// ロード用のImageなどを持つCanvas
        /// </summary>
        [SerializeField] private Canvas _canvas;
        /// <summary>
        /// ロードに使う画像
        /// </summary>
        [SerializeField] private Image _fadeImage;

        protected override void Awake()
        {
            //親クラスのAwake()を実行
            base.Awake();
            //canvasをオフにする
            _canvas.gameObject.SetActive(false);
        }

        /// <summary>
        /// シーンの遷移をおこなうコルーチン
        /// </summary>
        /// <param name="sceneName">移行先のシーン名</param>
        private IEnumerator FlashLoadSceneCoroutine(string sceneName)
        {
            //シーンを変える
            SceneManager.LoadScene(sceneName);
            //1フレーム待つ（この間に切り替え先のSceneのAwake()をおこなう）
            yield return null;
            //マネージャーにシーンの読み込みが終わったことを伝える
            var setUpScene = GetComponentInterface.FindObjectOfInterface<ISetUpScene>();
            setUpScene?.SetUpScene();
        }

        /// <summary>
        /// シーンの遷移をおこなうコルーチン
        /// </summary>
        /// <param name="nestSceneName">移行先のシーン名</param>
        /// <param name="loadTime">移行に必要な時間</param>
        private IEnumerator LoadSceneCoroutine(string nestSceneName,float loadTime)
        {
            //アニメーションさせる
            yield return StartTransitionSceneAnimeCoroutine(loadTime / 2);
            //シーンを変える
            yield return SceneManager.LoadSceneAsync(nestSceneName);
            //1フレーム待つ（この間に切り替え先のSceneのAwake()をおこなう）
            yield return null;
            //アニメーションさせる
            yield return EndTransitionSceneAnimeCoroutine(loadTime / 2);
            //マネージャーにシーンの読み込みが終わったことを伝える
            var setUpScene = GetComponentInterface.FindObjectOfInterface<ISetUpScene>();
            setUpScene?.SetUpScene();
        }

        /// <summary>
        /// シーンを変えるときにおこなうアニメーションのコルーチン
        /// </summary>
        /// <param name="animationTime">アニメーションの時間</param>
        private IEnumerator StartTransitionSceneAnimeCoroutine(float animationTime)
        {
            //Canvasをオンにする
            _canvas.gameObject.SetActive(true);
            //画面右端に画像を置く
            _fadeImage.rectTransform.position = new Vector2(1920, 1080 / 2f);
            //真ん中までアニメーションさせる
            yield return _fadeImage.rectTransform
                .DOMoveX(1920 / 2f, animationTime)
                .SetEase(Ease.OutSine)
                .SetLink(gameObject)
                .WaitForCompletion();
        }

        /// <summary>
        /// シーンが遷移し終わったあとのアニメーションをおこなうコルーチン
        /// </summary>
        /// <param name="animationTime">アニメーションの時間</param>
        private IEnumerator EndTransitionSceneAnimeCoroutine(float animationTime)
        {
            //画面左端までアニメーションさせる
            yield return _fadeImage.rectTransform
                .DOMoveX(0, animationTime)
                .SetEase(Ease.InSine)
                .SetLink(gameObject)
                .WaitForCompletion();
            //キャンバスをオフにする
            _canvas.gameObject.SetActive(false);
        }
        
        /// <summary>
        /// アニメーションなしでシーンを読み込む処理
        /// </summary>
        /// <param name="sceneName"></param>
        public void FlashLoadScene(string sceneName)
        {
            StartCoroutine(FlashLoadSceneCoroutine(sceneName));
        }
        
        /// <summary>
        /// 次のシーンを読み込む処理
        /// </summary>
        /// <param name="nestSceneName">読み込むシーン名</param>
        /// <param name="loadTime">読み込みの時間</param>
        public void LoadNextScene(string nestSceneName,float loadTime = 1.5f)
        {
            StartCoroutine(LoadSceneCoroutine(nestSceneName, loadTime));
        }
    }
}
