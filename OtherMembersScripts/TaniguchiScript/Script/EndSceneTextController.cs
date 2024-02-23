using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace PullAnimals
{
    public class EndSceneTextController : MonoBehaviour
    {
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
        /// トータルスコアタイトルを表示するTextオブジェクト
        /// </summary>
        [SerializeField] private Text _nowTotalScoreNumText;
        /// <summary>
        /// 順位タイトルを表示するTextオブジェクト
        /// </summary>
        [SerializeField] private Text _nowRankNumText;
        /// <summary>
        /// 順位タイトルを表示するTextオブジェクト
        /// </summary>
        [SerializeField] private Text _rankNumText;
        /// <summary>
        /// ランキング一位のスコアを表示するTextオブジェクト
        /// </summary>
        [SerializeField] private Text _rank1NumText;
        /// <summary>
        /// ランキング二位のスコアを表示するTextオブジェクト
        /// </summary>
        [SerializeField] private Text _rank2NumText;
        /// <summary>
        /// ランキング三位のスコアを表示するTextオブジェクト
        /// </summary>
        [SerializeField] private Text _rank3NumText;
        /// <summary>
        /// ランキングタイトルを表示するTextオブジェクト
        /// </summary>
        [SerializeField] private Text _dayRankText;
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
        [SerializeField] private Image _bgImage0;
        /// <summary>
        /// 文字なし背景を表示するImageオブジェクト
        /// </summary>
        [SerializeField] private Image _bgImage1;

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
        /// textに映すラビットの数の初期化
        /// </summary>
        private int _nowRabbitNum = 0;
        /// <summary>
        /// textに映すスコアの数の初期化
        /// </summary>
        private int _nowScoreNum = 0;

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
                if (EndInputController.Instance.GetChangeTextKeyDown())
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
            TextCountAnimation(_nowRabbitNum, _rabbitNum, _normalRabbitNumText);

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
            TextCountAnimation(_nowRabbitNum, _silverRabbitNum, _silverRabbitNumText);

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
            TextCountAnimation(_nowRabbitNum, _goldRabbitNum, _goldRabbitNumText);

            _goldRabbitNumText.transform.DOPunchScale(new Vector3(0.7f, 1, 0), 2.0f);

            //一秒待ってからゲームオブジェクトを表示させる
            yield return new WaitForSeconds(1.0f);
            _totalScoreNumText.gameObject.SetActive(true);

            //0.2秒待ってからゲームオブジェクトを表示させる
            yield return new WaitForSeconds(0.2f);

            // トータルスコアをtextに表示
            DOTween.To(() => _nowScoreNum, (n) => _nowScoreNum = n, _totalScore, 1)
            .OnUpdate(() => _totalScoreNumText.text =_nowScoreNum.ToString("#,0"));

            //SEの再生
            SePlayer.Instance.Play("SE_Score");

            yield return new WaitForSeconds(1.0f);
            _changeText.gameObject.SetActive(true);
        }
        private void ChangeText()
        {
            //textとimageの切り替え
            _dayRankText.gameObject.SetActive(true);
            _nowTotalScoreNumText.gameObject.SetActive(true);
            _nowRankNumText.gameObject.SetActive(true);
            _keyText.gameObject.SetActive(true);
            _rankNumText.gameObject.SetActive(true);
            _rank1NumText.gameObject.SetActive(true);
            _rank2NumText.gameObject.SetActive(true);
            _rank3NumText.gameObject.SetActive(true);
            _bgImage1.gameObject.SetActive(true);
            _normalRabbitNumText.gameObject.SetActive(false);
            _silverRabbitNumText.gameObject.SetActive(false);
            _goldRabbitNumText.gameObject.SetActive(false);
            _changeText.gameObject.SetActive(false);
            _normalRabbitNumImage.gameObject.SetActive(false);
            _silverRabbitNumImage.gameObject.SetActive(false);
            _goldRabbitNumImage.gameObject.SetActive(false);
            _bgImage0.gameObject.SetActive(false);
        }
        private void TextCountAnimation(int startNum,int rabbitNum,Text rabbitNumText)
        {
            DOTween.To(() => startNum, (n) => startNum = n, rabbitNum, 1)
            .OnUpdate(() => rabbitNumText.text = "x" + startNum.ToString("#,0"));
        }
        
    }
}

