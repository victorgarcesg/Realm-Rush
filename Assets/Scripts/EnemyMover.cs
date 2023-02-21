using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] float waitTime = 1f;

    private void Start()
    {
        StartCoroutine(FollowPath());
    }

    private IEnumerator FollowPath()
    {
        foreach (var waypoint in path)
        {
            var position = waypoint.transform.position;
            transform.position = new Vector3(position.x, transform.position.y, position.z);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
