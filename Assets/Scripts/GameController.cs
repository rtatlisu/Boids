using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [Range(0, 0.2f)]
    public float boidSpeed;
    [Tooltip("1: unmodified, >1: stronger repel")]
    [Range(1, 5)]
    public float separationStrength;


    public float turnSpeed;
    public int rows;
    public int columns;
    public int numOfBoids;
    public GameObject boidPrefab;
    private Vector2 minBounds;
    private Vector2 maxBounds;
    public float cellSize;
    public bool separation;
    public bool alignment;
    public bool cohesion;

    [Tooltip("higher value -> separation starts earlier")]
    [Range(10, 2f)]
    public float separationRadius;

    [Tooltip("higher value -> alignment starts earlier")]
    [Range(10, 2f)]
    public float alignmentRadius;
    [Tooltip("higher value -> cohesion starts earlier")]
    [Range(10, 2f)]
    public float cohesionRadius;
    public List<GameObject> neighbors;
    private Dictionary<Vector2, List<GameObject>> grid = new Dictionary<Vector2, List<GameObject>>();

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
        neighbors = new List<GameObject>();
    }
    // Start is called before the first frame update
    void Start()
    {
        minBounds = -Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        maxBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        float spawnOffset = 2.0f;

        for (int i = 0; i < numOfBoids; i++)
        {
            float rndX = Random.Range(minBounds.x + spawnOffset, maxBounds.x - spawnOffset);
            float rndY = Random.Range(minBounds.y + spawnOffset, maxBounds.y - spawnOffset);

            GameObject boid = Instantiate(boidPrefab);
            boid.transform.position = new Vector3(rndX, rndY);
            boid.name = "Boid " + (i + 1);

            rndX = Random.Range(-1.0f, 1.0f);
            rndY = Random.Range(-1.0f, 1.0f);
            Quaternion q = Quaternion.LookRotation(Vector3.forward, new Vector2(rndX, rndY));
            boid.transform.rotation = q;

            boid.tag = "Boid";




        }




    }

    // Update is called once per frame
    void Update()
    {

        grid.Clear();
        AssignBoidsToGrid();

        /*      bool update = false;
              foreach (KeyValuePair<Vector2, List<GameObject>> pair in grid)
              {

                  foreach (GameObject boid in pair.Value)
                  {

                      if (boid.GetComponent<Boid>().updateCells)
                      {
                          update = true;
                          break;
                      }
                      else
                      {
                          update = false;
                      }
                  }
              }
      */
        //   if (update)
        //  {
        foreach (KeyValuePair<Vector2, List<GameObject>> pair in grid)
        {

            foreach (GameObject boid in pair.Value)
            {

                neighbors = FindNeighbors(boid);
                boid.GetComponent<Boid>().neighbors = neighbors;
            }
        }
        //  }
    }



    private void AssignBoidsToGrid()
    {
        GameObject[] boids = GameObject.FindGameObjectsWithTag("Boid");
        for (int i = 0; i < boids.Length; i++)
        {

            GameObject boid = boids[i];
            Vector2 pos = boid.transform.position;
            Vector2 cell = new Vector2(Mathf.FloorToInt(pos.x / cellSize), Mathf.FloorToInt(pos.y / cellSize));
            if (boids[i].GetComponent<Boid>().currentCell == cell)
            {
                boids[i].GetComponent<Boid>().updateCells = false;

            }
            else
            {
                boids[i].GetComponent<Boid>().updateCells = true;
                boids[i].GetComponent<Boid>().currentCell = cell;
            }
            if (!grid.ContainsKey(cell))
            {
                grid[cell] = new List<GameObject>();
            }
            grid[cell].Add(boid);

        }
    }

    private List<GameObject> FindNeighbors(GameObject boid)
    {
        List<GameObject> neighbors = new List<GameObject>();
        Vector2 pos = boid.transform.position;
        Vector2 cell = new Vector2(Mathf.FloorToInt(pos.x / cellSize), Mathf.FloorToInt(pos.y / cellSize));


        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector2 neighborCell = cell + new Vector2(x, y);

                if (grid.ContainsKey(neighborCell))
                {


                    foreach (GameObject neighborBoid in grid[neighborCell])
                    {
                        if (neighborBoid != boid) // Exclude the boid itself
                        {
                            Vector2 dir = neighborBoid.transform.position - boid.transform.position;
                            float angle = Vector2.Angle(dir, boid.transform.up);

                            if (angle < neighborBoid.GetComponent<Boid>().viewAngle / 2)
                            {
                                neighbors.Add(neighborBoid);
                            }
                        }

                    }

                }

            }
        }

        return neighbors;
    }




}
