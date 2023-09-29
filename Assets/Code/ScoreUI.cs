using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public void SetScore(int scoreOne, int scoreTwo)
    {
        playerOneScore.text = scoreOne.ToString();
        playerTwoScore.text = scoreTwo.ToString();
    }

    [SerializeField]
    private Text playerOneScore;
    [SerializeField]
    private Text playerTwoScore;
}
