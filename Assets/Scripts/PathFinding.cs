using System;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    EnemySpawner enemySpawner;
    WaveConfigSO waveConfigSO;
    Transform[] waypoints;
    int waypointIndex = 0;

    void Start()
    {
        enemySpawner = FindFirstObjectByType<EnemySpawner>();
        waveConfigSO = enemySpawner.GetCurrentWave();

        waypoints = waveConfigSO.GetWaypoints();
        transform.position = waveConfigSO.GetStartingWaypoint().position;
    }

    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if (waypointIndex < waypoints.Length)
        {
            Vector3 targetPos = waypoints[waypointIndex].position;
            float moveDelta = waveConfigSO.GetEnemyMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveDelta);
        
            if (transform.position == targetPos)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
