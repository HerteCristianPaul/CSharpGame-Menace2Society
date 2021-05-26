using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;                                
    public Transform target;
    private float startFOV, targetFOV;
    public float zoomSpeed = 5f;
    public Camera theCam;

    public void Awake() 
    {
        instance = this;                        // Makes this script accessible from another
    }

    // Start is called before the first frame update
    public void Start()
    {
        startFOV = theCam.fieldOfView;                                  // Starts with the normal field of view &
        targetFOV = startFOV;                                           //  fixes the forever zoom bug when we start the game
    }

    // LateUpdate() = It starts after all the updates are done
    // CameraPoint follows and rotate after Main Camera
    public void LateUpdate()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
        // Moves a value from started to other value using increment | it moves slower towards the target value
        theCam.fieldOfView = Mathf.Lerp(theCam.fieldOfView, targetFOV, zoomSpeed*Time.deltaTime);                
    }

    public void ZoomIn(float newZoom)
    {
        targetFOV = newZoom;                                            // Zooms in till a specific point(newZoom variable)
    }

    public void ZoomOut()
    {
        targetFOV = startFOV;                           // Zooms out till the standard field of view
    }
}
