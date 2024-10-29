using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionManager : MonoBehaviour
{
    [Header("LevelSelection Properties")]
    [SerializeField] private Button[] levelSelectionButtons;

    private void Start()
    {
        AssignLevelSelectionButtons();
    }

    /// <summary>
    /// Assign methods to buttons in the level selection canvas
    /// For the button to work, it must have the name of the scene to be loaded.
    /// </summary>
    public void AssignLevelSelectionButtons()
    {
        foreach (Button button in levelSelectionButtons)
        {
            button.onClick.AddListener(() => GameManager.Instance.LoadScene(button.name));
        }
    }
}
