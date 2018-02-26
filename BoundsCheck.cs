using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour {
    [Header("Set in Inspector")] // variables to be set-able at start in inspector
    public float radius = 1f;
    public bool keepOnScreen = true;

    [Header("Set Dynamically")] // variables to be set-able at start and are not fixed
    public bool isOnScreen = true;
    public float camWidth;
    public float camHeight;
    [HideInInspector]
    public bool offRight, offLeft, offUp, offDown;

    void Awake() // at start,
    {
        // set camera to perspective and aspect ratio
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    void LateUpdate()
    {
        // initialize vars
        Vector3 pos = transform.position;
        isOnScreen = true;
        offRight = offLeft = offUp = offDown = false;

        if (pos.x > camWidth - radius) // if off screen to the right,
        {
            pos.x = camWidth - radius; // reset pos.x
            isOnScreen = false; // off screen
            offRight = true; // off right true
        }
        if (pos.x < -camWidth + radius) // etc
        {
            pos.x = -camWidth + radius;
            isOnScreen = false;
            offLeft = true;
        }
        if (pos.y > camHeight - radius)
        {
            pos.y = camHeight - radius;
            isOnScreen = false;
            offUp = true;
        }
        if (pos.y < -camHeight + radius)
        {
            pos.y = -camHeight + radius;
            isOnScreen = false;
            offDown = true;
        }
        isOnScreen = !(offRight || offLeft || offUp || offDown); // if off screen, on screen is false
        if(keepOnScreen && !isOnScreen) // if on screen is false and the inspector set keepOnScreen to true,
        {
            transform.position = pos; // reset position
            isOnScreen = true; // put onscreen
            offRight = offLeft = offUp = offDown = false; // make off-flags all false
        }
        transform.position = pos;
    }

    // Draw the bounds in the Scene pane using OnDrawGizmos()
    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(camWidth * 2, camHeight * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
