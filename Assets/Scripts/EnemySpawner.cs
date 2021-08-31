using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject meleePrefab;
    public GameObject rangedPrefab;

    void Spawn(GameObject enemy, Transform pos)
    {
        Instantiate(enemy, pos.position, Quaternion.identity);
    }
}
