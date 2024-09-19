////namespace Niantic.Lightship.Maps.Builders.Standard.Objects
////{
////using PooledObjectDictionary = System.Collections.Generic.Dictionary<UnityEngine.GameObject, Niantic.Lightship.Maps.ObjectPools.PooledObject<UnityEngine.GameObject>>;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using JetBrains.Annotations;
//using Niantic.Lightship.Maps.Core;
//using Niantic.Lightship.Maps.Core.Features;
//using Niantic.Lightship.Maps.ObjectPools;
//using Niantic.Lightship.Maps.Utilities;
//using Niantic.Lightship.Maps.Builders.Standard;
//using Niantic.Lightship.Maps.Builders.Standard.Objects;
//using Niantic.Lightship.Maps;
////using Unity.XR.CoreUtils;
//using System.Linq;

///// <summary>
///// An <see cref="ObjectBuilderStandard"/> used to place object
///// instances from <see cref="IAreaFeature"/> features.
///// </summary>
//public class MyLinearObjectBuilder : ObjectBuilderStandard
//{
//    [Tooltip("A prefab or GameObject that this builder " +
//        "will instantiate when placing new objects.")]
//    [SerializeField]
//    private GameObject _prefab;

//    private static ChannelLogger Log { get; } = new(nameof(AreaObjectBuilder));

//    private ILightshipMapView _lightshipMapView;
//    private Niantic.Lightship.Maps.ObjectPools.ObjectPool<GameObject> _objectPool;
//    //private readonly PooledObjectDictionary _pooledObjects = new();
//    private readonly Dictionary<GameObject, PooledObject<GameObject>> _pooledObjects = new();

//    /// <inheritdoc />
//    public override void Initialize(ILightshipMapView lightshipMapView)
//    {
//        base.Initialize(lightshipMapView);
//        _lightshipMapView = lightshipMapView;
//        _objectPool = new Niantic.Lightship.Maps.ObjectPools.ObjectPool<GameObject>(_prefab, onAcquire: OnObjectAcquired, onRelease: OnObjectReleased);
//    }

//    private void OnObjectAcquired(PooledObject<GameObject> pooledObject)
//    {
//        var featureInstance = pooledObject.Value;
//        _pooledObjects.Add(featureInstance, pooledObject);

//        // Enable and un-hide this object (if it was pooled)
//        GameObjectUtils.EnableAndShow(featureInstance);
//    }

//    private static void OnObjectReleased(GameObject poolGameObject)
//    {
//        // Detach this child object from its parent,
//        // disable it, and hide it in the hierarchy.
//        GameObjectUtils.DisableAndHide(poolGameObject);
//    }

//    /// <inheritdoc />
//    protected override void BuildFeature(IMapTile mapTile, GameObject parent, IMapTileFeature feature)
//    {
//        if (feature is ILinearFeature linearFeature)
//        {
//            // Get or create a prefab instance from the object pool
//            var pooledObject = _objectPool.GetOrCreate();
//            var featureInstance = pooledObject.Value;

//            /*
//            // Get the object's position, rotation, and scale
//            var position = GetObjectPosition(areaFeature);
//            var rotation = GetObjectRotation(areaFeature);
//            var localScale = GetObjectScale(areaFeature);

//            // Adjust the object's local scale relative to our parent maptile
//            var scaleFactor = 1.0f / (_lightshipMapView.MapScale * mapTile.Size);
//            var tileScale = (float)scaleFactor * _prefab.transform.localScale;
//            localScale.Scale(tileScale);

//            // Hook this up to the parent and set its local transform
//            featureInstance.transform.SetParent(parent.transform, false);
//            featureInstance.transform.localPosition = position + ZOffset;
//            featureInstance.transform.localRotation = rotation;
//            featureInstance.transform.localScale = localScale;
//            */

//            // Get the line points
//            var points = linearFeature.Points;

//            // Iterate over each point and place an object there
//            for (int i = 0; i < points.Length - 1; i++)
//            {
//                // Get the start and end of the current line segment
//                var start = points[i];
//                var end = points[i + 1];

//                // Calculate the mid-point of the line segment (or place along the entire line if desired)
//                var midPoint = (start + end) / 2f;

