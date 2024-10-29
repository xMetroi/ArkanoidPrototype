using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    enum PowerupType
    {
        Resize,
        BallSpeed,
        PaddleSpeed,
        Life,
        BallMultiplier,
    }
    
    [Header("Movement Properties")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private PowerupType powerupType;
    
    [Header("Resize Properties")]
    [SerializeField] private Vector2 resizeMultiplier;
    [SerializeField] private float resizeTime;
    
    [Header("SpeedMultiplier Properties")]
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float speedMultiplierTime;
    
    [Header("Ball Speed Multiplier Properties")]
    [SerializeField] private float ballSpeedMultiplier;
    [SerializeField] private float ballSpeedMultiplierTime;
    
    [Header("Add life Properties")]
    [SerializeField] private float lifeToAdd;
    
    // Update is called once per frame
    void Update()
    {
        //Make the powerup move down
        transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        IPowerup powerup = other.GetComponent<IPowerup>();
        
        switch (powerupType)
        {
            case PowerupType.Resize:
                powerup.Resize(resizeMultiplier.x, resizeMultiplier.y, resizeTime);
                break;
            case PowerupType.PaddleSpeed:
                powerup.SpeedMultiplier(speedMultiplier, speedMultiplierTime);
                break;
            case PowerupType.BallSpeed:
                powerup.BallSpeedMultiplier(ballSpeedMultiplier, ballSpeedMultiplierTime);
                break;
            case PowerupType.Life:
                powerup.AddLifes(lifeToAdd);
                break;
            case PowerupType.BallMultiplier:
                int ballMultiplier = GameManager.Instance.GetBallsList().Count;
                powerup.BallMultiplier(ballMultiplier);
                break;
        }
        
        Destroy(gameObject);
    }
    
    #if UNITY_EDITOR
    
    /// <summary>
    /// WE USE THIS TO ONLY SHOW DETERMINED VARIABLES IN THE INSPECTOR DEPENDING ON THE TYPE OF POWERUP
    /// </summary>
    [CustomEditor(typeof(Powerup))]
    public class PowerupEditor : Editor
    {
        SerializedProperty powerupTagProp;

        private void OnEnable()
        {
            powerupTagProp = serializedObject.FindProperty("powerupType");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // Show general variables
            EditorGUILayout.PropertyField(serializedObject.FindProperty("movementSpeed"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("powerupType"));

            // Show specific variables depending on the type of the powerup
            switch ((PowerupType)powerupTagProp.enumValueIndex)
            {
                case PowerupType.Resize:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("resizeMultiplier"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("resizeTime"));
                    break;

                case PowerupType.PaddleSpeed:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("speedMultiplier"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("speedMultiplierTime"));
                    break;

                case PowerupType.BallSpeed:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("ballSpeedMultiplier"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("ballSpeedMultiplierTime"));
                    break;
                case PowerupType.Life:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("lifeToAdd"));
                    break;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
