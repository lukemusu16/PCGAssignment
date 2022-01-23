using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TerrainTextureData
{

    public Texture2D terrainTexture;
    public Vector2 tileSize;
    public float minHeight;
    public float maxHeight;
}

[System.Serializable]
public class TreeData
{

    public GameObject treeMesh;
    public float minHeight;
    public float maxHeight;

}

public class GenerateRandomHeights : MonoBehaviour
{

    private Terrain terrain;
    private TerrainData td;

    [SerializeField]
    [Range(0,1)]
    private float minRandomHeightRange = 0f;

    [SerializeField]
    [Range(0, 1)]
    private float maxRandomHeightRange = 0.1f;

    [SerializeField]
    private bool loadHeightMap = true;

    [SerializeField]
    private bool flattenTerrainInEditMode = false;

    [SerializeField]
    private bool loadHeightMapInEditMode = false;

    [SerializeField]
    private bool flattenTerrainOnExit = true;

    [Header("PerlinNosie")]
    [SerializeField]
    private bool perlinNoise = false;

    [SerializeField]
    private float perlinNoiseWidthScale = 0.01f;

    [SerializeField]
    private float perlinNoiseHeightScale = 0.01f;

    [Header("Texture")]
    [SerializeField]
    private List<TerrainTextureData> terrainTextureData;

    [SerializeField]
    private bool addTerrainTexture = false;

    [SerializeField]
    private float terrainTextureBlendOffset = 0.01f;

    [Header("Tree Data")]
    [SerializeField]
    private List<TreeData> treeData;

    [SerializeField]
    private int maxTrees = 2000;

    [SerializeField]
    private int treeSpacing = 10;

    [SerializeField]
    private bool addTrees = false;

    [SerializeField]
    private int terrainLayerIndex;

    [Header("Water")]
    [SerializeField]
    private GameObject water;

    [SerializeField]
    private float waterHeight = 0.3f;


    // Start is called before the first frame update
    void Start()
    {

        terrain = GetComponent<Terrain>();
        td = Terrain.activeTerrain.terrainData;

        if (loadHeightMap)
        {
            GenerateHeights();
            AddTerrainTexture();
            AddTrees();
            AddWater();      
        }
    }

    void GenerateHeights()
    {
        float[,] heightMap = new float[td.heightmapResolution, td.heightmapResolution];

        for (int width = 0; width < td.heightmapResolution; width++)
        {
            for (int height = 0; height < td.heightmapResolution; height++)
            {
                //Either perlin noise or random values
                
                if (perlinNoise)
                {
                    heightMap[width, height] = Mathf.PerlinNoise(width * perlinNoiseWidthScale, height * perlinNoiseHeightScale);
                }
                else
                {
                    heightMap[width, height] = Random.Range(minRandomHeightRange, maxRandomHeightRange);
                }
                
                //randomise and add smoothness


                //heightMap[width, height] = Random.Range(minRandomHeightRange, maxRandomHeightRange);
                //heightMap[width, height] += Mathf.PerlinNoise(width * perlinNoiseWidthScale, height * perlinNoiseHeightScale);

                //heightMap[width, height] = Mathf.PerlinNoise(Mathf.Sin(Random.Range(minRandomHeightRange, maxRandomHeightRange) * perlinNoiseWidthScale), Mathf.Sin(Random.Range(minRandomHeightRange, maxRandomHeightRange) * perlinNoiseHeightScale));

            }
        }

        td.SetHeights(0, 0, heightMap);
    }

    void FlattenTerrain()
    {
        float[,] heightMap = new float[td.heightmapResolution, td.heightmapResolution];

        for (int width = 0; width < td.heightmapResolution; width++)
        {
            for (int height = 0; height < td.heightmapResolution; height++)
            {

                heightMap[width, height] = 0;

            }
        }

        td.SetHeights(0, 0, heightMap);
    }

