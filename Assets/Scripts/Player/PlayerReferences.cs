using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReferences : MonoBehaviour
{
    [Header("Script References")]
    public PlayerMovement playerMovement;
    public PlayerStats playerStats;
    public PlayerMechanics playerMechanics;
    public PlayerUI playerUI;
    public PlayerSounds playerSounds;
    
    #region Component References
    
    public PlayerInputs playerInputs;
    [HideInInspector] public Rigidbody rb;
    
    #endregion
    
    private void Awake()
    {
        playerInputs = new PlayerInputs();
        playerInputs.Movement.Enable();

        rb = GetComponent<Rigidbody>();
    }
}
