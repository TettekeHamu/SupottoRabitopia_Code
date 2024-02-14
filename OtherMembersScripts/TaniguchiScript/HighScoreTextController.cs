using UnityEngine;
using UnityEngine.UI;

namespace PullAnimals
{
    public class HighScoreTextController : MonoBehaviour
    {
        private int _totalScore;
        private int _nowRank;
        [SerializeField]private string[] _ranking = {};
        private int[] _rankingValue = new int[50];
        [SerializeField] private Text[] _rankingText = new Text[3];
        [SerializeField] private Text _myRankText;

        // Start is called before the first frame update
        void Start()
        {
            _totalScore = PlayerPrefs.GetInt("TotalScore", 0);
            _nowRank = 0;
            GetRanking();
            SetRanking(_totalScore);
            
            for (int i = 0; i < _rankingText.Length; i++)
            {
                _rankingText[i].text = _rankingValue[i].ToString();
            }
            if (_nowRank > 0 && _nowRank <= _rankingValue.Length)
            {
                _myRankText.text = _nowRank.ToString() + "位";
            }

        }
        void GetRanking()
        {
            for (int i = 0; i < _ranking.Length; i++)
            {
                _rankingValue[i] = PlayerPrefs.GetInt(_ranking[i]);
            }
        }
        void SetRanking(int value)
        {
            for (int i = 0; i < _ranking.Length; ++i)
            {
                if (value >= _rankingValue[i])
                {
                    var change = _rankingValue[i];
                    _rankingValue[i] = value;
                    value = change;
                    if (_nowRank == 0)
                    {
                        _nowRank = i + 1;
                    }
                }
            }
            for (int i = 0; i < _ranking.Length; i++)
            {
                PlayerPrefs.SetInt(_ranking[i], _rankingValue[i]);
            }
        }
    }
}