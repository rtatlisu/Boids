using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameController))]
public class GridEditor : Editor
{
    private void OnSceneGUI()
    {
        GameController gc = (GameController)target;
        Handles.color = Color.white;


        float xIt = -44.44f;
        //Debug.Log(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)));
        float yIt = -25.0f;
        float maxBoundX = 44.44f;
        float maxBoundY = 25.0f;
        Debug.Log(-44.44f / 3);
        // Debug.Log(maxBoundX);
        for (float i = yIt; i < maxBoundY; i++)
        {
            for (float j = xIt; j < maxBoundX; j++)
            {
                if (j * gc.cellSize >= xIt && j * gc.cellSize <= maxBoundX && i * gc.cellSize >= yIt && i * gc.cellSize <= maxBoundY)
                {
                    // Debug.Log(j * gc.cellSize + " <= " + maxBoundX);
                    //Vector2 cell = new Vector2(Mathf.FloorToInt(j * gc.cellSize), Mathf.FloorToInt(i * gc.cellSize));
                    Vector2 cell = new Vector2(j * gc.cellSize, i * gc.cellSize);
                    Handles.DrawWireCube(cell, new Vector2(gc.cellSize, gc.cellSize));
                }

            }

        }
    }
}
