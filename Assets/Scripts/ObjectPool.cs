using System;
using System.Collections;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] [Range(0,5)] float spawnTimer = 1f;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int poolSize = 5;
    private GameObject[] pool;
    private Transform pathStart;

    private void Awake()
    {
        PopulatePool();
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
             pool[i] = Instantiate(enemyPrefab, transform);
             pool[i].SetActive(false);
        }
    }

    private IEnumerator SpawnEnemy()
    {
        while (Application.isPlaying)
        {
            EnableObjectsInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    private void EnableObjectsInPool()
    {
        foreach (GameObject item in pool)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                return;
            }
        }
    }
}
