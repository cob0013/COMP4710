using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    public Material wallMaterial;
    // Start is called before the first frame update
    void Start()
    {
        wallMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ColorChange(Color color)
    {
        Debug.Log("changing wall color");
        wallMaterial.color = color;
    }
}