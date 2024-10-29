using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [Header("Movement Properties")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private Vector2 movementDirection;
    
    [Header("Damage Properties")]
    [SerializeField] private float damage;
    
    private Rigidbody rb;
    
    #region Events

    public event Action OnBallImpact;
    
    #endregion
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        rb.velocity = movementDirection * movementSpeed;
    }
    
    #region Getter / Setter
    
    public void SetMovementSpeed(float speed) { this.movementSpeed = speed; }
    public float GetMovementSpeed() { return this.movementSpeed; }
    
    public void SetMovementDirection(Vector2 direction) { this.movementDirection = direction; }
    public Vector2 GetMovementDirection() { return this.movementDirection; }
    
    #endregion

    private void OnCollisionEnter(Collision coll)
    {
        Vector2 direction;
        
        OnBallImpact?.Invoke();

        switch (coll.gameObject.tag)
        {
            case "Player":
                // Get the contact point of the ball on the paddle
                ContactPoint contact = coll.contacts[0];
        
                // Calculate the hit point relative to the paddle's center
                Vector3 paddleCenter = coll.gameObject.transform.position;
                float hitFactor = (contact.point.x - paddleCenter.x) / coll.collider.bounds.size.x;
        
                // Calculate the new direction based on where the ball hit the paddle
                Vector2 newDirection = new Vector2(hitFactor, 1).normalized;

                // Set the new movement direction
                SetMovementDirection(newDirection);
                break;
            
            case "DestroyableBlock":
                
                //Set Direction
                direction = Vector2.Reflect(movementDirection, coll.contacts[0].normal);
        
                SetMovementDirection(direction.normalized);
                
                //Apply Damage
                coll.gameObject.GetComponent<IDamageable>().Damage(damage);
                
                break;
            
            default:
                
                direction = Vector2.Reflect(movementDirection, coll.contacts[0].normal);
        
                SetMovementDirection(direction.normalized);;
                break;
        }
        
    }
}
