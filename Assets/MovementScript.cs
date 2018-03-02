using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {

    public Rigidbody rb;

    public float forceMultiplier;
    public float pelleMultiplier;

    public Vector3 arrowVector;

    float clickStartPosX;
    float clickStartPosY;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown() {
        clickStartPosX = Input.mousePosition.x;
        clickStartPosY = Input.mousePosition.y;

    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = Input.mousePosition;

        arrowVector = new Vector3((clickStartPosX - mousePos.x) * forceMultiplier, 0, (clickStartPosY - mousePos.y) * forceMultiplier);

        Debug.DrawLine(rb.transform.position, arrowVector * pelleMultiplier);
    }

    void OnMouseUp()
    {

        rb.AddForce(arrowVector);
    }
}
