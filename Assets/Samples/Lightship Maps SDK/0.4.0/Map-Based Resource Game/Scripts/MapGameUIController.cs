// Copyright 2022 Niantic, Inc. All Rights Reserved.

using System;
using System.Collections.Generic;
using Niantic.Lightship.Maps.SampleAssets.Player;
using Niantic.Lightship.Maps.Samples.GameSample;
using TMPro;
using UnityEngine;

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
        private TMP_Text _woodText;

        [SerializeField]
        private TMP_Text _planksText;

        [SerializeField]
        private TMP_Text _stoneText;

        [SerializeField]
        private TMP_Text _bricksText;

        [SerializeField]
        private TMP_Text _errorText;

        // keeps track if the player has already won or not
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
            var gpsInfo = Input.location.lastData; // use Unity's GPS system
                if (gpsInfo.timestamp > _lastGpsUpdateTime)
                {
                    _lastGpsUpdateTime = gpsInfo.timestamp;
                    CheckIfPlayerNearLocation(gpsInfo);
                }
        }

        private void CheckIfPlayerNearLocation(LocationInfo gpsInfo)
        {
            Dictionary<string, Tuple<float, float>> landmarks = new Dictionary<string, Tuple<float, float>>
                {
                    //{"John Britten North Entrance", Tuple.Create(-43.52046f, 172.58311f) },
                    {"John Britten East Entrance", Tuple.Create(-43.52069f, 172.58339f) },
                    {"Engcore Carpark Tree", Tuple.Create(-43.52118f, 172.58391f) },
                    {"EPS Entrance", Tuple.Create(-43.52128f, 172.58437f) },
                    {"Kauri Tree near Chemical Engineering", Tuple.Create(-43.52149f, 172.58451f) },
                    //{"Sir Robertson Stewart Statue", Tuple.Create(-43.52209f, 172.58308f) },
                };

            var current_lat = (float)gpsInfo.latitude; // convert doubles to floats: loses precision, but floats are good enough
            var current_lng = (float)gpsInfo.longitude;

            foreach (KeyValuePair<string, Tuple<float, float>> landmark in landmarks)
            {
                if (Vector3.Distance(new Vector3(current_lat, 0, current_lng), new Vector3(landmark.Value.Item1, 0f, landmark.Value.Item2)) < 0.0001) // 0.0001 degrees: within 11.1 metres. Phones are accurate to a few metres, so this threshold is acceptable.
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
            _gamePlayScreen.SetActive(true);
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
