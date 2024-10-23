using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Properties")] 
    [SerializeField] private float movementSpeed;
    private Vector2 movementInput;
    
    private PlayerReferences playerReferences;
    
    // Start is called before the first frame update
    void Start()
    {
        playerReferences = GetComponent<PlayerReferences>();
    }

    // Update is called once per frame
    void Update()
    {
        InputHandler();
    }

    private void FixedUpdate()
    {
        Movement();
    }
    
    #region Getter / Setter
    
    public void SetMovementSpeed(float speed) { this.movementSpeed = speed; }
    public float GetMovementSpeed() { return this.movementSpeed; }
    
    #endregion

    #region Inputs

    private void InputHandler()
    {
        movementInput = playerReferences.playerInputs.Movement.Movement.ReadValue<Vector2>().normalized;
    }
    
    #endregion
    
    #region Movement

    private void Movement()
    {
        playerReferences.rb.velocity = new Vector2(movementInput.x * movementSpeed, playerReferences.rb.velocity.y);
    }
    
    #endregion
}
