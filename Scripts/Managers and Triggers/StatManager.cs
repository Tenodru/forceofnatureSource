using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// General stats manager for the current level.
/// </summary>
public class StatManager : MonoBehaviour
{
    #region Singleton
    public static StatManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    public delegate void OnStatChanged();
    public OnStatChanged onStatChanged;                                         //Callback method called whenever a statistic is changed.

    int playerHealth = 0;
    int enemiesKilled = 0;
    int currentEnemyCount = 0;
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePlayerHealth(int amount)
    {
        playerHealth += amount;
    }

    public void ChangeEnemiesKilled(int amount)
    {
        enemiesKilled += amount;
    }

    public void ChangeCurrentEnemyCount(int amount)
    {
        currentEnemyCount += amount;
    }

    public void ChangeScore(int amount)
    {
        score += amount;
    }

    public int GetPlayerHealth()
    {
        return playerHealth;
    }

    public int GetEnemiesKilled()
    {
        return enemiesKilled;
    }

    public int GetCurrentEnemyCount()
    {
        return currentEnemyCount;
    }

    public int GetScore()
    {
        return score;
    }
}
