using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DebugSandboxManager : MonoBehaviour
{
    [SerializeField] private GameObject _lightsObject;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleLight()
    {
        if (_lightsObject.activeSelf)
        {
            _lightsObject.SetActive(false);
        }
        else
        {
            _lightsObject.SetActive(true);
        }
    }

    public void ToggleLightColor(string color)
    {
        switch (color)
        {
            case "red":
                _lightsObject.GetComponentInChildren<Light>().color = Color.red;
                break;
            case "yellow":
                _lightsObject.GetComponentInChildren<Light>().color = Color.yellow;
                break;
            case "blue":
                _lightsObject.GetComponentInChildren<Light>().color = Color.blue;
                break;
            default:
                break;
        }
    }
    
    private void DeactivateMenu()
    {
        gameObject.SetActive(false);
    }
    
    public void CloseMenu()
    {
        Invoke(nameof(DeactivateMenu), 0.05f);
    }
}
