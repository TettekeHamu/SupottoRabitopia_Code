using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;

namespace PullAnimals
{
    public class EndSceneTextController : MonoBehaviour
    {
        /// <summary>
        /// InputSystemを使用するためのコンポーネント
        /// </summary>
        [SerializeField] private PlayerInput _playerInput;
        /// <summary>
        /// 普通のウサギを抜いた数を表示するTextオブジェクト
        /// </summary>
        [SerializeField] private Text _normalRabbitNumText;
        /// <summary>
        /// 銀のウサギを抜いた数を表示するTextオブジェクト
        /// </summary>
        [SerializeField] private Text _silverRabbitNumText;
        /// <summary>
        /// 金のウサギを抜いた数を表示するTextオブジェクト
        /// </summary>
        [SerializeField] private Text _goldRabbitNumText;
        /// <summary>
        /// トータルスコアを表示するTextオブジェクト
        /// </summary>
        [SerializeField] private Text _totalScoreNumText;
        /// <summary>
        /// 終わったことを知らせるTextオブジェクト
        /// </summary>
        [SerializeField] private Text _endText;
        /// <summary>
        /// タイトルに戻るボタンを表示するTextオブジェクト
        /// </summary>
        [SerializeField] private Text _keyText;
        /// <summary>
        /// スコアを表示させた後次のtextに変えるボタン表示するTextオブジェクト
        /// </summary>
        [SerializeField] private Text _changeText;
        /// <summary>
        /// 普通のウサギの画像を表示するImageオブジェクト
        /// </summary>
        [SerializeField] private Image _normalRabbitNumImage;
        /// <summary>
        /// 銀のウサギの画像を表示するImageオブジェクト
        /// </summary>
        [SerializeField] private Image _silverRabbitNumImage;
        /// <summary>
        /// 金のウサギの画像を表示するImageオブジェクト
        /// </summary>
        [SerializeField] private Image _goldRabbitNumImage;
        /// <summary>
        /// 背景を表示するImageオブジェクト
        /// </summary>
        [SerializeField] private Image _bgImage;

        /// <summary>
        /// 掘った普通のラビットの数
        /// </summary>
        private int _rabbitNum;
        /// <summary>
        /// 掘った銀のラビットの数
        /// </summary>
        private int _silverRabbitNum;
        /// <summary>
        /// 掘った金のラビットの数
        /// </summary>
        private int _goldRabbitNum;
        /// <summary>
        /// トータルスコア
        /// </summary>
        private int _totalScore;
        /// <summary>
        /// トータルスコア
        /// </summary>
        private int _maxScore;

        /// <summary>
        /// textに映すラビットの数の初期化
        /// </summary>
        private int _nowRabbitNum = 0;
        /// <summary>
        /// textに映すスコアの数の初期化
        /// </summary>
        private int nowScoreNum = 0;

        private void Awake()
        {
            // メインシーンからノーマルラビットの数を持ってくる
            _rabbitNum = PlayerPrefs.GetInt("NormalRabbit", 0);
            // メインシーンから銀ラビットの数を持ってくる
            _silverRabbitNum = PlayerPrefs.GetInt("SilverRabbit", 0);
            // メインシーンから金ラビットの数を持ってくる
            _goldRabbitNum = PlayerPrefs.GetInt("GoldRabbit", 0);
            // メインシーンからトータルスコアの数持ってくる
            _totalScore = PlayerPrefs.GetInt("TotalScore", 0);
            // メインシーンからトータルスコアの数持ってくる
            _maxScore = PlayerPrefs.GetInt("MaxScore", 1);
        }
        // Start is called before the first frame update
        void Start()
        {
            //シーンが変わってから条件付きでtextを表示させるコルーチン
            StartCoroutine(MyCoroutine());
        }

