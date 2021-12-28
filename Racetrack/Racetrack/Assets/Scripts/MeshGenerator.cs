using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator
{

    //Lists to generate
    //Point positions
    private List<Vector3> pointsList = new List<Vector3>();
    //Normals
    private List<Vector3> normalsList = new List<Vector3>();
    //Uv mapping
    private List<Vector2> uvList = new List<Vector2>();
    //Triangles
    private List<int> trianglesList = new List<int>();
    //subMesh
    private List<int>[] submeshList = new List<int>[] { };


    //Constructor
    public MeshGenerator(int submeshCount) {
        submeshList = new List<int>[submeshCount];

        for (int i=0; i<submeshCount; i++) {
            submeshList[i] = new List<int>();
        }
    }

    public void CreateTriangle(Vector3 point1, Vector3 point2, Vector3 point3, int submeshIndex) {
        //Calculating the normal, i.e. the faces
        Vector3 normal = Vector3.Cross(point2 - point1, point3 - point1).normalized;

        //Get point index for next 3 locations
        int point1Index = pointsList.Count; 
        int point2Index = pointsList.Count + 1;
        int point3Index = pointsList.Count + 2;

        //Add points
        pointsList.Add(point1);
        pointsList.Add(point2);
        pointsList.Add(point3);

        //Add triangles
        trianglesList.Add(point1Index);
        trianglesList.Add(point2Index);
        trianglesList.Add(point3Index);

        //Add points to Submesh
        submeshList[submeshIndex].Add(point1Index);
        submeshList[submeshIndex].Add(point2Index);
        submeshList[submeshIndex].Add(point3Index);

        //Add normals
        normalsList.Add(normal);
        normalsList.Add(normal);
        normalsList.Add(normal);
        
        //add uv coordinales - values between 1 and 0
        uvList.Add(new Vector2(0, 0));
        uvList.Add(new Vector2(0, 1));
        uvList.Add(new Vector2(1, 1));
    }

    public Mesh CreateMesh() {
        Mesh mesh = new Mesh();

        mesh.vertices   = pointsList.ToArray();
        mesh.triangles  = trianglesList.ToArray();
        mesh.normals    = normalsList.ToArray();
        mesh.uv         = uvList.ToArray();

        mesh.subMeshCount = submeshList.Length;

        for (int i = 0; i < submeshList.Length; i++) {
            if (submeshList[i].Count < 3)
            {
                mesh.SetTriangles(new int[3]{0,0,0}, i);
            } else {
                mesh.SetTriangles(submeshList[i].ToArray(), i);
            }
        }

        return mesh;
    }
}
