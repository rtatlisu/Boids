using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board instance;
    public GameObject quadPrefab;
    public GameObject boidPrefab;
    private int[,] tilePositions;
    private List<Vector2> tiles;

    public float topLimit;
    public float bottomLimit;

    public float leftLimit;
    public float rightLimit;

    void Awake()
    {


        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        tiles = new List<Vector2>();


    }
    void Start()
    {
        tilePositions = new int[GameController.instance.rows, GameController.instance.columns];
    }

    public void SpawnBoard(int rows, int columns)
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject go = Instantiate(quadPrefab, this.transform);
                go.transform.position = new Vector3(j, i, 0);
                go.name = "Quad " + ((i * columns) + j);
                tilePositions[j, i] = (i * columns) + j;
                tiles.Add(new Vector2(j, i));

                if (i == 0 && j == 0)
                {
                    leftLimit = go.transform.position.x;
                    bottomLimit = go.transform.position.y;
                }
                else if (i == columns - 1 && j == rows - 1)
                {
                    topLimit = go.transform.GetChild(0).GetComponent<MeshRenderer>().bounds.max.y;
                    rightLimit = go.transform.GetChild(0).GetComponent<MeshRenderer>().bounds.max.x;

                }


            }
        }
    }

    public void SpawnBoid()
    {
        int index = Random.Range(0, tiles.Count - 1);
        Vector2 pos = tiles[index];


        GameObject go = Instantiate(boidPrefab, this.transform);
        go.transform.position = pos;
    }

    public bool WithinBounds(Vector2 pos)
    {

        if (pos.x > leftLimit && pos.x < rightLimit && pos.y > bottomLimit && pos.y < topLimit)
        {

            return true;
        }
        print("out of bounds");
        return false;
    }




}
