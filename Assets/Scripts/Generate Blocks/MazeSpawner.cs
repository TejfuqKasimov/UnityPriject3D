using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public GameObject cellPrefab;
    public GameObject blockPrefab;
    private GameObject currentBlock;
    private GameObject previousBlock;
    private GameObject previousPrBlock;
    public GameObject startRoomPrefab;
    private GameObject startRoom;
    private float Rast = 5.5f;
    private float PlayerRast = 5.5f;
    private float PlayerPrRast = 5.5f;
    private const float Wight = 3.4f;
    private const float Height = 3.4f;
    private int CountWight = 5;
    private int CountHeight = 5;
    private GameObject[,] currentGOMaze = new GameObject[5, 5];
    private GameObject[,] previousGOMaze = new GameObject[5, 5];
    private GameObject[,] previousPrGOMaze = new GameObject[5, 5];
    public Transform Character;
    private int LevelCount = 0;
    [SerializeField] private bool isEnd = false;
    public GameObject WayPrefab;
    private GameObject[,] currentPath = new GameObject[11, 11];
    private GameObject[,] previousPath = new GameObject[11, 11];
    private GameObject[,] previousPrPath = new GameObject[11, 11];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startRoom = Instantiate(startRoomPrefab, Vector3.zero, quaternion.identity);
        currentBlock = createBlock();
        currentPath = GenerateWay();
        currentGOMaze = Generate();
    }

    // Update is called once per frame
    void Update()
    {
        if (Character.position.x >= PlayerRast)
        {
            PlayerPrRast = PlayerRast;
            PlayerRast = Rast;
            LevelCount++;
            previousPrPath = previousPath;
            previousPath = currentPath;
            currentPath = GenerateWay();
            previousPrGOMaze = previousGOMaze;
            previousGOMaze = currentGOMaze;
            currentGOMaze = Generate();
            previousPrBlock = previousBlock;
            previousBlock = currentBlock;
            currentBlock = createBlock();
            int tmp = LevelCount / 4;
            CountWight = 5 + 2 * tmp;
            CountHeight = 5 + 2 * tmp;

            if (previousPrBlock != null)
            {
                Destroy(previousPrBlock);
            }
            if (startRoom != null)
            {
                Destroy(startRoom);
            }

            for (int x = 0; x < previousPrGOMaze.GetLength(0); ++x)
            {
                for (int z = 0; z < previousPrGOMaze.GetLength(1); ++z)
                {
                    if (previousPrGOMaze[x, z] != null)
                    {
                        Destroy(previousPrGOMaze[x, z]);
                    }
                    if (previousPrPath[x, z] != null)
                    {
                        Destroy(previousPrPath[x, z]);
                    }
                }
            }
        }
        if (isEnd)
        {
            int x = (int)((Character.position.x - PlayerPrRast) / Height);
            int y = (int)((Character.position.z + (Height * previousPrGOMaze.GetLength(0) / 2)) / Height);
            Debug.Log(x + ", " + y);
            // ShowWay();
        }
    }
    public GameObject[,] Generate()
    {
        MazeGenerator generator = new MazeGenerator();
        MazeGeneratorCell[,] maze = generator.GenerateMaze(CountWight, CountHeight);
        GameObject[,] GOMaze = new GameObject[maze.GetLength(0), maze.GetLength(1)];
        currentPath[0, CountHeight / 2].GetComponent<Way>().Bottom.SetActive(true);
        for (int x = 0; x < maze.GetLength(0); ++x)
        {
            for (int z = 0; z < maze.GetLength(1); ++z)
            {
                GOMaze[x, z] = Instantiate(cellPrefab, new Vector3(PlayerRast + 1.7f + x * Wight, 0, -(Height * CountHeight / 2 - 1.7f) + z * Height), quaternion.identity);
                
                Cell c = GOMaze[x, z].GetComponent<Cell>();
                c.WallLeft.SetActive(maze[x, z].WallLeft);
                c.WallBottom.SetActive(maze[x, z].WallBottom);
                if (maze[x, z].WallLeft) currentPath[x, z].GetComponent<Way>().Left.SetActive(false);
                else                     currentPath[x, z].GetComponent<Way>().Left.SetActive(true);

                if (maze[x, z].WallBottom) currentPath[x, z].GetComponent<Way>().Up.SetActive(false);
                else                       currentPath[x, z].GetComponent<Way>().Up.SetActive(true);

                if (x > 0)
                    if (maze[x - 1, z].WallBottom) currentPath[x, z].GetComponent<Way>().Bottom.SetActive(false);
                    else                         currentPath[x, z].GetComponent<Way>().Bottom.SetActive(true);
                
                if (z > 0)
                    if (maze[x, z - 1].WallLeft) currentPath[x, z].GetComponent<Way>().Right.SetActive(false);
                    else                           currentPath[x, z].GetComponent<Way>().Right.SetActive(true);
            }
        }
        return GOMaze;
    }
    public GameObject[,] GenerateWay()
    {
        GameObject[,] WayObj = new GameObject[CountHeight, CountWight];
        for (int x = 0; x < CountHeight; ++x)
        {
            for (int z = 0; z < CountWight; ++z)
            {
                WayObj[x, z] = Instantiate(WayPrefab, new Vector3(PlayerRast + 1.7f + x * Wight, 0, -(Height * CountHeight / 2 - 1.7f) + z * Height), quaternion.identity);
                Way c = WayObj[x, z].GetComponent<Way>();
                c.Left.SetActive(false);
                c.Right.SetActive(false);
                c.Up.SetActive(false);
                c.Bottom.SetActive(false);
            }
        }
        return WayObj;
    }
    private GameObject createBlock() 
    {
        Rast += 1f * Height * CountHeight / 2;
        GameObject curObj = Instantiate(blockPrefab, new Vector3(Rast, 0, 0), quaternion.identity);
        Block curBlock = curObj.GetComponent<Block>();

        curBlock.Floor.transform.localScale = new Vector3(Height * CountHeight / 10, 1, Height * CountHeight / 10);

        curBlock.WallLeft.transform.localScale = new Vector3(Height * CountHeight, 4.4f, 0.35f);
        curBlock.WallLeft.transform.position = new Vector3(Rast, 2.2f, Height * CountHeight / 2  + 0.175f);

        curBlock.WallRight.transform.localScale = new Vector3(Height * CountHeight, 4.4f, 0.35f);
        curBlock.WallRight.transform.position = new Vector3(Rast, 2.2f, (-1) * Height * CountHeight / 2  - 0.175f);


        curBlock.WallWithDoorLeft.transform.localScale = new Vector3(0.35f, 4.4f, Height * CountHeight / 2 - 1.6f);
        curBlock.WallWithDoorLeft.transform.position = new Vector3(Rast - 1f * Height * CountHeight / 2 - 0.175f, 2.2f, (Height * CountHeight / 2 - 1.6f) / 2 + 1.6f);
        
        curBlock.WallWithDoorRight.transform.localScale = new Vector3(0.35f, 4.4f, Height * CountHeight / 2 - 1.7f);
        curBlock.WallWithDoorRight.transform.position = new Vector3(Rast - 1f * Height * CountHeight / 2 - 0.175f, 2.2f, -(Height * CountHeight / 2 - 1.7f) / 2 - 1.7f);
        
        curBlock.WallWithDoorUp.transform.position = new Vector3(Rast - 1f * Height * CountHeight / 2 - 0.175f, 3.9f, -0.05f);
        
        Rast += 1f * Height * CountHeight / 2;

        return curObj;
    }

    // private void ShowWay()
    // {
        
    // }
}