//                // Set the object's position, rotation, and scale
//                var position = GetObjectPosition(midPoint, mapTile);
//                var rotation = Quaternion.LookRotation(end - start); // Align the object along the line segment
//                var localScale = _prefab.transform.localScale;

//                // Hook the object to the parent and set the local transform
//                featureInstance.transform.SetParent(parent.transform, false);
//                featureInstance.transform.localPosition = position;
//                featureInstance.transform.localRotation = rotation;
//                featureInstance.transform.localScale = localScale;
//            }
//        }
//    }


//    // Helper method to calculate object position
//    private Vector3 GetObjectPosition(Vector3 point, IMapTile mapTile)
//    {
//        // Scale position based on the map tile, ensure all double values are cast to float
//        var scaledPosition = point / (float)(_lightshipMapView.MapScale * mapTile.Size);

//        // Add the ZOffset (make sure ZOffset is also a Vector3)
//        return scaledPosition + ZOffset;
//    }


//    // Override Release to handle object release logic
//    public override void Release(GameObject parent)
//    {
//        // Custom release logic for linear features
//        while (parent.transform.childCount > 0)
//        {
//            var child = parent.transform.GetChild(0);
//            var childGameObject = child.gameObject;

//            if (!_pooledObjects.Remove(childGameObject, out var pooledObject))
//            {
//                // If not found in the pool, destroy it
//                childGameObject.transform.SetParent(null, false);
//                Destroy(childGameObject);
//                continue;
//            }

//            // Return the GameObject to the pool
//            pooledObject.Dispose();
//        }
//    }



//    /*
//    /// <inheritdoc />
//    public override void Release(GameObject parent)
//        {
//            while (parent.transform.childCount > 0)
//            {
//                var child = parent.transform.GetChild(0);
//                var childGameObject = child.gameObject;

//                if (!_pooledObjects.Remove(childGameObject, out var pooledObject))
//                {
//                    // If we can't find this child in our list of pooled objects,
//                    // just destroy it rather than releasing it back to the pool
//                    Log.Warning("Couldn't find a pooled object to release!");
//                    childGameObject.transform.SetParent(null, false);
//                    Destroy(childGameObject);
//                    continue;
//                }

//                // Return the GameObject to the pool
//                pooledObject.Dispose();
//            }
//        }

//        /// <inheritdoc />
//        protected override Vector3 GetObjectPosition(IMapTileFeature feature)
//        {
//            if (feature is not IAreaFeature areaFeature)
//            {
//                // This method should only be called for area features,
//                // so if we somehow got a different feature type, log
//                // an error and return a default value of Vector3.zero.

//                var type = feature.GetType().Name;
//                Log.Error($"Feature '{type}' is not an area feature!");
//                return Vector3.zero;
//            }

//            // Return the area feature's centroid for our object's position
//            return MeshBuilderUtils.CalculateCentroid(areaFeature.Points);
//        }
//    */
//}


////}






//public static class GameObjectUtils
//{
//    public static void EnableAndShow(GameObject gameObject)
//    {
//        if (gameObject != null)
//        {
//            // Enable the GameObject
//            gameObject.SetActive(true);

//            // Un-hide by setting its renderer to be visible, if necessary
//            var renderers = gameObject.GetComponentsInChildren<Renderer>();
//            foreach (var renderer in renderers)
//            {
//                renderer.enabled = true;  // Make sure the renderer is enabled
//            }

//            // If using UI components like Canvas, ensure those are enabled too
//            var canvases = gameObject.GetComponentsInChildren<Canvas>();
//            foreach (var canvas in canvases)
//            {
//                canvas.enabled = true;
//            }
//        }
//    }

//    public static void DisableAndHide(GameObject gameObject)
//    {
//        if (gameObject != null)
//        {
//            // Disable the GameObject
//            gameObject.SetActive(false);

//            // Optionally, hide the object by disabling its renderer components
//            var renderers = gameObject.GetComponentsInChildren<Renderer>();
//            foreach (var renderer in renderers)
//            {
//                renderer.enabled = false;  // Disable the renderer to hide the object
//            }

//            // If using UI components like Canvas, disable those as well
//            var canvases = gameObject.GetComponentsInChildren<Canvas>();
//            foreach (var canvas in canvases)
//            {
//                canvas.enabled = false;
//            }
//        }
//    }
//}


