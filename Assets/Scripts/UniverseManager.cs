using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseManager : MonoBehaviour
{
    [SerializeField]
    private GameObject galaxyPrefabR;

    [SerializeField]
    private GameObject galaxyPrefabB;

    [SerializeField]
    private GameObject galaxyPrefabG;

    [SerializeField]
    private GameObject galaxyPrefabW;

    [SerializeField]
    private string fileName = "";

    [SerializeField]
    private double spawnRadius;

    [SerializeField]
    private double galaxyRadius;

    [SerializeField]
    private GameObject player;

    private DataReader dataReader;
    private GalaxyData[] data;

    public Sprite s;

    public Universe universe;

    public static int galaxiesToSpawn;

    public static GameObject[] everyGalaxyCanvas;

    private float maxDistance = float.MinValue;

    private void Awake()
    {
        if(OptionsMenu.low)
        {
            galaxiesToSpawn = 600;
        } 
        else if(OptionsMenu.medium)
        {
            galaxiesToSpawn = 800;
        }
        else if(OptionsMenu.high)
        {
            galaxiesToSpawn = 1000;
        }

        dataReader = new DataReader(fileName, galaxiesToSpawn);
        data = dataReader.Read(OnDataRead);
    }

    // Use this for initialization
    void Start()
    {
        universe = new Universe(galaxyRadius, spawnRadius, data, s);
        universe.SpawnGalaxies(galaxyPrefabR, galaxyPrefabB, galaxyPrefabG, galaxyPrefabW, maxDistance);
        everyGalaxyCanvas = GameObject.FindGameObjectsWithTag("galaxyImage");  //returns GameObject[]
    }

    void Update()
    {
        for (int i = 0; i < everyGalaxyCanvas.Length; i++)
        {
            everyGalaxyCanvas[i].transform.LookAt(player.transform);
        }
    }

    

    private void OnDataRead(string header, float value)
    {
        if (header == "distance")
        {
            maxDistance = value > maxDistance ? value : maxDistance;
        }
    }

    public static void updateCoverage(int c)
    {
        galaxiesToSpawn = c;
    }
}