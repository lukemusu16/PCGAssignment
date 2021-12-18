using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private float playerSpeed = 5.0f;
    private float gravityValue = -9.81f;

    

    private void Start()
    {
        controller = this.gameObject.GetComponent<CharacterController>();
        

        MeshRenderer mr = this.GetComponent<MeshRenderer>();
        mr.materials = MaterialsList().ToArray();
    }

    void Update()
    {


        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0.5f, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.position = move;
        }


        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }



    private List<Material> MaterialsList()
    {
        List<Material> ml = new List<Material>();

        /*
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
        */
        Material redMaterial = new Material(Shader.Find("Specular"));
        redMaterial.color = Color.red;

        ml.Add(redMaterial);
        return ml;
    }
}
