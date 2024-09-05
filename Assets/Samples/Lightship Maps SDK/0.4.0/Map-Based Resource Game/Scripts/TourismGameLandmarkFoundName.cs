using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Niantic.Lightship.Maps.SampleAssets.Player;

namespace Niantic.Lightship.Maps.Samples.GameSample
{
    /// <summary>
    /// UI menu item to show buildings to be built, checks if there is enough resources to build this structure
    /// </summary>
    internal class TourismGameLandmarkFoundName : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _landmarkNameText;

        private PlayerLocationController playerLocationController;


        private void OnEnable()
        {
            playerLocationController = FindObjectOfType<PlayerLocationController>();
            string landmarkName = playerLocationController.getLandmarkName();
            _landmarkNameText.text = landmarkName;
        }

        public void changeLandmarkName(string n)
        {
            _landmarkNameText.text = n;
        }
    }
}
