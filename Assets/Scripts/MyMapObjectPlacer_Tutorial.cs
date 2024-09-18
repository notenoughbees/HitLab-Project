using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Niantic.Lightship.Maps.Core.Coordinates;
using Niantic.Lightship.Maps.MapLayers.Components;
using Niantic.Lightship.Maps;

public class MyMapObjectPlacer_Tutorial : MonoBehaviour
{
    // from tutorial: https://lightship.dev/docs/maps/unity/how-to/place_objects_on_map
    [SerializeField]
    private LayerGameObjectPlacement _objectSpawner;

    private Camera _mapCamera;

    // TAKEN FROM MapGameInteractions.cs
    [SerializeField]
    private LightshipMapView _lightshipMapView;



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("MyMapObjectPlacer2 START");
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
            Debug.Log("MyMapObjectPlacer2 clicked or touched");

            Vector2 touchPosition = Input.mousePosition;
            PlaceObject(touchPosition);
        }
    }



    public void PlaceObject(Vector3 touchPosition)
    {
        // from tutorial: https://lightship.dev/docs/maps/unity/how-to/place_objects_on_map
        var location = ScreenPointToLatLong(touchPosition);
        var cameraForward = _mapCamera.transform.forward;
        var forward = new Vector3(cameraForward.x, 0f, cameraForward.z).normalized;
        var rotation = Quaternion.LookRotation(forward);

        _objectSpawner.PlaceInstance(location, rotation);
        Debug.Log("MyMapObjectPlacer2 PlaceInstance()");
    }


    // TAKEN FROM MapGameInteractions.cs
    private LatLng ScreenPointToLatLong(Vector3 screenPosition)
    {
        var clickRay = _mapCamera.ScreenPointToRay(screenPosition);
        var pointOnMap = clickRay.origin + clickRay.direction * (-clickRay.origin.y / clickRay.direction.y);
        return _lightshipMapView.SceneToLatLng(pointOnMap);
    }
}
