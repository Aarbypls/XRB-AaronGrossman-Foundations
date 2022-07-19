using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject _start;
    [SerializeField] private GameObject _end;
    [SerializeField] private int _points = 5;
    [SerializeField] private int _speed = 0;
    
    private Vector3 _startLocation;
    private Vector3 _endLocation;

    private void Awake()
    {
        _startLocation = _start.transform.position;
        _endLocation = _end.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Lerp(_startLocation, _endLocation, _speed));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == _endLocation)
        {
            StartCoroutine(Lerp(_endLocation, _startLocation, _speed));
        }
        else if (transform.position == _startLocation)
        {
            StartCoroutine(Lerp(_startLocation, _endLocation, _speed));
        }
    }

    public int Hit()
    {
        return _points;
    }

    IEnumerator Lerp(Vector3 startLocation, Vector3 targetLocation, float speed)
    {
        float time = 0f;
        while (transform.position != targetLocation)
        {
            transform.position = Vector3.Lerp(startLocation, targetLocation,
                    (time / Vector3.Distance(startLocation, targetLocation)) * speed);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
