using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Boid))]
public class BoidEditor : Editor
{
    private void OnSceneGUI()
    {
        Boid boid = (Boid)target;
        Handles.color = Color.white;
        //Handles.DrawWireCube(boid.transform.position, new Vector2(boid.viewRadius, boid.viewRadius));
        Vector3 viewAngleA = boid.DirFromAngle(-boid.viewAngle / 2);
        Vector3 viewAngleB = boid.DirFromAngle(boid.viewAngle / 2);

        Handles.DrawLine(boid.transform.position, boid.transform.position + viewAngleA * boid.viewRadius);
        Handles.DrawLine(boid.transform.position, boid.transform.position + viewAngleB * boid.viewRadius);

        Handles.color = Color.red;
        // Debug.Log(boid.neighbors.Count);
        if (boid.neighbors.Count > 0)
        {
            for (int i = 0; i < boid.neighbors.Count; i++)
            {
                Vector2 dir = boid.neighbors[i].transform.position - boid.transform.position;
                float angle = Vector2.Angle(dir, boid.transform.up);

                if (angle < boid.viewAngle / 2)
                {
                    Handles.DrawLine(boid.transform.position, boid.neighbors[i].transform.position, 5);
                }
            }
        }
    }
}
