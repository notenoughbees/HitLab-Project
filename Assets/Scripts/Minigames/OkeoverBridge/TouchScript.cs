using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScript : MonoBehaviour
    {
        public static event Action CaddisflyCountIncreased = delegate { };

        private MinigameOkeoverBridgeUIController uiScript;

        public GameObject caddisflyPrefab; // a prefab, not a sprite
        public AudioSource eggHatchSound;
        public AudioSource spiderSquishSound;

        void Start()
        {
            Debug.Log("TouchScript START");
            uiScript = FindObjectOfType<MinigameOkeoverBridgeUIController>();

            SwipeDetection.instance.swipePerformed += HandleSwipe;
        }

        void Update()
        {
            //Debug.Log("uiScript.isPlayingGame: " + uiScript.isPlayingGame);
            if (uiScript.isPlayingGame && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
                RaycastHit hit;
                Debug.DrawRay(ray.origin, ray.direction * 200, Color.yellow, 100f);

                if (Physics.Raycast(ray, out hit))
                {
                    GameObject sprite;
                    switch (hit.collider.tag)
                    {
                        // if we touched an egg sprite, delete it, play a sound, & replace it with a caddisfly sprite
                        case "Egg":
                            Debug.Log("touched an egg");
                            sprite = hit.transform.gameObject;
                            Destroy(sprite);

                            eggHatchSound.Play();

                            Vector3 eggPos = hit.collider.transform.position;
                            Debug.Log("eggPos: " + eggPos.ToString());
                            SpawnCaddisfly(eggPos);

                            CaddisflyCountIncreased?.Invoke(); // trigger the event
                            break;

                        default:
                            Debug.Log("touched something else (not egg)");
                            break;
                    }
                }
                else
                {
                    Debug.Log("didn't hit anything");
                }
            }
        }


        // While eggs must be touched to do stuff, spiders must be swiped (mimics swiping away a spiderweb)
        private void HandleSwipe(Vector2 direction)
        {
            Debug.Log("HANDLESWIPE...");
            if (uiScript.isPlayingGame && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)  //
            {                                                                                                   //
                Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);                              //
                RaycastHit hit;                                                                                 //
                                                                                                                //
                if (Physics.Raycast(ray, out hit))                                                              // same code as for touching
                {
                    // if we swiped on a spider, delete it & play a sound
                    if (hit.collider.tag == "Spider")
                    {
                        Debug.Log("swiped on a spider");
                        GameObject sprite = hit.transform.gameObject;
                        Destroy(sprite);

                        spiderSquishSound.Play();
                    }
                    else
                    {
                        Debug.Log("swiped on something else (not spider)");
                    }
                }
                else
                {
                    Debug.Log("didn't swipe anything");
                }
            }

        }


        private void SpawnCaddisfly(Vector3 objPos)
        {
            Debug.Log("spawning a caddisfly at " + objPos + "...");
            GameObject newCaddisfly = Instantiate(caddisflyPrefab, objPos, Quaternion.identity);

            // set the fly's hatching position because the fly will centre around here when it flies around
            CaddisflyBehaviour moveScript = newCaddisfly.GetComponent<CaddisflyBehaviour>();
            moveScript.SetHatchPosition(objPos);
        }


        void OnDestroy()
        {
            // Unsubscribe from swipe event when this script is destroyed
            if (SwipeDetection.instance != null)
            {
                SwipeDetection.instance.swipePerformed -= HandleSwipe;
            }
        }
}
