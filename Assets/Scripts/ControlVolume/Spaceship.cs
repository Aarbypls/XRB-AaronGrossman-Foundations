using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField] private Bounds _bounds;
    [SerializeField] private Vector3 _boundSize;
    [SerializeField] private ControlVolume _controlVolume;

    public bool isBeingControlled;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingControlled)
        {
            Vector3 currentPos = transform.position;
            transform.position = new Vector3(
                currentPos.x - (_controlVolume._scaledValueX * 5f),
                  currentPos.y,
                currentPos.z + (_controlVolume._scaledValueZ * 5f));
        }
    }
    
    private void UpdateBounds()
    {
        _bounds = new Bounds(transform.position, _boundSize);
    }
}