        // Update is called once per frame
        void Update()
        {
            //Aボタンを押したらtextを切り替える
            if (_changeText.gameObject.activeInHierarchy)
            {
                if (_playerInput.actions["ChangeText"].WasPressedThisFrame())
                {
                    SePlayer.Instance.Play("SE_Decide");
                    //スコアのtextを非表示にして次のtextを表示させる
                    ChangeText();
                }
            }
        }
        //シーンが変わってから条件付きでtextを表示させるコルーチン
        IEnumerator MyCoroutine()
        {
            //一秒待ってからゲームオブジェクトを表示させる
            yield return new WaitForSeconds(1.0f);
            _normalRabbitNumImage.gameObject.SetActive(true);
            _normalRabbitNumText.gameObject.SetActive(true);

            //SEの再生
            SePlayer.Instance.Play("SE_RabbitNum");

            //0.2秒待ってからゲームオブジェクトを表示させる
            yield return new WaitForSeconds(0.2f);

            // ラビットの数をtextに表示
            DOTween.To(() => _nowRabbitNum, (n) => _nowRabbitNum = n, _rabbitNum, 1)
            .OnUpdate(() => _normalRabbitNumText.text = "x" + _nowRabbitNum.ToString("#,0"));

            _normalRabbitNumText.transform.DOPunchScale(new Vector3(1, 1, 0), 2.0f);

            //一秒待ってからゲームオブジェクトを表示させる
            yield return new WaitForSeconds(1.0f);
            _silverRabbitNumImage.gameObject.SetActive(true);
            _silverRabbitNumText.gameObject.SetActive(true);

            //SEの再生
            SePlayer.Instance.Play("SE_RabbitNum");

            //textに映すラビットの数の初期化
            _nowRabbitNum = 0;

            //0.2秒待ってからゲームオブジェクトを表示させる
            yield return new WaitForSeconds(0.2f);

            // 銀ラビットの数をtext表示
            DOTween.To(() => _nowRabbitNum, (n) => _nowRabbitNum = n, _silverRabbitNum, 1)
            .OnUpdate(() => _silverRabbitNumText.text = "x" + _nowRabbitNum.ToString("#,0"));

            _silverRabbitNumText.transform.DOPunchScale(new Vector3(0.7f, 1, 0), 2.0f);

            //一秒待ってからゲームオブジェクトを表示させる
            yield return new WaitForSeconds(1.0f);
            _goldRabbitNumImage.gameObject.SetActive(true);
            _goldRabbitNumText.gameObject.SetActive(true);

            //SEの再生
            SePlayer.Instance.Play("SE_RabbitNum");

            //textに映すラビットの数の初期化
            _nowRabbitNum = 0;

            
            //0.2秒待ってからゲームオブジェクトを表示させる
            yield return new WaitForSeconds(0.2f);

            // 金ラビットの数をtext表示
            DOTween.To(() => _nowRabbitNum, (n) => _nowRabbitNum = n, _goldRabbitNum, 1)
            .OnUpdate(() => _goldRabbitNumText.text = "x" + _nowRabbitNum.ToString("#,0"));

            _goldRabbitNumText.transform.DOPunchScale(new Vector3(0.7f, 1, 0), 2.0f);

            //一秒待ってからゲームオブジェクトを表示させる
            yield return new WaitForSeconds(1.0f);
            _totalScoreNumText.gameObject.SetActive(true);

            //0.2秒待ってからゲームオブジェクトを表示させる
            yield return new WaitForSeconds(0.2f);

            // トータルスコアをtextに表示
            DOTween.To(() => nowScoreNum, (n) => nowScoreNum = n, _totalScore, 1)
            .OnUpdate(() => _totalScoreNumText.text = nowScoreNum.ToString("#,0"));

            //SEの再生
            SePlayer.Instance.Play("SE_Score");

            yield return new WaitForSeconds(1.0f);
            _changeText.gameObject.SetActive(true);
        }
        private void ChangeText()
        {
            //textとimageの切り替え
            _endText.gameObject.SetActive(true);
            _keyText.gameObject.SetActive(true);
            _normalRabbitNumText.gameObject.SetActive(false);
            _silverRabbitNumText.gameObject.SetActive(false);
            _goldRabbitNumText.gameObject.SetActive(false);
            _totalScoreNumText.gameObject.SetActive(false);
            _changeText.gameObject.SetActive(false);
            _normalRabbitNumImage.gameObject.SetActive(false);
            _silverRabbitNumImage.gameObject.SetActive(false);
            _goldRabbitNumImage.gameObject.SetActive(false);
            _bgImage.gameObject.SetActive(false);
        }
        
    }
}

