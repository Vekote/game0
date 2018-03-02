using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {

    public Rigidbody rb;
    public Camera cam;

    public float forceMultiplier;
    public float launchForce;

    public Vector3 arrowVector;
    public LineRenderer lineRenderer;

    float clickStartPosX;
    float clickStartPosY;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown() {
        clickStartPosX = cam.WorldToScreenPoint(rb.transform.position).x;
        clickStartPosY = cam.WorldToScreenPoint(rb.transform.position).y;

        Debug.Log(cam.WorldToScreenPoint(rb.transform.position));
    }

    private void OnMouseDrag() //TODO: min launchforce (jos alle ni ei lähe), max launchforce. kunnon indikaattori voimalle
    {
        Vector3 mousePos = Input.mousePosition;
        //Debug.Log(clickStartPosX + ", " + mousePos.x + " : " + clickStartPosY + ", " + mousePos.y);

        arrowVector = new Vector3((clickStartPosX - mousePos.x) * forceMultiplier, 0, (clickStartPosY - mousePos.y) * forceMultiplier);

        launchForce = arrowVector.magnitude;

        Debug.DrawLine(arrowVector, rb.transform.position);
        }

    void OnMouseUp()
    {
        rb.AddForce(arrowVector);
    }
}
