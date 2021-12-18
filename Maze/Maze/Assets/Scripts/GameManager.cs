using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject playerPrefab, endpointPrefab;


    // Start is called before the first frame update
    void Start()
    {
        Maze m = new Maze();
        GameObject player = Instantiate(playerPrefab, new Vector3(Random.Range(1, m.blockSizeX-1), 0.5f, 0.5f), Quaternion.identity);
        GameObject endpoint = Instantiate(endpointPrefab, new Vector3(Random.Range(0, m.blockSizeX-1), 0.5f, m.blockSizeY - 2), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
