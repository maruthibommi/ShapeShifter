using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private List<GameObject> activeTiles; //later used to delete passed tiles
    public GameObject[] tilePrefabs; //used to store different types of tiles to spawn 
    public GameObject[] obstacles; //used to store different types of obstacles to spawn
    public int obstacleCount =0; //used to determine number of obstacles per tile
    
    public Camera MainCamera; 
    private Vector2 screenBounds;
    private float objectWidth=0;
    private float objectHeight=0;
    public float tileLength = 30;
    public int numberOfTiles = 8; //number of tiles to be spawned always
    public int totalNumOfTiles = 8; //total number of different tiles 
    private int tileCounter ; //total number of tiles spawned
    public float zSpawn = 0; //used to calculate position of next tile to be spawned
    public bool ctrlobsCnt = true; 
    public Transform playerTransform;
    public float obstacleScale;
    private int previousIndex; //stores the current spawned tile index
    public GameObject bot; //bottom boundary for the player
    float botpos;
    void Start()
    {
        botpos = bot.transform.position.y+3.5f; //calculating y position of bottom boundary
        obstacleCount = 0;
        ctrlobsCnt = true;
       //-----for loop to calculate the maximum width and height of the obstacle-----//
        foreach (GameObject i in obstacles)
        {
            float tobjectWidth = i.transform.GetComponent<MeshRenderer>().bounds.extents.z; 
            float tobjectHeight = i.transform.GetComponent<MeshRenderer>().bounds.extents.y;     
            if(objectWidth <= tobjectWidth)
            {
                objectWidth = tobjectWidth;
            }
            if(objectHeight <= tobjectHeight)
            {
                objectHeight = tobjectHeight;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
       
        activeTiles = new List<GameObject>();
        //-----for loop to spawn n number of tiles at the begining----//
        for (int i = 0; i < numberOfTiles; i++)
        {
            if(i==0)
                SpawnTile();
            else
                SpawnTile(Random.Range(0, totalNumOfTiles));
        }
        //
        
    }
    void Update()//generates and deletes tiles
    {
        if(playerTransform.position.x - 30 >= zSpawn - (numberOfTiles * tileLength))
        {
            
            int index = Random.Range(0, totalNumOfTiles);
            while(index == previousIndex)
                index = Random.Range(0, totalNumOfTiles);

            DeleteTile();
            SpawnTile(index);
        }
            
    }
    float Absol(float a)//to calculate the absolute value 
    {
        if(a<0)
        {
            return a*-1;
        }
        else
        {
            return a;
        }
    }
    public void SpawnTile(int index = 0)//spawns the tile when called 
    {
        GameObject go = Instantiate(tilePrefabs[index],(transform.right * zSpawn)+ (new Vector3(0, botpos, 0)), transform.rotation);
        float x,Px,Py,z,Pz;
        float y;
        y = Random.Range(screenBounds.y * 1 + objectHeight, screenBounds.y * -1 - objectHeight);
        x = Random.Range( screenBounds.x * 1 + objectWidth, screenBounds.x * -1 - objectWidth);
        z = Random.Range(10f,20f);
        Pz = z;
        if(tileCounter >= 6 && ctrlobsCnt)//spawns 1st 6 tiles without obstacles
        {
            obstacleCount = 1;
            ctrlobsCnt = false;
        }
        if(obstacleCount == 1)//spawns 1 obstacle per tile randomly
        {
            GameObject goc1 = Instantiate(obstacles[index],(transform.right * zSpawn)+ (new Vector3(z, y, x)), transform.rotation);
            goc1.transform.localScale = new Vector3(obstacleScale,obstacleScale,obstacleScale);
            goc1.transform.parent = go.transform;
            Px=x;
            Py=y;
        }
        else if(obstacleCount ==2)//spawns 2 obstacles per tile randomly
        {
            GameObject goc1 = Instantiate(obstacles[index],(transform.right * zSpawn)+ (new Vector3(z, y, x)), transform.rotation);
            goc1.transform.localScale = new Vector3(obstacleScale,obstacleScale,obstacleScale);
            goc1.transform.parent = go.transform;
            Px=x;
            Py=y;
            while (Absol(Px-x)<1.5 || Absol(Py-y)<1.5 || Absol(Pz-z)<4) 
            {
                y= Random.Range(-2f,3);
                x = Random.Range(-3f,3f);
                z = Random.Range(10f,20f);
            }
            GameObject goc2 = Instantiate(obstacles[(index+Random.Range(1,3))/8],(transform.right * zSpawn)+ (new Vector3(z, y, x)), transform.rotation);
            goc2.transform.localScale = new Vector3(obstacleScale,obstacleScale,obstacleScale);
            goc2.transform.parent = go.transform;
        }
        else
        {
            
        }
        
        activeTiles.Add(go);//adds each spawned tile to active once
        tileCounter+=1;
        zSpawn += tileLength;
        previousIndex = index;
    }

    private void DeleteTile()//deletes the 1st tile spawned in the aray
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}