using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Endpoint : MonoBehaviour
{
    [SerializeField]
    GameObject gm;
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer mr = this.GetComponent<MeshRenderer>();
        mr.materials = MaterialsList().ToArray();        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player(Clone)")
        {
            gm.GetComponent<GameManager>().enabled = false;
            SceneManager.LoadScene("Main");
            gm.GetComponent<GameManager>().enabled = true;
        }
    }

    private List<Material> MaterialsList()
    {
        List<Material> ml = new List<Material>();

        Material yellowMaterial = new Material(Shader.Find("Specular"));
        yellowMaterial.color = Color.yellow;

        ml.Add(yellowMaterial);
        return ml;
    }
}
