using Niantic.Lightship.Maps.Core.Coordinates;
using Niantic.Lightship.Maps.MapLayers.Components;
using UnityEngine;

public class MyMapObjectPlacerManager : MonoBehaviour
{
    // from tutorial: https://lightship.dev/docs/maps/unity/how-to/place_objects_on_map
    [SerializeField]
    private LayerGameObjectPlacement _objectSpawner;

    private Camera _mapCamera;



    // Start is called before the first frame update
    void Start()
    {
        _mapCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // from Ball Workshop: https://lightship.dev/docs/ardk/how-to/ar/meshing_physics_real_world/#testing-mesh-physics
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
#else
        if (Input.touchCount > 0)
#endif
        {
            Debug.Log("clicked or touched");

            Vector2 touchPosition = Input.mousePosition;
            PlaceObject(touchPosition);
        }
    }



    public void PlaceObject(Vector2 touchPosition) {
        // from tutorial: https://lightship.dev/docs/maps/unity/how-to/place_objects_on_map
        var location = ScreenPointToLatLong(touchPosition);
        var cameraForward = _mapCamera.transform.forward;
        var forward = new Vector3(cameraForward.x, 0f, cameraForward.z).normalized;
        var rotation = Quaternion.LookRotation(forward);

        _objectSpawner.PlaceInstance(location, rotation);
    }

    private LatLng ScreenPointToLatLong(Vector2 screenPoint)
    {
        return new LatLng(screenPoint.x, screenPoint.y);
    }

}
