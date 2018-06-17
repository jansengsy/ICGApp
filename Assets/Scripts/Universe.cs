using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System;

[System.Serializable]
public class Universe
{
    public double spawnRadius;
    public double galaxyRadius;
    private Sprite galaxySprite;
    public GalaxyData[] galaxyData;
    public List<GameObject> spawnedGalaxies;
    private bool secondGroup = false;

    public Universe(double galaxyRadius, double spawnRadius, GalaxyData[] galaxyData, Sprite sprite)
    {
        this.galaxyRadius = galaxyRadius;
        this.spawnRadius = spawnRadius;
        this.galaxyData = galaxyData;
    }

    public GameObject[] SpawnGalaxies(GameObject pr, GameObject pb, GameObject pg, GameObject pw, float maxDistance)
    {
        spawnedGalaxies = new List<GameObject>();

        for (int i = 0; i < galaxyData.Length; i++)
        {
            float spawnDistance = GetNormalisedDistance(galaxyData[i].lookBackTime, maxDistance);
            GameObject selectedPrefab = pw;

            if(i > 599)
            {
                secondGroup = true;
            }

            GameObject g = SpawnGalaxy(i, ref selectedPrefab, spawnDistance);//, galaxyData[i].StellarMass);
            spawnedGalaxies.Add(g);
        }

        return spawnedGalaxies.ToArray();
    }

    public GameObject SpawnGalaxy(int index, ref GameObject galaxyPrefab, float spawnDistance)//, float stellarMass)
    {
        float hkhkh = 1;

        if(secondGroup)
        {
            hkhkh = 100f;
        }

        float px = (galaxyData[index].Position.x * 1500) * (galaxyData[index].lookBackTime * hkhkh);
        float py = (galaxyData[index].Position.y * 1500) * (galaxyData[index].lookBackTime * hkhkh);
        float pz = (galaxyData[index].Position.z * 1500) * (galaxyData[index].lookBackTime * hkhkh);
        Vector3 p = new Vector3(px, py, pz);
        
        GameObject g = GameObject.Instantiate(galaxyPrefab, p, Quaternion.identity);

        float imgNumber = galaxyData[index].imageNum;
        galaxySprite = Resources.Load<Sprite>("GalaxyImages/" + imgNumber);
        g.GetComponent<Image>().sprite = galaxySprite;

        return g;
    }

    private float GetNormalisedDistance(float distance, float maxDistance)
    {
        return ((float)distance / (float)maxDistance) * (float)spawnRadius;
    }
}
