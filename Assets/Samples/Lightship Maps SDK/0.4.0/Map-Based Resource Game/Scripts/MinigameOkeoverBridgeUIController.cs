// Copyright 2022 Niantic, Inc. All Rights Reserved.

using System;
using System.Collections.Generic;
using Niantic.Lightship.Maps.SampleAssets.Player;
using Niantic.Lightship.Maps.Samples.GameSample;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//namespace Niantic.Lightship.Maps.Samples.GameSample
//{
/// <summary>
/// UI controller for the Okeover Bridge minigame.
/// </summary>
public class MinigameOkeoverBridgeUIController : MonoBehaviour
    {
     
        [SerializeField]
        private GameObject _introScreen;

        [SerializeField]
        private GameObject _errorScreen;

        [SerializeField]
        private GameObject _gamePlayScreen;

        [SerializeField]
        private GameObject _gameOverScreen;

        [SerializeField]
        private GameObject _XrOrigin;

        [SerializeField]
        private GameObject _ArSession;

        [SerializeField]
        private TMP_Text _errorText;


        private bool _hasPlayerWon;

        private double _lastGpsUpdateTime;

        private void Start()
        {
            _introScreen.SetActive(true);
            _gamePlayScreen.SetActive(false);
            _gameOverScreen.SetActive(false);

        }

        private void Update()
        {




        }

        private void OnDestroy()
        {
            if (MapGameState.Instance != null)
            {

            }

        }



        public void OnIntroContinuePressed()
        {
            _introScreen.SetActive(false);
            _gamePlayScreen.SetActive(true);
        }

























        public void OnGameplayBackToMapPressed()
        {
            SceneManager.LoadScene("MapResourceGameSampleScene");
        }





        public void OnGameOverContinuePressed()
        {
            _gameOverScreen.SetActive(false);
            _gamePlayScreen.SetActive(true);
        }




}
//}
