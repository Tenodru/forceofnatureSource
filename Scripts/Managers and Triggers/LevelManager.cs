using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] savepoints;

    public void ClearSavepoints()
    {
        foreach (GameObject s in savepoints)
        {
            s.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }

}
