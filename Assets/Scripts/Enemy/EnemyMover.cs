using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] float speed = 1f;
    private List<Node> path = new List<Node>();
    Enemy enemy;
    GridManager gridManager;
    Pathfinder pathfinder;

    private void OnEnable()
    {
        FindPath();
        ResetToStart();
        StartCoroutine(FollowPath());
    }

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    private void FindPath()
    {
        path.Clear();
        path = pathfinder.GetNewPath();
    }

    private void ResetToStart()
    {
        if (path.Count() > 0)
        {
            transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
        }
    }

    private IEnumerator FollowPath()
    {
        for (int i = 0; i < path.Count; i++)
        {
            var startPosition = transform.position;
            var endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            transform.LookAt(endPosition);

            float travelPercent = 0f;

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
    }

    private void FinishPath()
    {
        gameObject.SetActive(false); // disables enemy if gets to the end of the path
        enemy.StealGold();
    }
}
