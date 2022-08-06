using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

public class Axe : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable _grabInteractable;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GameObject _axeHead;
    
    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(_grabInteractable, "You have not assigned a grab interactable to axe " + name);
        
        _grabInteractable.selectExited.AddListener(OnAxeThrow);
    }

    public void OnAxeThrow(SelectExitEventArgs arg0)
    {
        _rigidbody.isKinematic = false;
    }
}
