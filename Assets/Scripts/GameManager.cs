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
    
    public event Action OnLevelPassed;
    
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
        
        SoundManager.Instance.PlayLobbyMusic();
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
            
            SoundManager.Instance.PlayGameMusic();
            
            Time.timeScale = 1; //Unpause game
        }

        if (scene.name == "MainMenu")
        {
            LevelSelectionManager levelSelection = FindObjectOfType<LevelSelectionManager>();
            
            if (levelSelection != null)
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
        
        Transform ballHolder = GameObject.Find("BallsHolder").transform;
        
        GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity, ballHolder);
        
        balls.Add(ball);
    }

    /// <summary>
    /// Method to substract the remaining destroyable blocks count to detect when the game is over
    /// </summary>
    /// <param name="amount"> amount to substract </param>
    public void SubstractRemainingBlocks(int amount)
    {
        blocksRemain -= amount;

        PlayerReferences player = playerReference.GetComponent<PlayerReferences>();
        
        //if is zero blocks remain and player is alive
        if (blocksRemain <= 0 && player.playerStats.GetPlayerHp() > 0)
        {
            Debug.Log("Level passed");
            
            SoundManager.Instance.StopMusic();
            OnLevelPassed?.Invoke();
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

    /// <summary>
    /// Use this to get the next level
    /// </summary>
    /// <param name="actualLevel"> the actual level ( name of the scene ) </param>
    /// <returns></returns>
    public string GetNextLevel(string actualLevel)
    {
        //Get the actual level number
        int actualLevelIndex = int.Parse(actualLevel.Split(" ")[1]);
        
        //Return the next level ( actual level number plus one )
        return $"Level {actualLevelIndex + 1}";
    }
    
    /// <summary>
    /// Check if the given scene is in the build settings
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    public bool IsSceneInBuildSettings(string sceneName)
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        for (int i = 0; i < sceneCount; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string name = System.IO.Path.GetFileNameWithoutExtension(path); // Obtiene solo el nombre de la escena

            if (name == sceneName)
            {
                return true; // La escena está en los Build Settings
            }
        }
        return false; // La escena no está en los Build Settings
    }
    
    #endregion
}
