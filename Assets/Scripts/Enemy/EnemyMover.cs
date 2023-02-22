using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] float speed = 1f;
    private List<Waypoint> path = new List<Waypoint>();

    private void OnEnable()
    {
        FindPath();
        ResetToStart();
        StartCoroutine(FollowPath());
    }

    private void FindPath()
    {
        path.Clear();

        Transform parent = GameObject.FindGameObjectWithTag("Path").transform;
        foreach (Transform child in parent)
        {
            path.Add(child.GetComponent<Waypoint>());
        }
    }

    private void ResetToStart()
    {
        if (path.Count() > 0)
        {
            transform.position = path[0].transform.position;
        }
    }

    private IEnumerator FollowPath()
    {
        foreach (var waypoint in path)
        {
            var startPosition = transform.position;
            var endPosition = waypoint.transform.position;
            transform.LookAt(endPosition);

            float travelPercent = 0f;

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        gameObject.SetActive(false); // disables enemy if gets to the end of the path
    }
}
