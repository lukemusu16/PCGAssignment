using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadHeightMap : MonoBehaviour
{

    private Terrain terrain;
    private TerrainData td;

    [SerializeField]
    private Texture2D heightMapImg;

    [SerializeField]
    private Vector3 heightMapScale = new Vector3(1, 1, 1);

    [SerializeField]
    private bool loadHeightMap = true;

    [SerializeField]
    private bool flattenTerrainInEditMode = false;

    [SerializeField]
    private bool loadHeightMapInEditMode = false;

    [SerializeField]
    private bool flattenTerrainOnExit = true;


    // Start is called before the first frame update
    void Start()
    {

        terrain = GetComponent<Terrain>();
        td = Terrain.activeTerrain.terrainData;

        if (loadHeightMap)
        {
            LoadHeightMapImg();
        }
    }

    void LoadHeightMapImg()
    {
        
        float[,] heightMap = new float[td.heightmapResolution, td.heightmapResolution];

        for (int width = 0; width < td.heightmapResolution; width++)
        {
            for (int height = 0; height < td.heightmapResolution; height++)
            {
                heightMap[height, width] = heightMapImg.GetPixel((int)(width * heightMapScale.x), (int)(height * heightMapScale.z)).grayscale * heightMapScale.y;

            }
        }

        td.SetHeights(0,0, heightMap);
    }

    void FlattenTerrain()
    {
        float[,] heightMap = new float[td.heightmapResolution, td.heightmapResolution];

        for (int width = 0; width < td.heightmapResolution; width++)
        {
            for (int height = 0; height < td.heightmapResolution; height++)
            {

                heightMap[height, width] = 0;

            }
        }

        td.SetHeights(0, 0, heightMap);
    }

    private void OnValidate()
    {
        if (flattenTerrainInEditMode)
        {
            FlattenTerrain();
        }
        else if (loadHeightMapInEditMode)
        {
            LoadHeightMapImg();
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
