using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCanvas : MonoBehaviour
{
    public GameObject Camera;
    public Canvas canvas;

    public float offSetPlaneDistance = 0.2f;

    private void Start()
    {
        canvas.worldCamera = Camera.gameObject.GetComponent<Camera>();
        canvas.planeDistance = Camera.gameObject.GetComponent<Camera>().nearClipPlane + offSetPlaneDistance;
    }
}