    private void AddTerrainTexture()
    {
        TerrainLayer[] terrainLayers = new TerrainLayer[terrainTextureData.Count];

        for (int i = 0; i < terrainTextureData.Count; i++)
        {
            if (addTerrainTexture)
            {
                terrainLayers[i] = new TerrainLayer();
                terrainLayers[i].diffuseTexture = terrainTextureData[i].terrainTexture;
                terrainLayers[i].tileSize = terrainTextureData[i].tileSize;
            }
            else
            {
                terrainLayers[i] = new TerrainLayer();
                terrainLayers[i].diffuseTexture = null;
            }
        }

        td.terrainLayers = terrainLayers;

        float[,] heightMap = td.GetHeights(0,0,td.heightmapResolution,td.heightmapResolution);

        float[,,] alphaMapList = new float[td.alphamapWidth, td.alphamapHeight, td.alphamapLayers];

        for (int width = 0; width < td.alphamapWidth; width++)
        {
            for (int height = 0; height < td.alphamapHeight; height++)
            {

                float[] alphamap = new float[td.alphamapLayers];

                for (int i = 0; i < terrainTextureData.Count; i++)
                {
                    float heightBegin = terrainTextureData[i].minHeight - terrainTextureBlendOffset;
                    float heightEnd = terrainTextureData[i].maxHeight + terrainTextureBlendOffset;

                    if (heightMap[width, height] >= heightBegin && heightMap[width,height] <= heightEnd)
                    {
                        alphamap[i] = 1;
                    }
                }

                Blend(alphamap);
                

                for (int j = 0; j < terrainTextureData.Count; j++)
                {
                    alphaMapList[width, height, j] = alphamap[j];
                }

            }
        }

        td.SetAlphamaps(0, 0, alphaMapList);
    }

    private void Blend(float[] alphamap)
    {
        float total = 0;

        for (int i = 0; i < alphamap.Length; i++)
        {
            total += alphamap[i];
        }

        for (int i = 0; i < alphamap.Length; i++)
        {
            alphamap[i] = alphamap[i] / total;
        }
    }

    private void AddTrees()
    {
        TreePrototype[] trees = new TreePrototype[treeData.Count];

        for (int i = 0; i < treeData.Count; i++)
        {
            trees[i] = new TreePrototype();
            trees[i].prefab = treeData[i].treeMesh;
        }

        td.treePrototypes = trees;

        List<TreeInstance> treeInstanceList = new List<TreeInstance>();

        if (addTrees)
        {
            for (int z = 0; z < td.size.z; z += treeSpacing)
            {
                for (int x = 0; x < td.size.x; x += treeSpacing)
                {
                    for (int treeIndex = 0; treeIndex < trees.Length; treeIndex++)
                    {
                        if (treeInstanceList.Count < maxTrees)
                        {
                            float currentHeight = td.GetHeight(x, z) / td.size.y;

                            if (currentHeight >= treeData[treeIndex].minHeight && currentHeight <= treeData[treeIndex].maxHeight)
                            {
                                float randomX = (x + Random.Range(-5, 5)) / td.size.x;
                                float randomZ = (z + Random.Range(-5, 5)) / td.size.z;

                                Vector3 treePos = new Vector3(randomX * td.size.x, 
                                                              currentHeight * td.size.y, 
                                                              randomZ *td.size.z) + this.transform.position;

                                RaycastHit hit;

                                int layerMask = 1 << terrainLayerIndex;

                                if (Physics.Raycast(treePos, Vector3.down, out hit, 100f, layerMask) ||
                                    Physics.Raycast(treePos, Vector3.up, out hit, 100f, layerMask))
                                {
                                    float treeDistance = (hit.point.y - this.transform.position.y) / td.size.y;

                                    TreeInstance treeInstance = new TreeInstance();
                                    treeInstance.position = new Vector3(randomX, treeDistance, randomZ);
                                    treeInstance.rotation = Random.Range(0, 360);
                                    treeInstance.prototypeIndex = treeIndex;
                                    treeInstance.color = Color.white;
                                    treeInstance.lightmapColor = Color.white;
                                    treeInstance.heightScale = 0.95f;
                                    treeInstance.widthScale = 0.95f;

                                    treeInstanceList.Add(treeInstance);
                                }

                                
                            }
                        }
                    }
                }
            }
        }

        td.treeInstances = treeInstanceList.ToArray();
    }

    private void AddWater()
    {
        GameObject waterGO = Instantiate(water, this.transform.position, this.transform.rotation);
        waterGO.name = "Water";
        waterGO.transform.position = this.transform.position + new Vector3(td.size.x / 2, waterHeight * td.size.y, td.size.z / 2);
        waterGO.transform.localScale = new Vector3(td.size.x, 1, td.size.z);
    }

    private void OnValidate()
    {
        if (flattenTerrainInEditMode)
        {
            FlattenTerrain();
        }
        else if (loadHeightMapInEditMode)
        {
            GenerateHeights();
        }
    }

    private void OnDestroy()
    {
        if (flattenTerrainOnExit)
        {
            FlattenTerrain();
        }
    }

}
