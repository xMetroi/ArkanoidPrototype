using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Player Stats UI")]
    [SerializeField] private TMP_Text playerHpText;
    [SerializeField] private TMP_Text playerPointsText;
    private PlayerReferences references;

    [Header("Pause Menu Properties")] 
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button restartLevelButton;
    [SerializeField] private Button backToLobbyButton;
    [SerializeField] private Button closePauseMenuButton;
    [SerializeField] private GameObject pauseMenuCanvas;
    private bool paused = false;
    
    [Header("Game Over Menu Properties")]
    [SerializeField] private GameObject gameOverMenuCanvas;
    [SerializeField] private Button gameOverRestartLevelButton;
    [SerializeField] private Button gameOverBackToLobbyButton;
    
    [Header("Level Passed Menu Properties")]
    [SerializeField] private GameObject levelPassedMenuCanvas;
    [SerializeField] private Button levelPassedNextLevelButton;
    [SerializeField] private Button levelPassedBackToLobbyButton;
    
    // Start is called before the first frame update
    void Start()
    {
        references = GetComponentInParent<PlayerReferences>();
        
        references.playerStats.OnPlayerDamaged += OnPlayerDamaged;
        references.playerStats.OnPlayerDeath += OnPlayerDeath;
        references.playerStats.OnGamePointAdded += OnGamePointAdded;
        references.playerMechanics.OnAddLife += OnAddLife;
        GameManager.Instance.OnLevelPassed += OnLevelPassed;
        
        Initialize();
    }

    private void OnDestroy()
    {
        if (references != null)
        {
            references.playerStats.OnPlayerDamaged -= OnPlayerDamaged;
            references.playerStats.OnPlayerDeath -= OnPlayerDeath;
            references.playerStats.OnGamePointAdded -= OnGamePointAdded;
            references.playerMechanics.OnAddLife -= OnAddLife;   
            GameManager.Instance.OnLevelPassed -= OnLevelPassed;
        }
    }

    private void Initialize()
    {
        playerHpText.text = $"Lifes: {references.playerStats.GetPlayerHp()}";
        playerPointsText.text = $"Points: {references.playerStats.GetGamePoints()}";
        
        AssignPauseMenuMethods();
        AssignGameOverMenuMethods();
    }

    #region Player Events
    
    /// <summary>
    /// Trigger when player is damaged
    /// </summary>
    /// <param name="damage"></param>
    private void OnPlayerDamaged(float damage)
    {
        playerHpText.text = $"Lifes: {references.playerStats.GetPlayerHp()}";
    }

    /// <summary>
    /// Trigger when player is damaged and have zero lifes
    /// </summary>
    private void OnPlayerDeath()
    {
        ShowGameOverMenu();
    }

    /// <summary>
    /// Trigger when player get a game point
    /// </summary>
    /// <param name="pointsAdded"></param>
    private void OnGamePointAdded(float pointsAdded)
    {
        playerPointsText.text = $"Points: {references.playerStats.GetGamePoints()}";
    }

    /// <summary>
    /// Trigger when player get a life
    /// </summary>
    private void OnAddLife()
    {
        playerHpText.text = $"Lifes: {references.playerStats.GetPlayerHp()}";
    }

    private void OnLevelPassed()
    {
        ShowLevelPassedMenu();
        AssignLevelPassedButton();
    }
    
    #endregion
    
    #region Pause Menu
    
    private void AssignPauseMenuMethods()
    {
        pauseButton.onClick.AddListener(() => GameManager.Instance.PauseResumeGame(paused));
        restartLevelButton.onClick.AddListener(() => GameManager.Instance.LoadScene(gameObject.scene.name));
        backToLobbyButton.onClick.AddListener(() => GameManager.Instance.LoadScene("MainMenu"));
        closePauseMenuButton.onClick.AddListener(() => GameManager.Instance.PauseResumeGame(paused));
    }

    public void OpenClosePauseMenu()
    {
        if (!paused)
        {
            paused = true;
            pauseButton.interactable = false;
            pauseMenuCanvas.SetActive(true);
        }
        else
        {
            paused = false;
            pauseButton.interactable = true;
            pauseMenuCanvas.SetActive(false);
        }
    }
    
    #endregion
    
    #region Game Over Menu

    private void AssignGameOverMenuMethods()
    {
        gameOverRestartLevelButton.onClick.AddListener(() => GameManager.Instance.LoadScene(gameObject.scene.name));
        gameOverBackToLobbyButton.onClick.AddListener(() => GameManager.Instance.LoadScene("MainMenu"));
    }
    
    private void ShowGameOverMenu()
    {
        GameManager.Instance.PauseResumeGame(!paused);
        gameOverMenuCanvas.SetActive(true);
    }
    
    #endregion
    
    #region Level Passed Menu

    private void AssignLevelPassedButton()
    {
        string nextLevelSceneName = GameManager.Instance.GetNextLevel(gameObject.scene.name);

        levelPassedBackToLobbyButton.onClick.AddListener(() => GameManager.Instance.LoadScene("MainMenu"));
        
        //If the next level is loaded in the build settings
        if (GameManager.Instance.IsSceneInBuildSettings(nextLevelSceneName))
        {
            levelPassedNextLevelButton.interactable = true;
            levelPassedNextLevelButton.onClick.AddListener(() => GameManager.Instance.LoadScene(nextLevelSceneName));
        }
    }

    private void ShowLevelPassedMenu()
    {
        GameManager.Instance.PauseResumeGame(!paused);
        levelPassedMenuCanvas.SetActive(true);
    }
    
    #endregion
}
