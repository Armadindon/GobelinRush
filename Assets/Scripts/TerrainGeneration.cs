using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{

    [Header("Terrain Properties")]
    [SerializeField]
    private int xSize;
    [SerializeField]
    private int zSize;
    [SerializeField]
    private int pathWidth;

    [Header("Block's Prefab")]
    [SerializeField]
    private Transform m_pathParent;
    [SerializeField]
    private GameObject m_pathPrefab;
    [SerializeField]
    private Transform m_surroundingParent;
    [SerializeField]
    private GameObject m_surroundingPrefab;

    [Header("Castle and Monster House")]
    [SerializeField]
    private Transform m_buildingParents;
    [SerializeField]
    private GameObject m_housePrefab;
    [SerializeField]
    private GameObject m_castlePrefab;


    //Matrix representing the field
    private TerrainType[,] matrix;

    private void Awake()
    {
        InitMatrix();
        GenerateTerrain();
    }

    private void GenerateTerrain()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                // On ajoute les blocs en fonction de ce qu'il y a dans la matrice
                switch (matrix[x, z])
                {
                    case TerrainType.PATH:
                        Instantiate(m_pathPrefab, new Vector3(x, 0, z), Quaternion.identity, m_pathParent);
                        break;
                    case TerrainType.SURROUNDING:
                        Instantiate(m_surroundingPrefab, new Vector3(x, .1f, z), Quaternion.identity, m_surroundingParent);
                        break;
                    case TerrainType.CASTLE:
                        Instantiate(m_pathPrefab, new Vector3(x, 0, z), Quaternion.identity, m_pathParent);
                        //On le rotate qu'il soit face à l'arrivée
                        Instantiate(m_castlePrefab, new Vector3(x, 1, z), new Quaternion(0, 180, 0, 0), m_buildingParents);
                        break;
                    case TerrainType.HOUSE:
                        Instantiate(m_pathPrefab, new Vector3(x, 0, z), Quaternion.identity , m_pathParent);
                        Instantiate(m_housePrefab, new Vector3(x, 1, z), Quaternion.identity, m_buildingParents);
                        break;
                }
            }
        }
    }


    private void InitMatrix()
    {
        matrix = new TerrainType[xSize, zSize];
        //On initialise la matrice, on genère un chemin simple au centre
        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                if (x > (xSize / 2) - ((float)pathWidth / 2) && x <= (xSize / 2) + (pathWidth / 2))
                {
                    matrix[x, z] = TerrainType.PATH;
                }
                else
                {
                    matrix[x, z] = TerrainType.SURROUNDING;
                }
            }
        }

        matrix[xSize / 2, 0] = TerrainType.HOUSE;
        matrix[xSize / 2, zSize-1] = TerrainType.CASTLE;
    }

}

public enum TerrainType
{
    PATH,
    SURROUNDING,
    HOUSE,
    CASTLE
}
