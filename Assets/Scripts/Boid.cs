using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boid : MonoBehaviour
{
    public float viewRadius;
    public float viewAngle;
    public Vector2 currentCell;
    [HideInInspector]
    public bool updateCells;

    public List<GameObject> neighbors;
    // Start is called before the first frame update
    void Start()
    {
        neighbors = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += transform.up * GameController.instance.boidSpeed;


        WrapAround();


        if (neighbors.Count > 0)
        {
            Vector3 direction = Vector3.zero;
            if (GameController.instance.separation)
            {
                direction += Separation();
            }
            if (GameController.instance.alignment)
            {
                direction += Alignment();
            }
            if (GameController.instance.cohesion)
            {
                direction += Cohesion();
            }
            if (direction != Vector3.zero)
            {
                Quaternion q = Quaternion.LookRotation(Vector3.forward, direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, q, GameController.instance.turnSpeed);
            }


        }





    }

    //makes the boids appear on the other side of the screen if they exit bounds
    private void WrapAround()
    {

        // Debug.DrawRay(transform.position, -transform.up * 20, Color.green);

        //print(GetLength(-transform.up * 20));
        // Get the object's rotation in euler angles
        Vector3 eulerRotation = transform.rotation.eulerAngles;

        // Convert the rotation to radians
        float angleInRadiansZ = Mathf.Deg2Rad * eulerRotation.z;
        float angleInRadiansX = Mathf.Deg2Rad * eulerRotation.x;



        // Calculate the direction vector
        Vector3 directionVector = new Vector3(Mathf.Sin(angleInRadiansX), 0, Mathf.Cos(angleInRadiansZ));

        //print(directionVector);
        Vector2 limit = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        bool outOfBoundsX = Mathf.Abs(transform.position.x) > Mathf.Abs(limit.x);
        bool outOfBoundsY = Mathf.Abs(transform.position.y) > Mathf.Abs(limit.y);

        if (outOfBoundsX && outOfBoundsY)
        {
            transform.position = new Vector2(transform.position.x * -1, transform.position.y * -1);
        }
        else if (outOfBoundsX)
        {
            transform.position = new Vector2(transform.position.x * -1, transform.position.y);
        }
        else if (outOfBoundsY)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y * -1);
        }
    }

    private Vector3 Separation()
    {
        Vector3 direction = transform.up;
        for (int i = 0; i < neighbors.Count; i++)
        {
            if (Vector3.Distance(neighbors[i].transform.position, transform.position) < GameController.instance.separationRadius)
            {
                float ratio = GameController.instance.separationStrength / Vector3.Distance(neighbors[i].transform.position, transform.position);
                direction -= ratio * (neighbors[i].transform.position - transform.position);
            }
        }
        // Mathf.Clamp01((neighbors[0].transform.position - transform.position).magnitude)
        return direction.normalized;
        // Quaternion q = Quaternion.LookRotation(Vector3.forward, direction);


        // transform.rotation = Quaternion.RotateTowards(transform.rotation, q, GameController.instance.turnSpeed);


    }

    private Vector3 Alignment()
    {
        Vector3 avgDirection = Vector3.zero;
        for (int i = 0; i < neighbors.Count; i++)
        {
            if (Vector3.Distance(neighbors[i].transform.position, transform.position) < GameController.instance.alignmentRadius)
            {
                avgDirection += neighbors[i].transform.up;
            }
        }
        return (avgDirection /= neighbors.Count).normalized;

        // Quaternion q = Quaternion.LookRotation(Vector3.forward, avgDirection);


        // transform.rotation = Quaternion.RotateTowards(transform.rotation, q, GameController.instance.turnSpeed);
    }

    private Vector3 Cohesion()
    {
        Vector3 avgPosition = Vector3.zero;
        for (int i = 0; i < neighbors.Count; i++)
        {

            float ratio = 1 / Vector3.Distance(neighbors[i].transform.position, transform.position);
            avgPosition += neighbors[i].transform.position;

        }
        avgPosition /= neighbors.Count;


        Vector3 direction = avgPosition - transform.position;
        return direction.normalized;

        // Quaternion q = Quaternion.LookRotation(Vector3.forward, direction);


        //transform.rotation = Quaternion.RotateTowards(transform.rotation, q, GameController.instance.turnSpeed);

    }


    //calculates a direction vector based on the angle around the forward vector of the boid
    public Vector2 DirFromAngle(float angleInDegrees)
    {
        angleInDegrees -= transform.eulerAngles.z;
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }


}
