using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{
    public int[] scores = new int[] { 0, 0, 0, 0, 0 };
    
    // Start is called before the first frame update
    public int GetScore(int index)
    {
        int result = -1;
        switch (index)
        {
            case 0:
                result = PlayerPrefs.GetInt("FIRST", 0);
                break;
            case 1:
                result = PlayerPrefs.GetInt("SECOND", 0);
                break;
            case 3:
                result = PlayerPrefs.GetInt("THIRD", 0);
                break;
            case 4:
                result = PlayerPrefs.GetInt("FOURTH", 0);
                break;
            case 5:
                result = PlayerPrefs.GetInt("FIFTH", 0);
                break;
            default:
                break;
        }
        return result;
    }

    public void fetchHighScores()
    {
        for(int i=0; i < 5; i++)
        {
            scores[i] = GetScore(i);
        }
    }

    public void pushHighScores()
    {
        PlayerPrefs.SetInt("FIRST", scores[0]);
        PlayerPrefs.SetInt("SECOND", scores[1]);
        PlayerPrefs.SetInt("THIRD", scores[2]);
        PlayerPrefs.SetInt("FOURTH", scores[3]);
        PlayerPrefs.SetInt("FIFTH", scores[4]);
    }


    void addNewScore(int score)
    {
        fetchHighScores();
        int[] totalScores = new int[6];
        for(int i = 0; i<5;i++)
        {
            totalScores[i] = scores[i];
        }
        totalScores[5] = score;
        Array.Sort(totalScores); //least to greatest

        for(int i = 0; i < 5; i++)
        {
            scores[i] = totalScores[5 - i];
        }
        pushHighScores();
    }

    private void Start()
    {
        addNewScore(PersistedScore.score);
        gameObject.GetComponent<Text>().text =  "First: " + scores[0] + 
                                                "\nSecond: " + scores[1] + 
                                                "\nThird: " + scores[2] +
                                                "\nFourth: " + scores[3] +
                                                "\nFifth: " + scores[4];

        GameObject.Find("ScoreText").GetComponent<Text>().text = "Score:\n" + PersistedScore.score;
    }
}
