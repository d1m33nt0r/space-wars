using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class BestScore : MonoBehaviour
{
    public GameObject scoreUITextGO;

    Text bestScoreTextUI;

    int currentScore;
    int bestScore;

    // Start is called before the first frame update
    void Start()
    {

        bestScoreTextUI = GetComponent<Text>();
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }


    public void SetBestScore()
    {
        currentScore = scoreUITextGO.GetComponent<GameScore>().Score;
        bestScore = PlayerPrefs.GetInt("BestScore");


        if (currentScore > bestScore)
        {
            bestScore = currentScore;


            PlayerPrefs.SetInt("BestScore", bestScore);




        }
        string scoreStr = string.Format("{0:0000000}", bestScore);
        bestScoreTextUI.text = scoreStr;

    }
}
