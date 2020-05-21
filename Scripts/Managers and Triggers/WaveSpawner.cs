using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("Enemies")]                                             //References to enemy assets to be spawned and their spawn costs.
    [SerializeField] GameObject enemy1;
    [SerializeField] int enemy1SpawnCost;
    [SerializeField] float enemy1SpawnChance;
    [SerializeField] float enemy1SpawnTier;
    [SerializeField] GameObject enemy2;
    [SerializeField] int enemy2SpawnCost;
    [SerializeField] float enemy2SpawnChance;
    [SerializeField] float enemy2SpawnTier;

    [Header("Spawnpoint References")]                               //Spawnpoint area center reference points.
    [SerializeField] Transform[] spawnAreas;

    [Header("Other Attributes")]                                    //Other changeable attributes that affect wave spawning.
    [Tooltip("The difficulty of the game, used in calculating the amount of enemies spawned with each 'wave'.")]
    [Range(1, 4)] [SerializeField] int difficulty = 1;
    [Tooltip("How high the spawn budget can go before being capped.")]
    [SerializeField] int spawnBudgetCap = 20;

    [Header("Wave Calculator")]                                     //A calculator that displays how many enemies will be spawned in each wave.
    [Space(20)]
    [Range(1, 4)] [SerializeField] int exampleDifficulty = 1;
    [SerializeField] float exampleTime = 1;
    [SerializeField] int exampleWave = 1;
    [SerializeField] int exampleStartTime = 1;

    int exampleTimeSpawnScale;
    int exampleSpawnTier;

    float nextSpawnTime = 0.0f;
    int enemiesKilled = 0;
    int currentEnemyCount = 0;
    int wave = 1;
    int spawnTier = 1;
    int timeSpawnScale;
    int currentTime;
    int startTime;

    int priorEnemiesKilled = 0;

    int spawnBudget = 0;
    int spawnBudgetThisWave = 0;

    bool enableSpawning = false;

    Requirement spawnStart;
    StatManager statManager;

    // Start is called before the first frame update
    void Start()
    {
        spawnStart = GetComponent<Requirement>();
        statManager = StatManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enableSpawning)
        {
            startTime = (int)Time.time;

            priorEnemiesKilled = statManager.GetEnemiesKilled();

            if (spawnStart.RequirementMet())
            {
                enableSpawning = true;
            }
        }

        else if (enableSpawning)
        {
            currentTime = (int)Time.time - startTime;
            currentEnemyCount = statManager.GetCurrentEnemyCount();

            if ((currentTime / 30) <= 0)
                spawnTier = 1;
            else if ((currentTime / 30) >= 5)
                spawnTier = 5;
            else spawnTier = (currentTime / 30) + 1;

            if (spawnTier == 2)
            {
                enemy1SpawnChance = 0.3f;
                enemy2SpawnChance = 0.65f;
            }
            if (spawnTier >= 3)
            {
                enemy1SpawnChance = 0.4f;
                enemy2SpawnChance = 0.55f;
            }

            if (currentEnemyCount <= difficulty * 2)
            {
                timeSpawnScale = (currentTime / 10);

                if ((int)(((int)(Mathf.Sqrt(wave)) / 2) * difficulty * timeSpawnScale) < 5)
                    spawnBudget = 5;
                else if ((int)(((int)(Mathf.Sqrt(wave)) / 2) * difficulty * timeSpawnScale) >= 20)
                    spawnBudget = 20;
                else spawnBudget = (int)(((int)(Mathf.Sqrt(wave)) / 2) * difficulty * timeSpawnScale);

                spawnBudgetThisWave = spawnBudget;
                wave += 1;
                SpawnEnemies();
            }
        }
    }

    /// <summary>
    /// Spawns enemies when called.
    /// </summary>
    public void SpawnEnemies()
    {
        Quaternion rot = new Quaternion(0, 0, 0, 0);
        int spawnAmount = (int)((Mathf.Sqrt(wave) / 2) * difficulty * 3);

        while (spawnBudget > 0)
        {
            int point = Random.Range(0, spawnAreas.Length);

            float randPointX = Random.Range(spawnAreas[point].position.x + spawnAreas[point].GetComponent<Renderer>().bounds.size.x / 2, spawnAreas[point].position.x - spawnAreas[point].GetComponent<Renderer>().bounds.size.x / 2);
            float randPointZ = Random.Range(spawnAreas[point].position.z + spawnAreas[point].GetComponent<Renderer>().bounds.size.z / 2, spawnAreas[point].position.z - spawnAreas[point].GetComponent<Renderer>().bounds.size.z / 2);

            Vector3 randPoint = new Vector3(randPointX, spawnAreas[point].position.y, randPointZ);

            float chance = Random.Range(0, 1.0f);
            if (chance <= enemy1SpawnChance)
            {
                Instantiate(enemy1, randPoint, rot);
                statManager.ChangeCurrentEnemyCount(1);
                spawnBudget -= enemy1SpawnCost;
            }
            else
            {
                Instantiate(enemy2, randPoint, rot);
                statManager.ChangeCurrentEnemyCount(1);
                spawnBudget -= enemy2SpawnCost;
            }
        }
    }

    public void EnableSpawning()
    {
        enableSpawning = true;
    }


    public int UICalculateSpawnBudget()
    {
        int exampleSpawnBudget = 0;

        exampleTimeSpawnScale = (int)((exampleTime - exampleStartTime) / 10);

        if ((int)((exampleTime - exampleStartTime) / 30) <= 0)
            exampleSpawnTier = 1;
        else if ((int)((exampleTime - exampleStartTime) / 30) >= 5)
            exampleSpawnTier = 5;
        else exampleSpawnTier = (int)((exampleTime - exampleStartTime) / 30);

        if ((int)(((int)(Mathf.Sqrt(exampleWave)) / 2) * exampleDifficulty * exampleTimeSpawnScale) < 5)
            exampleSpawnBudget = 5;
        else if ((int)(((int)(Mathf.Sqrt(exampleWave)) / 2) * exampleDifficulty * exampleTimeSpawnScale) >= 20)
            exampleSpawnBudget = 20;
        else exampleSpawnBudget = (int)(((int)(Mathf.Sqrt(exampleWave)) / 2) * exampleDifficulty * exampleTimeSpawnScale);

        return exampleSpawnBudget;
    }

    public int UITimeScale()
    {
        return exampleTimeSpawnScale;
    }

    public int UISpawnTier()
    {
        return exampleSpawnTier;
    }

}
