using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    public int Score = 0;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Score = PersistedScore.score;
        scoreText.text = "Score: " + Score;
    }

    public void AddPoints(int amount)
    {
        PersistedScore.score += amount;
    }
}
