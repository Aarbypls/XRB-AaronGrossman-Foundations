using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameEventManager : MonoBehaviour
{
    public enum GameMode
    {
        FP,
        VR
    }
    
    [Header("Accessibility")] 
    public Handed _handedness;
    
    public GameMode gameMode;
    
    [Header("UI")]
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _failedPanel;
    [SerializeField] private GameObject _successPanel;
    [SerializeField] private float _canvasFadeTime = 2f;
    [SerializeField] private Material _skyboxMaterial;

    [Header("Audio")]
    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioClip _caughtMusic;
    [SerializeField] private AudioClip _successMusic;

    private PlayerInput _playerInput;
    private FirstPersonController _firstPersonController;
    private bool _isFadingIn = false;
    private float _fadeLevel = 0f;
    private bool _isGoalReached = false;

    private Color _initialSkyboxColor;
    private float _initialSkyboxExposure;
    private float _initialSkyboxAtmosphereThickness;

    private void Awake()
    {
        _handedness = (Handed)PlayerPrefs.GetInt("handedness");
    }

    // Start is called before the first frame update
    void Start()
    {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();

        foreach (EnemyController enemy in enemies)
        {
            enemy.onInvestigate.AddListener(EnemyInvestigating);
            enemy.onPlayerFound.AddListener(PlayerFound);
            enemy.onReturnToPatrol.AddListener(EnemyReturnToPatrol);
        }

        GameObject player = GameObject.FindWithTag("Player");
        
        if (player)
        {
            _playerInput = player.GetComponent<PlayerInput>();
            _firstPersonController = player.GetComponent<FirstPersonController>();
        }
        else
        {
            Debug.LogWarning("There is no player (or object with tag \"Player\" in the scene.");
        }

        _canvasGroup.alpha = 0;
        _failedPanel.SetActive(false);
        _successPanel.SetActive(false);
        
        ResetShaderValues();
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _initialSkyboxAtmosphereThickness = _skyboxMaterial.GetFloat("_AtmosphereThickness");
        _initialSkyboxColor = _skyboxMaterial.GetColor("_SkyTint");
        _initialSkyboxExposure = _skyboxMaterial.GetFloat("_Exposure");
    }

    private void EnemyReturnToPatrol()
    {
        
    }

    private void PlayerFound(Transform enemyThatFoundPlayer)
    {
        if (_isGoalReached)
        {
            return;
        }

        _failedPanel.SetActive(true);
        
        if (gameMode == GameMode.FP)
        {
            _isFadingIn = true;

            _firstPersonController.CinemachineCameraTarget.transform.LookAt(enemyThatFoundPlayer);
            
            DeactivateInput();
        }
        else
        {
            StartCoroutine(GameOverFadeOutSaturation(1f));
        }

        PlayBGM(_caughtMusic);
    }

    private IEnumerator GameOverFadeOutSaturation(float startDelay = 0f)
    {
        yield return new WaitForSeconds(startDelay);
        
        Time.timeScale = 0;

        float fade = 0f;

        while (fade < 1)
        {
            fade += Time.unscaledDeltaTime / _canvasFadeTime;
            
            Shader.SetGlobalFloat("_AllWhite", fade);
            _skyboxMaterial.SetFloat("_AtmosphereThickness", Mathf.Lerp(_initialSkyboxAtmosphereThickness, 0.7f, fade));
            _skyboxMaterial.SetColor("_SkyTint", Color.Lerp(_initialSkyboxColor, Color.white, fade));
            _skyboxMaterial.SetFloat("_Exposure", Mathf.Lerp(_initialSkyboxExposure, 8f, fade));
            
            yield return null;
        }

        yield return new WaitForSecondsRealtime(2f);
        RestartScene();
    }

    private void OnDestroy()
    {
        ResetShaderValues();
    }

    private void ResetShaderValues()
    {
        Shader.SetGlobalFloat("_AllWhite", 0);
        _skyboxMaterial.SetFloat("_AtmosphereThickness", _initialSkyboxAtmosphereThickness);
        _skyboxMaterial.SetColor("_SkyTint", _initialSkyboxColor);
        _skyboxMaterial.SetFloat("_Exposure", _initialSkyboxExposure);
    }

    public void GoalReached()
    {
        _isGoalReached = true;
        _isFadingIn = true;
        
        _successPanel.SetActive(true);

        DeactivateInput();
        PlayBGM(_successMusic);
    }

    private void DeactivateInput()
    {
        _playerInput.DeactivateInput();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void PlayBGM(AudioClip newBgm)
    {
        if (_bgmSource.clip == newBgm)
        {
            return;
        }
        
        _bgmSource.clip = newBgm;
        _bgmSource.volume = .25f;
        _bgmSource.Play();
    }

    private void EnemyInvestigating()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartScene()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        ResetShaderValues();
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFadingIn)
        {
            if (_fadeLevel < 1f)
            {
                _fadeLevel += Time.deltaTime/_canvasFadeTime;
            }
        }
        else
        {
            if (_fadeLevel > 0f)
            {
                _fadeLevel -= Time.deltaTime / _canvasFadeTime;
            }
        }
        
        _canvasGroup.alpha = _fadeLevel;
    }

    public void ToggleDominantHand()
    {
        if (_handedness == Handed.Right)
        {
            _handedness = Handed.Left;
        }
        else
        {
            _handedness = Handed.Right;
        }
        
        PlayerPrefs.SetInt("handedness", (int)_handedness);
        PlayerPrefs.Save();
        
        RestartScene();
    }
}
