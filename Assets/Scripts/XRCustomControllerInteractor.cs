using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

public class XRCustomControllerInteractor : MonoBehaviour
{
    private XRBaseControllerInteractor _controller;

    private void Start()
    {
        _controller = GetComponent<XRBaseControllerInteractor>();
        Assert.IsNotNull(_controller, "There is no XRBaseControllerInteractor assigned to " + gameObject.name);

        _controller.selectEntered.AddListener(ParentInteractable);
        _controller.selectExited.AddListener(UnparentInteractable);
    }

    private void ParentInteractable(SelectEnterEventArgs arg0)
    {
        arg0.interactable.transform.parent = transform;
    }
    
    private void UnparentInteractable(SelectExitEventArgs arg0)
    {
        arg0.interactable.transform.parent = null;
    }
}
