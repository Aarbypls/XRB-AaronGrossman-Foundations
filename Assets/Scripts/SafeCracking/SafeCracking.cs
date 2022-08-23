using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeCracking : MonoBehaviour
{
    [SerializeField] private Bounds _bounds;
    [SerializeField] private Vector3 _boundSize;
    [SerializeField] private bool _handInBounds;
    [SerializeField] private GameObject _hand;
    [SerializeField] private GameObject _dial;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBounds();
        CheckBounds();

        if (_handInBounds)
        {
            _dial.transform.rotation = Quaternion.AngleAxis(_hand.transform.rotation.eulerAngles.z * 3, Vector3.right);
        }
    }
    
    private void UpdateBounds()
    {
        _bounds = new Bounds(transform.position, _boundSize);
    }

    private void CheckBounds()
    {
        Vector3 handPosition = _hand.transform.position;

        if (_bounds.Contains(handPosition))
        {
            _handInBounds = true;
        }
        else
        {
            _handInBounds = false;
        }
    }
}
