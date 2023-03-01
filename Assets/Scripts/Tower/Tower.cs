using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    [SerializeField] float buildTimer = 1f;

    Bank bank;

    private void Start()
    {
        StartCoroutine(Build());
    }

    private IEnumerator Build()
    {
        // var children = transform.GetComponentsInChildren<Transform>();
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(false);
            }
        }
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildTimer);
            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(true);
            }
        }
    }

    public bool CreateTower(Tower towerPrefab, Vector3 position)
    {
        bank = FindObjectOfType<Bank>();

        if (bank == null)
        {
            return false;
        }

        if (bank.CurrentBalance >= cost)
        {
            Instantiate(towerPrefab, position, Quaternion.identity);
            bank.Withdraw(cost);
            return true;
        }

        return false;
    }
}
