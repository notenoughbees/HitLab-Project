using UnityEngine;
using UnityEngine.UI;
using Niantic.Lightship.AR.VpsCoverage;

public class LoadVPSHintImage : MonoBehaviour
{
    [Tooltip("VPS Coverage list manager")]
    [SerializeField]
    private VpsCoverageTargetListManager _vpsCoverageTargetListManager;

    public RawImage rawImage;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("LoadVPSHintImage Start()");

        // Subscribe to the Target Set event from the VPS target manager
        _vpsCoverageTargetListManager.OnCurrentTargetSet += HandleTargetSet;

        if (_vpsCoverageTargetListManager.GetCurrentTarget().Equals(null))
        {
            Debug.LogError("ERROR: _currentTarget is null.");
            return;
        }
    }


    private void HandleTargetSet(LocalizationTarget target)
    {
        Debug.Log("Target is set, downloading image...");

        string url = target.ImageURL;

        if (!string.IsNullOrEmpty(url))
        {
            Debug.Log($"URL: {url}");

            // Download the image
            CoverageClientManager _coverageClientManager = _vpsCoverageTargetListManager.GetCoverageClientManager();
            _coverageClientManager.TryGetImageFromUrl(url, downLoadedImage =>
            {
                if (downLoadedImage != null)
                {
                    rawImage.texture = downLoadedImage;
                }
                else
                {
                    Debug.LogError("ERROR: Couldn't download image.");
                }
            });
        }
        else
        {
            Debug.LogError("ERROR: No valid URL found for the current target.");
        }
    }


    private void OnDestroy()
    {
        // Unsubscribe from the event when this object is destroyed
        _vpsCoverageTargetListManager.OnCurrentTargetSet -= HandleTargetSet;
    }
}
