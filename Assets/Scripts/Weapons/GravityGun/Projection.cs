using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projection : MonoBehaviour
{
    private Scene _simulationScene;
    private PhysicsScene _physicsScene;
    [SerializeField] private Transform _obstaclesParent;

    [SerializeField] private LineRenderer _line;
    [SerializeField] private int _maxPhysicsFrameIterations = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        CreatePhysicsScene();
    }

    private void CreatePhysicsScene()
    {
        _simulationScene =
            SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScene = _simulationScene.GetPhysicsScene();

        foreach (Transform objectTransform in _obstaclesParent)
        {
            var ghostObject = Instantiate(objectTransform.gameObject, objectTransform.position,
                objectTransform.rotation);
            ghostObject.GetComponent<Renderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(ghostObject, _simulationScene);
        }
    }

    public void SimulateTrajectory(GravityBullet bulletPrefab, Vector3 position, Vector3 velocity)
    {
        var ghostObject = Instantiate(bulletPrefab, position, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(ghostObject.gameObject, _simulationScene);
        
        ghostObject.Init(velocity, true);
        
        _line.positionCount = _maxPhysicsFrameIterations;

        for (int i = 0; i < _maxPhysicsFrameIterations; i++)
        {
            _physicsScene.Simulate(Time.fixedDeltaTime);
            _line.SetPosition(i, ghostObject.transform.position);
        }
        
        Destroy(ghostObject.gameObject);
    }
}
