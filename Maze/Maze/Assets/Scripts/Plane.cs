﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class Plane : MonoBehaviour
{
    [SerializeField]
    private float quadSize = 1f;

    [SerializeField]
    private int planeSizeX = 10;

    [SerializeField]
    private int planeSizeZ = 10;

    [SerializeField]
    private int meshSize = 1;

    // Start is called before the first frame update
    void Start()
    {
        RenderPlane();
    }

    private void RenderPlane()
    {
        MeshFilter mf = this.GetComponent<MeshFilter>();
        MeshRenderer mr = this.GetComponent<MeshRenderer>();
        MeshCollider mc = this.GetComponent<MeshCollider>();

        mf.mesh = CreatePlane();
        mr.materials = MaterialsList().ToArray();
        mc.sharedMesh = mf.mesh;
    }

    private Mesh CreatePlane()
    {
        MeshGenerator mg = new MeshGenerator(meshSize);

        //points
        Vector3[,] quadPoints = new Vector3[planeSizeX, planeSizeZ];

        for (int x = 0; x < planeSizeX; x++)
        {
            for (int z = 0; z < planeSizeZ; z++)
            {
                quadPoints[x, z] = new Vector3(quadSize * x, 0, quadSize * z);
            }
        }

        //create quads
        for (int x = 0; x < planeSizeX - 1; x++)
        {
            for (int z = 0; z < planeSizeZ - 1; z++)
            {
                Vector3 point1 = quadPoints[x + 1, z];
                Vector3 point2 = quadPoints[x, z];
                Vector3 point3 = quadPoints[x, z + 1];
                Vector3 point4 = quadPoints[x + 1, z + 1];

                mg.CreateTriangle(point1, point2, point3, 0);
                mg.CreateTriangle(point1, point3, point4, 0);
            }
        }

        return mg.CreateMesh();
    }

    private List<Material> MaterialsList()
    {
        List<Material> materials = new List<Material>();

        Material white = new Material(Shader.Find("Specular"));
        white.color = Color.white;

        materials.Add(white);

        return materials;
    }
}
