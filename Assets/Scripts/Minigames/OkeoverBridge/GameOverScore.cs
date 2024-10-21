using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScore : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text eggScoreText;
    [SerializeField] TMP_Text flyScoreText;


    public void Start()
    {
        Debug.Log("egg score: " + eggScoreText.text);
        Debug.Log("fly score: " + flyScoreText.text);
        scoreText.text = (int.Parse(eggScoreText.text) + int.Parse(flyScoreText.text)).ToString();
        Debug.Log("FINAL SCORE: " + scoreText.text);
    }
}
