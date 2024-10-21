using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Based on tutorial: https://www.youtube.com/watch?v=z5CdXvbTQ2Q
public class ScoreCounter : MonoBehaviour
{
    private TMP_Text scoreCounterText;
    public int scoreValue;

    public enum ScoreType { Egg, Fly }; // Enum for differentiating between Egg & Fly scoring
    public ScoreType scoreType; // set score type in the Inspector


    private void Awake()
    {
        Debug.Log("ScoreCounter AWAKE");

        // Subscribe to the 1 egg event or the 2 fly events, depending on which score type we're measuring.
        // This script is attached to both TMP_Texts for both of the scores.
        if (scoreType == ScoreType.Egg)
        {
            CaddisflyBehaviour.EggCountIncreased += IncrementEggScore;
        }
        else if (scoreType == ScoreType.Fly)
        {
            TouchScript.CaddisflyCountIncreased += IncrementFlyScore;
            SpiderBehaviour.CaddisflyCountDecreased += DecrementFlyScore;
        }

        scoreCounterText = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        Debug.Log("ScoreCounter START");
        scoreValue = 0;
    }

    private void Update()
    {
        //Debug.Log("ScoreCounter UPDATE scoreCounterText.text = " + scoreCounterText.text + "; scoreValue = " + scoreValue);
        scoreCounterText.text = scoreValue.ToString();
    }

    private void IncrementEggScore()
    {
        scoreValue += 1;
        Debug.LogWarning("[SC] Increased Egg Score: new score = " + scoreValue);
    }

    private void IncrementFlyScore()
    {
        scoreValue += 1;
        Debug.LogWarning("[SC] Increased Fly Score: new score = " + scoreValue);
    }
    private void DecrementFlyScore()
    {
        scoreValue -= 1;
        Debug.LogError("[SC] Decreased Fly Score: new score = " + scoreValue);
    }


    private void OnDestroy()
    {
        Debug.Log("ScoreCounter ONDESTROY");

        if (scoreType == ScoreType.Egg)
        {
            CaddisflyBehaviour.EggCountIncreased -= IncrementEggScore;
        }
        else if (scoreType == ScoreType.Fly)
        {
            TouchScript.CaddisflyCountIncreased -= IncrementFlyScore;
            SpiderBehaviour.CaddisflyCountDecreased -= DecrementFlyScore;
        }
    }


}
