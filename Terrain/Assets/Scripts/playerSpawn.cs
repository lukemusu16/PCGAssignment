using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSpawn : MonoBehaviour
{

    private Terrain terrain;
    private TerrainData td;

    [SerializeField]
    private int terrainLayerIndex = 8;

    [SerializeField]

    private GameObject playerPrefab;

    Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        terrain = GetComponent<Terrain>();
        td = Terrain.activeTerrain.terrainData;

        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        Vector3 directionUP = transform.TransformDirection(Vector3.up) * td.size.y / 2;
        Vector3 directionDOWN = transform.TransformDirection(Vector3.down) * td.size.y / 2;
        

        float randomX = Random.Range(0, td.size.x);
        float randomZ = Random.Range(0, td.size.z);

        playerPos = new Vector3(randomX, td.size.y, randomZ);

        print(playerPos);

        RaycastHit hit;

        int layerMask = 1 << terrainLayerIndex;

        GameObject player = Instantiate(playerPrefab, playerPos, Quaternion.identity);
        player.name = "Boobies";
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 directionUP = transform.TransformDirection(Vector3.up) * td.size.y/2;
        Gizmos.DrawRay(playerPos, directionUP);

        Gizmos.color = Color.green;
        Vector3 directionDOWN = transform.TransformDirection(Vector3.down) * td.size.y / 2;
        Gizmos.DrawRay(playerPos, directionDOWN);
    }
}
