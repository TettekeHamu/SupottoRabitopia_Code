using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTextController : MonoBehaviour
{
    /// <summary>
    /// ハイスコアを表示するtext
    /// </summary>
    [SerializeField] private Text _highScoreText;
    /// <summary>
    /// ハイスコアの変数
    /// </summary>
    [SerializeField] private int _highScore;

    private int _totalScore;
    // Start is called before the first frame update
    void Start()
    {
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
        // メインシーンからトータルスコアの数持ってくる
        _totalScore = PlayerPrefs.GetInt("TotalScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (_totalScore > _highScore)
        {
            _highScore = _totalScore;
            PlayerPrefs.SetInt("HighScore", _highScore);
        }
    }
}
