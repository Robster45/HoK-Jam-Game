using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    //private float stamina = 100.0f;
    private float health = 100.0f;

    //private bool canRun = true;
    //private bool hasStamina = true;
    private bool isDead = false;

    public static PlayerData Instance { get; set; }

    //public bool CanRun { get { return canRun; } }
    //public bool HasStamina { get { return hasStamina; } }
    public bool IsDead { get { return isDead; } }

    public float GetHealth { get { return health; } }
    //public float GetStamina { get { return stamina; } }

    private void Awake()
    {
        if (Instance != this && Instance != null) Destroy(this);
        else Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdjustHealth(float value)
    {
        health += value;
        CheckStatus();
    }

    //public void AdjustStamina(float value)
    //{
    //    stamina += value;
    //    CheckStatus();
    //}

    public void CheckStatus()
    {
        //if (stamina > 5.0f) hasStamina = true;
        //else hasStamina = false;
        //if (stamina <= 20.0f) canRun = false;
        //else canRun = true;
        if (health <= 0.0f) isDead = true;
    }
}
