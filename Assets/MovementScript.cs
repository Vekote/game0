using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {

    public Rigidbody rb;
    public Camera cam;

    public float forceMultiplier;
    private float launchForce;
    private Vector3 arrowVector;

    public LineRenderer lineRenderer;

    Vector3 clickStartPos;

	// Use this for initialization
	void Start () {
		    
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnMouseDown() {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);
        clickStartPos = transform.position;
        //Debug.Log(clickStartPos + " ja sit normi: " + cam.ScreenToWorldPoint(Input.mousePosition));
        //Debug.Log(transform.up);
    }

    private void OnMouseDrag() //TODO: min launchforce (jos alle ni ei lähe), max launchforce. kunnon indikaattori voimalle
    {
        Vector3 mousePos = GetCurrentMousePosition();

        launchForce = Vector3.Distance(mousePos, clickStartPos);
        Debug.Log(clickStartPos);
        arrowVector = (clickStartPos - mousePos) * forceMultiplier;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(1, transform.position + (transform.position - GetCurrentMousePosition()));



    }

    void OnMouseUp()
    {
        
        rb.AddForce(arrowVector);
        lineRenderer.enabled = false;
        lineRenderer.positionCount = 1;
    }

    private Vector3 GetCurrentMousePosition()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        var plane = new Plane(transform.position, transform.position + transform.right, transform.position + transform.forward);
        Debug.DrawLine(transform.position, transform.position + transform.right);
        Debug.DrawLine(transform.position + transform.right, transform.position + transform.forward);
        Debug.DrawLine(transform.position + transform.forward, transform.position);
        float rayDistance;
        if (plane.Raycast(ray, out rayDistance))
        {
            return ray.GetPoint(rayDistance);
        }

        throw new Exception("Error getting current mouse position");
    }
}
