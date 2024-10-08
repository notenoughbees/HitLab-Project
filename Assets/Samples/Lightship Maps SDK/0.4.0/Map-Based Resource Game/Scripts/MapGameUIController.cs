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
/// Simple UI controller for the MapGame, it switches between a couple of screens and reacts to
/// various button presses and keeps resources UI updated
/// </summary>
public class MapGameUIController : MonoBehaviour
    {
     
        [SerializeField]
        private MapGameMapInteractions _mapInteractibles;

        [SerializeField]
        private PlayerLocationController _player;

        [SerializeField]
        private GameObject _introScreen;

        [SerializeField]
        private GameObject _errorScreen;

        [SerializeField]
        private GameObject _gamePlayScreen;

        [SerializeField]
        private GameObject _buildMenu;

        [SerializeField]
        private GameObject _placeStructureScreen;

        [SerializeField]
        private GameObject _buildMenuButton;

        [SerializeField]
        private GameObject _gameOverScreen;

        [SerializeField]
        private TourismGameLandmarkFoundName _landmarkFoundScreen;

        [SerializeField]
        private GameObject _galleryScreen;  // test screen just for learning how to make a gallery of pictures

        [SerializeField]
        private GameObject _arModeScreen;

        [SerializeField]
        private GameObject _XrOrigin;

        [SerializeField]
        private GameObject _ArSession;

        [SerializeField]
        private GameObject _compassUI;

        [SerializeField]
        private GameObject _lightshipMap;

        [SerializeField]
        private GameObject _mapCamera;

        [SerializeField]
        private TMP_Text _woodText;

        [SerializeField]
        private TMP_Text _planksText;

        [SerializeField]
        private TMP_Text _stoneText;

        [SerializeField]
        private TMP_Text _bricksText;

        [SerializeField]
        private TMP_Text _errorText;


        private bool _hasPlayerWon;

        private double _lastGpsUpdateTime;

        private void Start()
        {
            _introScreen.SetActive(true);
            _gamePlayScreen.SetActive(false);
            _buildMenu.SetActive(false);
            _placeStructureScreen.SetActive(false);
            _buildMenuButton.SetActive(true);
            _gameOverScreen.SetActive(false);

            MapGameState.Instance.OnStructureBuilt += OnStructurePlaced;
            MapGameState.Instance.OnResourceUpdated += OnResourceUpdated;
            _player.OnGpsError += OnGpsError;
        }

        private void Update()
        {
            // For GPS-based landmark finding (not VPS):
            // when the player gets near any of the locations, make a popup window
            // appear saying they found the landmark, then let them enter AR Mode.

            // Disabling this feature to replace with VPS localisation feature!
            /*
            var gpsInfo = Input.location.lastData; // use Unity's GPS system
                if (gpsInfo.timestamp > _lastGpsUpdateTime)
                {
                    _lastGpsUpdateTime = gpsInfo.timestamp;
                    CheckIfPlayerNearLocation(gpsInfo);
                }
            */
        }

        /**
         * For GPS-based finding of landmarks: check if the player is near any of the locations, by using latitude & longitude.
         **/
        private void CheckIfPlayerNearLocation(LocationInfo gpsInfo)
        {
            Dictionary<string, Tuple<float, float>> landmarks = new Dictionary<string, Tuple<float, float>>
                {
                    {"John Britten North Entrance", Tuple.Create(-43.52046f, 172.58311f) },
                    {"John Britten East Entrance", Tuple.Create(-43.52069f, 172.58339f) },
                    {"Engcore Carpark Tree", Tuple.Create(-43.52118f, 172.58391f) },
                    {"EPS Entrance", Tuple.Create(-43.52128f, 172.58437f) },
                    {"Sir Robertson Stewart Statue", Tuple.Create(-43.52209f, 172.58308f) },
                    {"Forestry Entrance", Tuple.Create(-43.52302f, 172.58535f) },
                    {"Kauri Tree near Chemical Engineering", Tuple.Create(-43.52149f, 172.58451f) },
                };

            var current_lat = (float)gpsInfo.latitude; // convert doubles to floats: loses precision, but floats are good enough
            var current_lng = (float)gpsInfo.longitude;

            foreach (KeyValuePair<string, Tuple<float, float>> landmark in landmarks)
            {
                if (Vector3.Distance(new Vector3(current_lat, 0, current_lng), new Vector3(landmark.Value.Item1, 0f, landmark.Value.Item2)) < 0.001) // 0.0001 degrees: within 11.1 metres. Phones are accurate to a few metres, so this threshold is acceptable.
                {
                    _landmarkFoundScreen.gameObject.SetActive(true);
                    _landmarkFoundScreen.changeLandmarkName(landmark.Key); // make a ui element appear
                }
                else
                {
                    //_landmarkFoundScreen.gameObject.SetActive(false);
                }
            }
        }

        private void OnGpsError(string errorMessage)
            {
                _errorText.text = errorMessage;
                _errorScreen.SetActive(true);
            }

        private void OnDestroy()
        {
            if (MapGameState.Instance != null)
            {
                MapGameState.Instance.OnStructureBuilt -= OnStructurePlaced;
                MapGameState.Instance.OnResourceUpdated -= OnResourceUpdated;
            }

            if (_player != null)
            {
                _player.OnGpsError -= OnGpsError;
            }
        }

        private void OnResourceUpdated(MapGameState.ResourceType resourceType, int amount)
        {
            switch (resourceType)
            {
                case MapGameState.ResourceType.Wood:
                    _woodText.text = amount.ToString();
                    break;
                case MapGameState.ResourceType.Planks:
                    _planksText.text = amount.ToString();
                    break;
                case MapGameState.ResourceType.Stone:
                    _stoneText.text = amount.ToString();
                    break;
                case MapGameState.ResourceType.Bricks:
                    _bricksText.text = amount.ToString();
                    break;
            }
        }

        public void OnIntroContinuePressed()
        {
            _introScreen.SetActive(false);
            _gamePlayScreen.SetActive(true);
        }

        public void OnBuildButtonPressed()
        {
            // toggle the build menu
            _buildMenu.SetActive(!_buildMenu.activeInHierarchy);
        }

        public void OnBuildMenuClosePressed()
        {
            _buildMenu.SetActive(false);
        }

        public void OnBuildStructureItemPressed(int structureIndex)
        {
            _buildMenu.SetActive(false);
            _placeStructureScreen.SetActive(true);
            _buildMenuButton.SetActive(false);
            _mapInteractibles.StartPlacingStructure((MapGameState.StructureType)structureIndex);
        }

        private void OnStructurePlaced(MapGameState.StructureType structureType)
        {
            _placeStructureScreen.SetActive(false);
            _buildMenuButton.SetActive(true);

            // only show winning screen on first placement of stronghold
            if (!_hasPlayerWon && structureType == MapGameState.StructureType.Stronghold)
            {
                _hasPlayerWon = true;
                _gameOverScreen.SetActive(true);
                _gamePlayScreen.SetActive(false);
            }
        }

        public void OnGameOverContinuePressed()
        {
            _gameOverScreen.SetActive(false);
            _gamePlayScreen.SetActive(true);
        }

        public void OnLandmarkFoundContinuePressed()
        {
            _landmarkFoundScreen.gameObject.SetActive(false);
            _gamePlayScreen.SetActive(true);
        }

        public void OnLandmarkFoundARPressed()
        {
            _landmarkFoundScreen.gameObject.SetActive(false);
            _gamePlayScreen.SetActive(false);
            _arModeScreen.SetActive(true);

            // enable the XR Origin & AR Session so we go into AR mode
            _XrOrigin.SetActive(true);
            _ArSession.SetActive(true);
            _compassUI.SetActive(true);

            // disable entire lightship map - otherwise it weirdly tries to draw the ground, roads etc in the world and they get in the way
            _lightshipMap.SetActive(false);
            _mapCamera.SetActive(false);
        }

        public void OnArModeBackPressed()
        {
            _arModeScreen.SetActive(false);
            _gamePlayScreen.SetActive(true);

            _XrOrigin.SetActive(false);
            _ArSession.SetActive(false);
            _compassUI.SetActive(false);

            _lightshipMap.SetActive(true);
            _mapCamera.SetActive(true);
        }

        public void OnGameplayVPSLocalisationPressed()
        {
            SceneManager.LoadScene("VPSLocalizationScene");
            //SceneManager.LoadScene("PlaybackRecordingScene");
        }

        public void OnGameplayOpenGalleryPressed()
        {
            _gamePlayScreen.SetActive(false);
            _galleryScreen.SetActive(true);
        }

        public void OnGalleryClosePressed()
        {
            _galleryScreen.SetActive(false);
            _gamePlayScreen.SetActive(true);
        }











}
//}
