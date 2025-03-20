using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulb : MonoBehaviour
{
    [SerializeField] private Light luz;
    private bool lightIsOn;
    public bool LightIsOn { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        if (luz.enabled) { lightIsOn = true; }
        else { lightIsOn = false; }
        InteractableLuz.OnClickLight += SwitchLight;
    }

    
    void SwitchLight()
    {
        if (lightIsOn)
        {
            luz.enabled = false;
            lightIsOn = false;
        }
        else
        {
            luz.enabled = true;
            lightIsOn = true;
        }
    }
    private void OnDisable()
    {
        InteractableLuz.OnClickLight -= SwitchLight;
    }
}
