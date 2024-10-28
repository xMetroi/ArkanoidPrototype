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

    public void AssignLevelSelectionButtons()
    {
        foreach (Button button in levelSelectionButtons)
        {
            button.onClick.AddListener(() => GameManager.Instance.LoadScene(button.name));
        }
    }
}
