using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{

    public Rigidbody rb;
    public Camera cam;

    public float forceMultiplier;
    public float maxLaunchForce;
    public float minLaunchForce;
    private float launchForce;
    private Vector3 launchDirection;

    public LineRenderer lineRenderer;

    Vector3 clickStartPos;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Quaternion q = transform.rotation;
        q.eulerAngles = new Vector3(q.eulerAngles.x, 0, q.eulerAngles.z);
        transform.rotation = q;
    }

    void OnMouseDown()
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);
        clickStartPos = transform.position;

    }

    private void OnMouseDrag() 
    {
        Vector3 mousePos = GetCurrentMousePosition();
        Vector3 guideLinePos = clickStartPos + (clickStartPos - mousePos);

        launchForce = Vector3.Distance(mousePos, clickStartPos);;
        if (launchForce > maxLaunchForce) launchForce = maxLaunchForce;
        else if (launchForce < minLaunchForce) launchForce = 0f;
        
        launchDirection = (clickStartPos - mousePos);

        Ray ray = new Ray(clickStartPos, guideLinePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, launchForce))
        {
            var angle = Vector3.Angle(hit.normal, transform.up);
            if (angle < 45)
            {
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(1, hit.point);
                //TODO: laske jotenkin perkeleellä ton kulman avulla toi seuraava piste.. Nyt pysähtyy seinään oikein, mut myös ylämäessä pysähtyy,vaiks pitäis jatkaa matkaa ton muuttujan angle-kulmalla.
                //lineRenderer.positionCount = 3;
                //lineRenderer.SetPosition(2, ylämäessä-oleva-piste-hmm);
            }
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(1, hit.point);

        }
        else
        {
            Debug.Log(launchForce);
            lineRenderer.positionCount = 2;
            Vector3 direction = guideLinePos - clickStartPos;
            lineRenderer.SetPosition(1, lineRenderer.GetPosition(0) + direction.normalized * launchForce);
        }


    }

    void OnMouseUp()
    {
        if (launchForce > 1)
            rb.AddForce(launchDirection.normalized * launchForce * forceMultiplier);
        lineRenderer.enabled = false;
        lineRenderer.positionCount = 1;
    }

    private Vector3 GetCurrentMousePosition()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(transform.position, transform.position + transform.right, transform.position + transform.forward);
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
