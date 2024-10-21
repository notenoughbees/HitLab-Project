// MOVED TO assets/scripts/minigames/...

//using System;
//using System.Collections.Generic;
//using Niantic.Lightship.Maps.SampleAssets.Player;
//using Niantic.Lightship.Maps.Samples.GameSample;
//using TMPro;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using HitLabGame;

//namespace HitLabGame
//{
//    //namespace Niantic.Lightship.Maps.Samples.GameSample
//    //{
//    /// <summary>
//    /// UI controller for the Okeover Bridge minigame.
//    /// </summary>
//    public class MinigameOkeoverBridgeUIController : MonoBehaviour
//    {
//        //[SerializeField]
//        //private GameObject touchScript;

//        [SerializeField]
//        private GameObject _introScreen;

//        [SerializeField]
//        private GameObject _gamePlayScreen;

//        [SerializeField]
//        private GameObject _gameOverScreen;

//        [SerializeField]
//        private GameObject _XrOrigin;

//        [SerializeField]
//        private GameObject _ArSession;

//        public bool isPlayingGame { get; internal set; } = false;
//        //public bool isPlayingGame = false;
//        public float timeLeft = 18f;
//        public TMP_Text timerValue;

//        private void Start()
//        {
//            // only show the intro screen initially
//            _introScreen.SetActive(true);
//            _gamePlayScreen.SetActive(false);
//            _gameOverScreen.SetActive(false);
//        }

//        private void Update()
//        {
//            if (isPlayingGame)
//            {
//                // https://www.techwithsach.com/post/how-to-add-a-simple-countdown-timer-in-unity
//                timeLeft -= Time.deltaTime;
//                timerValue.text = timeLeft.ToString("0");
//            }

//            if (timeLeft <= 0)
//            {
//                isPlayingGame = false;
//                _gamePlayScreen.SetActive(false);
//                _gameOverScreen.SetActive(true);
//                //var camera = GetComponent<Camera>();
//                //GameObject camera = GameObject.FindGameObjectsWithTag("MainCamera")[0]; // there should only be 1 camera
//                //TouchScript script = camera.GetComponent <TouchScript>();
//                //Destroy(camera.FindObjectOfType(Script
//                //Destroy(touchScript);

//                //GameObject camera = Camera.main.gameObject;
//                //Destroy(camera.GetComponent("TouchScript"));
//                //Debug.Log("Destroyed touchScript!");
//            }


//        }





//        public void OnIntroContinuePressed()
//        {
//            _introScreen.SetActive(false);
//            _gamePlayScreen.SetActive(true);
//            isPlayingGame = true;
//        }


//        public void OnGameplayBackToMapPressed()
//        {
//            isPlayingGame = false;
//            SceneManager.LoadScene("MapResourceGameSampleScene");
//        }









//        public void OnGameOverReplayPressed()
//        {
//            //_gameOverScreen.SetActive(false);
//            //_introScreen.SetActive(true);
//            //TODO
//        }


//        public void OnGameOverContinuePressed()
//        {
//            SceneManager.LoadScene("MapResourceGameSampleScene");
//        }


//        private void OnDestroy()
//        {
//            if (MapGameState.Instance != null)
//            {

//            }

//        }


//    }
//    //}
//}


