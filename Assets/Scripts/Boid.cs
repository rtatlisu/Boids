using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public float viewRadius;
    public float viewAngle;
    public List<GameObject> neighbors;
    // Start is called before the first frame update
    void Start()
    {
        neighbors = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += new Vector3(0.01f, 0, 0);
        /*   float singleStep = 200f * Time.deltaTime;
           Vector3 newDir = Vector3.RotateTowards(transform.position, new Vector3(0.01f, 0, 0), singleStep, 0.0f);
           newDir = new Vector3(0, 3, 0) - transform.position;

           Debug.DrawRay(transform.position, newDir, Color.red);
           Quaternion q = Quaternion.LookRotation(Vector3.forward, newDir);

           //issue: need to make the sprite point along thhe y axis (green)

            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, singleStep);

   */
        // transform.position += transform.up * 0.08f;


        WrapAround();



    }

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

    public void Separation()
    {

    }

    private float CalculateVisionTreshold(int visionConeAngle)
    {
        //    print("1 " + (visionConeAngle / 2f));
        //   print("2 " + Mathf.Deg2Rad * (visionConeAngle / 2f));
        //  print("3 " + Mathf.Cos(Mathf.Deg2Rad * (visionConeAngle / 2f)));
        return Mathf.Cos(180 / 2f * Mathf.Deg2Rad);

    }

    public Vector2 DirFromAngle(float angleInDegrees)
    {
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }


}
