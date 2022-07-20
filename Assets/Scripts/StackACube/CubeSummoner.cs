using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeSummoner : MonoBehaviour
{
    [SerializeField] private InputActionReference _cubeActionReference;
    [SerializeField] private GameObject _cube;
    [SerializeField] private GameObject _leftHand;
    [SerializeField] private GameObject _rightHand;
    [SerializeField] private List<GameObject> _cubes;
    [SerializeField] private List<GameObject> _levelGoals;
    
    private Vector3 _leftHandPosition;
    private Vector3 _rightHandPosition;
    private Vector3 _spawnPosition;

    private float _maxDistance = 0.17f;
    private float _minDistance = 0.02f;

    private float _gameTimer = 0f;
    private float _timeLimit = 12f;

    private bool _started = false;

    // Start is called before the first frame update
    void Start()
    {
        _cubeActionReference.action.performed += SpawnCube;
    }

    private void SpawnCube(InputAction.CallbackContext obj)
    {
        if (_started == false)
        {
            _started = true;
        }
        
        if (_cubes.Count > 0)
        {
            GameObject fakeCubeToRemove = _cubes[0];
            _cubes.RemoveAt(0);
            fakeCubeToRemove.SetActive(false);
            float scaleVal = CalculateScaleVal();
            GameObject newCube = Instantiate(_cube, _spawnPosition, transform.rotation);
            BoxCollider boxCollider = newCube.GetComponent<BoxCollider>();

            boxCollider.transform.localScale = new Vector3(scaleVal, scaleVal, scaleVal);
            newCube.transform.localScale = new Vector3(scaleVal, scaleVal, scaleVal);
            boxCollider.enabled = true;
        }
        else
        {
            ResetGame();
        }
    }

    private float CalculateScaleVal()
    {
        float distance = Vector3.Distance(_leftHandPosition, _rightHandPosition);
        float scaleVal;

        if (distance / 2 < _minDistance)
        {
            scaleVal = _minDistance;
        }
        else if (distance / 2 > _maxDistance)
        {
            scaleVal = _maxDistance;
        }
        else
        {
            scaleVal = distance / 2;
        }

        return scaleVal;
    }

    // Update is called once per frame
    void Update()
    {
        SetSpawnPosition();

        if (_started)
        {
            if (_gameTimer > _timeLimit)
            {
                bool reset = true;
                
                foreach (GameObject levelGoal in _levelGoals)
                {
                    if (levelGoal.GetComponent<LevelGoal>().WonGame())
                    {
                        reset = false;
                    }
                }
                
                if (reset)
                {
                    ResetGame();
                }
            }
            else
            {
                _gameTimer += Time.deltaTime;
            }
        }
    }

    private void SetSpawnPosition()
    {
        _leftHandPosition = _leftHand.transform.position;
        _rightHandPosition = _rightHand.transform.position;
        _spawnPosition = _leftHandPosition + (_rightHandPosition - _leftHandPosition) / 2;
    }

    public void ResetGame()
    {
        _gameTimer = 0f;
        _started = false;
        ResetFakeStackingCubes();
        ResetStackingCubes();
        ResetLevelGoals();
    }

    private void ResetLevelGoals()
    {
        foreach (GameObject levelGoal in _levelGoals)
        {
            LevelGoal levelGoalScript = levelGoal.GetComponent<LevelGoal>();
            levelGoalScript.Reset();
        }
    }

    private void ResetStackingCubes()
    {
        List<StackingCube> stackingCubes = FindObjectsOfType<StackingCube>().ToList();

        foreach (StackingCube stackingCube in stackingCubes)
        {
            Destroy(stackingCube.gameObject);
        }
    }

    private void ResetFakeStackingCubes()
    {
        var fakeStackingCubes = FindObjectsOfType(typeof(FakeStackingCube), true).ToList();

        foreach (FakeStackingCube fakeStackingCube in fakeStackingCubes)
        {
            fakeStackingCube.gameObject.SetActive(true);
            fakeStackingCube.Reset();
            _cubes.Add(fakeStackingCube.gameObject);
        }
    }
}
