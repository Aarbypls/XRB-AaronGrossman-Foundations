using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : MonoBehaviour
{
    [SerializeField] private GameObject _fire;
    
    public void SetOnFire()
    {
        _fire.SetActive(true);
    }

    public void Extinguish()
    {
        _fire.SetActive(false);
    }
}
