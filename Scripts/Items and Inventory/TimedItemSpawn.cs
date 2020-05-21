using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedItemSpawn : MonoBehaviour
{
    [Header("Spawner Settings")]
    [Tooltip("An array of items to be spawned.")]
    public TimedSpawnItem[] items;

    bool spawnTimerEnabled = false;
    float timeSinceToggled = 0;
    float spawnTimer = 0;

    // Update is called once per frame
    void Update()
    {
        if (spawnTimerEnabled)
        {
            spawnTimer = Time.time - timeSinceToggled;
        }

        SpawnItems();
    }

    /// <summary>
    /// Spawns the items in the item array.
    /// </summary>
    void SpawnItems()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (spawnTimer >= items[i].time && !items[i].hasBeenSpawned)
            {
                if (items[i].timedSpawnType == TimedSpawnType.Activate)
                {
                    items[i].item.SetActive(true);
                    items[i].hasBeenSpawned = true;
                    if (items[i].spawnSound != null && items[i].item.GetComponent<AudioSource>() != null)
                    {
                        items[i].item.GetComponent<AudioSource>().PlayOneShot(items[i].spawnSound);
                    }
                }
                else if (items[i].timedSpawnType == TimedSpawnType.Instantiate)
                {
                    Instantiate(items[i].item);
                    items[i].hasBeenSpawned = true;
                    if (items[i].spawnSound != null && items[i].item.GetComponent<AudioSource>() != null)
                    {
                        items[i].item.GetComponent<AudioSource>().PlayOneShot(items[i].spawnSound);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Starts the spawn timer.
    /// </summary>
    public void StartSpawnTimer()
    {
        spawnTimerEnabled = true;
        timeSinceToggled = Time.time;
    }

    /// <summary>
    /// Stops the spawn timer.
    /// </summary>
    public void StopSpawnTimer()
    {
        spawnTimerEnabled = false;
        timeSinceToggled = Time.time;
    }
}

/// <summary>
/// An item that spawns after a set period of time.
/// </summary>
[System.Serializable]
public class TimedSpawnItem
{
    [Tooltip("The item to be spawned.")]
    public GameObject item;
    [Tooltip("When this item should spawn.")]
    public float time;
    [Tooltip("How this item should appear.")]
    public TimedSpawnType timedSpawnType;
    [Tooltip("The sound that should play when this item is spawned. Will only play if the spawned item has an AudioSource.")]
    public AudioClip spawnSound;

    [HideInInspector] public bool hasBeenSpawned = false;

    public TimedSpawnItem(GameObject newItem, float newTime)
    {
        item = newItem;
        time = newTime;
    }
}

/// <summary>
/// How this object should be spawned.
/// </summary>
[System.Serializable]
public enum TimedSpawnType
{
    [Tooltip("Enables a hidden object.")]
    Activate,
    [Tooltip("Spawns a prefab.")]
    Instantiate
}
