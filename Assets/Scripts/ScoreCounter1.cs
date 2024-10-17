using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Based on tutorial: https://www.youtube.com/watch?v=z5CdXvbTQ2Q
public class ScoreCounter1 : MonoBehaviour
{
    private TMP_Text scoreCounterText;
    public int scoreValue;

    public enum ScoreType { Egg, Fly }; // Enum for differentiate between Egg & Fly scoring
    public ScoreType scoreType; // set score type in the Inspector


    private void Awake()
    {
        Debug.Log("ScoreCounter1 AWAKE");

        // watch both events, and attach this script to both TMP_Texts for both of the scores
        if (scoreType == ScoreType.Egg)
        {
            CaddisflyBehaviour.EggCountIncreased += IncrementEggScore;
        }
        else if (scoreType == ScoreType.Fly)
        {
            TouchScript.CaddisflyCountIncreased += IncrementFlyScore;
        }

        scoreCounterText = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        Debug.Log("ScoreCounter1 START");
        scoreValue = 0;
    }

    private void Update()
    {
        //Debug.Log("ScoreCounter1 UPDATE scoreCounterText.text = " + scoreCounterText.text + "; scoreValue = " + scoreValue);
        scoreCounterText.text = scoreValue.ToString();
    }

    private void IncrementEggScore()
    {
        Debug.Log("Increasing Egg Score");
        scoreValue += 1;
    }

    private void IncrementFlyScore()
    {
        Debug.Log("Increasing Fly Score");
        scoreValue += 1;
    }

    private void OnDestroy()
    {
        Debug.Log("ScoreCounter1 ONDESTROY");

        if (scoreType == ScoreType.Egg)
        {
            CaddisflyBehaviour.EggCountIncreased -= IncrementEggScore;
        }
        else if (scoreType == ScoreType.Fly)
        {
            TouchScript.CaddisflyCountIncreased -= IncrementFlyScore;
        }
    }


}
