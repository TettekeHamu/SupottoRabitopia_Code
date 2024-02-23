using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace PullAnimals
{
    /// <summary>
    /// スコアの表示をおこなうクラス
    /// </summary>
    public class ScoreUIView : MonoBehaviour
    {
        /// <summary>
        /// スコアを表示するText
        /// </summary>
        [SerializeField] private Text _scoreText;
        /// <summary>
        /// スコアが加算されたときに表示するText
        /// </summary>
        [SerializeField] private Text _addScoreText;
        /// <summary>
        /// 加算Textの初期位置
        /// </summary>
        private Vector3 _addScorePos;

        private void Awake()
        {
            _addScoreText.gameObject.SetActive(false);
            _addScorePos = _addScoreText.transform.position;
        }
        
        /// <summary>
        /// スコアが加算されたときにTextを表示させるコルーチン
        /// </summary>
        /// <param name="addScore">加算された値</param>
        /// <returns></returns>
        private IEnumerator ShowAddScoreCoroutine(int addScore)
        {
            _addScoreText.transform.position = _addScorePos;
            _addScoreText.gameObject.SetActive(true);
            _addScoreText.text = "ぷらす" + addScore.ToString() + "てん！";
            yield return _addScoreText.transform
                .DOMoveY(_addScorePos.y + 40, 1f)
                .SetLink(gameObject)
                .WaitForCompletion();
            _addScoreText.gameObject.SetActive(false);
        }

        /// <summary>
        /// Textを更新する処理
        /// </summary>
        /// <param name="newScore">新しいスコア</param>
        public void UpdateScoreUI(int newScore)
        {
            _scoreText.text = "スコア：" + newScore.ToString();
        }

        /// <summary>
        /// スコアが加算されたときにTextを表示させる処理
        /// </summary>
        /// <param name="addScore">加算された値</param>
        public void ShowAddScore(int addScore)
        {
            StartCoroutine(ShowAddScoreCoroutine(addScore));
        }
    }
}
