using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] [Range(0,5)] float generationFrequency = 1f;
    [SerializeField] GameObject enemyPrefab;
    Transform pathStart;

    private void Start()
    {
        StartCoroutine(EnemyGeneration());
    }

    private IEnumerator EnemyGeneration()
    {
        while (Application.isPlaying)
        {
            Instantiate(enemyPrefab, transform);
            yield return new WaitForSeconds(generationFrequency);
        }
    }
}
