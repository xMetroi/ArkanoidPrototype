using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header("In Game Properties")]
    [SerializeField] private GameObject playerReference;
    [SerializeField] private List<GameObject> balls;
    [SerializeField] private int blocksRemain;

    [Header("Level Initialization")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject ballPrefab;
    
    public static GameManager Instance;
    
    #region Events
    
    public event Action OnGameOver;
    
    #endregion
    
    #region Getter / Setter

    public List<GameObject> GetBallsList() { return balls;  }
    
    #endregion

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    #region Scene Management

    /// <summary>
    /// Use this method to load a scene
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Contains("Level")) //The loaded scene is a level
        {
            Vector3 playerPosition = GameObject.Find("PlayerSpawnPoint").transform.position;
            Vector3 ballSpawnPosition = GameObject.Find("BallSpawnPoint").transform.position;
            blocksRemain = GameObject.Find("DestroyableBlocksHolder").transform.childCount;
            
            SpawnPlayer(playerPosition);
            SpawnBall(ballSpawnPosition, 2);
            
            Time.timeScale = 1; //Unpause game
        }

        if (scene.name == "MainMenu")
        {
            LevelSelectionManager levelSelection = FindObjectOfType<LevelSelectionManager>();
            
            levelSelection.AssignLevelSelectionButtons();
            
            Time.timeScale = 1; //Unpause game
        }
    }
    
    #endregion
    
    #region Level Management

    private void SpawnPlayer(Vector3 position)
    {
       playerReference = Instantiate(playerPrefab, position, Quaternion.identity);
    }

    public void SpawnBall(Vector3 position, float delay)
    {
        StartCoroutine(SpawnBallWithDelay(position, delay));
    }

    private IEnumerator SpawnBallWithDelay(Vector3 position, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
        
        balls.Add(ball);
    }

    public void SubstractRemainingBlocks(int amount)
    {
        blocksRemain -= amount;

        if (blocksRemain <= 0)
        {
            Debug.Log("Level passed");
            OnGameOver?.Invoke();
        }
    }
    
    #endregion
    
    #region Utilities

    public void PauseResumeGame(bool isPaused)
    {
        if (isPaused)
        {
            PauseTime();
        }
        else
        {
            ResumeTime();
        }
    }
    
    private void PauseTime()
    {
        Time.timeScale = 0;
    }

    private void ResumeTime()
    {
        Time.timeScale = 1;
    }
    
    #endregion
}
