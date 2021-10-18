using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOption : MonoBehaviour
{
    // Start is called before the first frame update
    void FogToggle(bool tog)
    {
        RenderSettings.fog = tog;
    }

}
