﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    [SerializeField]
    private int blockSizeX = 50;
    [SerializeField]
    private int blockSizeY = 50;

    private int offset;

    Dictionary<Vector3, GameObject> cubePos = new Dictionary<Vector3, GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        CreateMaze();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreateMaze()
    {

        Vector3[,] Points = new Vector3[blockSizeX, blockSizeY];


        for (int x = 0; x < blockSizeX; x++)
        {
            for (int z = 0; z < blockSizeY; z++)
            {
                Points[x, z] = new Vector3(1 * x, 1f, 1 * z);
            }
        }

        for (int x = 0; x < blockSizeX; x++)
        {
            for (int y = 0; y < blockSizeY; y++)
            {
                   GameObject cube = new GameObject();
                   cube.name = "Cube" + x + "," + y;
                   Cube c = cube.AddComponent<Cube>();
                   cube.transform.position = Points[x,y];
                   this.transform.parent = this.transform;
                   c.CubeSize = new Vector3(0.5f, 1f, 0.5f);

                    cubePos.Add(Points[x,y], cube);
                    
                   
            }
        }

        for (int x = 0; x < blockSizeX; x++)
        {
            for (int y = 0; y < blockSizeY; y++)
            {
                if (x == 0 || y == 0 || x == blockSizeX - 1 || y == blockSizeY - 1)
                {
                    GameObject delCube;

                    if (cubePos.TryGetValue(Points[x, y], out delCube))
                    {
                        delCube.GetComponent<Cube>().isVisted = true;
                    }
                }
                else if (x == 1 || x == blockSizeX - 2)
                {
                    GameObject delCube;

                    if (cubePos.TryGetValue(Points[x, y], out delCube))
                    {
                        Destroy(delCube);
                    }
                }
            }
        }

        GameObject outCube;

        if (cubePos.TryGetValue(Points[2, 1], out outCube))
        {
            print("got this far");
            MazeAlgorithm(outCube, Points);

        }
    }

    private void MazeAlgorithm(GameObject cube, Vector3[,] points)
    {

        /*Given a current cell as a parameter,
         
          Mark the current cell as visited

          While the current cell has any unvisited neighbour cells
                Choose one of the unvisited neighbours

                Remove the wall between the current cell and the chosen cell

                Invoke the routine recursively for a chosen cell*/

        cube.GetComponent<Cube>().isVisted = true;
        print("got componenet" + cube.GetComponent<Cube>().isVisted);
        Vector3 pos = cube.transform.position;
        print("got the position" + pos);

        GameObject[] cubes = new GameObject[4];

        if (cubePos.TryGetValue(points[(int)pos.x + 1, (int)pos.z], out cubes[0]) &&
            cubePos.TryGetValue(points[(int)pos.x, (int)pos.z + 1], out cubes[1]) &&
            cubePos.TryGetValue(points[(int)pos.x - 1, (int)pos.z], out cubes[2]) &&
            cubePos.TryGetValue(points[(int)pos.x, (int)pos.z - 1], out cubes[3]))
        {
            try
            {

                while (!cubes[1].GetComponent<Cube>().isVisted ||
                      !cubes[2].GetComponent<Cube>().isVisted ||
                      !cubes[3].GetComponent<Cube>().isVisted ||
                      !cubes[4].GetComponent<Cube>().isVisted)
                {
                    int ranNum = UnityEngine.Random.Range(0, 3);

                    cubes[ranNum].GetComponent<Cube>().isVisted = true;

                    Destroy(cubes[ranNum]);

                    MazeAlgorithm(cubes[ranNum], points);
                }
            }
            catch (Exception e)
            { 
                
            }

        }

    }
}
