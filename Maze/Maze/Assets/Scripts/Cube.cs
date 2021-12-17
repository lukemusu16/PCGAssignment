using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class Cube : MonoBehaviour
{
    [SerializeField]
    private Vector3 cubeSize = Vector3.one;

    [SerializeField]
    private int meshIndex = 0;

    [SerializeField]
    private int meshSize = 6;

    public bool isVisted = false;

    // Start is called before the first frame update
    void Start()
    {
        RenderCube();
    }

    public Vector3 CubeSize
    {
        get { return cubeSize; }
        set { cubeSize = value; }
    }

    private void RenderCube()
    {
        MeshFilter mf = this.GetComponent<MeshFilter>();
        MeshRenderer mr = this.GetComponent<MeshRenderer>();

        mf.mesh = CreateCube();
        mr.materials = MaterialsList().ToArray();
    }

    private Mesh CreateCube()
    {
        MeshGenerator mb = new MeshGenerator(meshSize);

        //top points
        Vector3 t1 = new Vector3(cubeSize.x, cubeSize.y, -cubeSize.z);
        Vector3 t2 = new Vector3(-cubeSize.x, cubeSize.y, -cubeSize.z);
        Vector3 t3 = new Vector3(-cubeSize.x, cubeSize.y, cubeSize.z);
        Vector3 t4 = new Vector3(cubeSize.x, cubeSize.y, cubeSize.z);

        //Bottom points
        Vector3 b1 = new Vector3(cubeSize.x, -cubeSize.y, -cubeSize.z);
        Vector3 b2 = new Vector3(-cubeSize.x, -cubeSize.y, -cubeSize.z);
        Vector3 b3 = new Vector3(-cubeSize.x, -cubeSize.y, cubeSize.z);
        Vector3 b4 = new Vector3(cubeSize.x, -cubeSize.y, cubeSize.z);

        //top face
        mb.CreateTriangle(t1, t2, t3, 0);
        mb.CreateTriangle(t1, t3, t4, 0);

        //bottom face
        mb.CreateTriangle(b3, b2, b1, 1);
        mb.CreateTriangle(b4, b3, b1, 1);

        //front face
        mb.CreateTriangle(b1, b2, t2, 2);
        mb.CreateTriangle(b1, t2, t1, 2);

        //back face
        mb.CreateTriangle(t4, b3, b4, 3);
        mb.CreateTriangle(t3, b3, t4, 3);

        //left face
        mb.CreateTriangle(t3, t2, b2, 4);
        mb.CreateTriangle(t3, b2, b3, 4);

        //right face
        mb.CreateTriangle(t1, t4, b4, 5);
        mb.CreateTriangle(t1, b4, b1, 5);

        return mb.CreateMesh();
    }

    private List<Material> MaterialsList()
    {
        List<Material> ml = new List<Material>();

        Material redMaterial = new Material(Shader.Find("Specular"));
        redMaterial.color = Color.red;

        Material greenMaterial = new Material(Shader.Find("Specular"));
        greenMaterial.color = Color.green;

        Material blueMaterial = new Material(Shader.Find("Specular"));
        blueMaterial.color = Color.blue;

        Material yellowMaterial = new Material(Shader.Find("Specular"));
        yellowMaterial.color = Color.yellow;

        Material magentaMaterial = new Material(Shader.Find("Specular"));
        magentaMaterial.color = Color.magenta;

        Material cyanMaterial = new Material(Shader.Find("Specular"));
        cyanMaterial.color = Color.cyan;

        ml.Add(redMaterial);
        ml.Add(greenMaterial);
        ml.Add(blueMaterial);
        ml.Add(yellowMaterial);
        ml.Add(magentaMaterial);
        ml.Add(cyanMaterial);

        return ml;
    }
}
