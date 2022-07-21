using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private PlayerController[] players;
    [SerializeField] private Transform nearestPlayer;
    [SerializeField] private float speed;
    [SerializeField] private EnemyTrigger enemyTrigger;
    
    private void Start()
    {
        enemyTrigger.Initialize(this);
        players = FindObjectsOfType<PlayerController>();
    }


    private void Update()
    {
        nearestPlayer = GetClosestPlayer();

        if (nearestPlayer)
        {
            var position = nearestPlayer.position;
            transform.position =
                Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
            
            var lookPos = position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
        }
    }

    Transform GetClosestPlayer()
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (PlayerController potentialTarget in players)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }

        return bestTarget;
    }

   

    // private Transform GetClosestPlayer()
    // {
    //     Transform tMin = null;
    //     float minDist = Mathf.Infinity;
    //     Vector3 currentPos = transform.position;
    //     foreach (PlayerController t in players)
    //     {
    //         float dist = Vector3.Distance(t.transform.position, currentPos);
    //         if (dist < minDist)
    //         {
    //             tMin = t.transform;
    //             minDist = dist;
    //         }
    //     }
    //     return tMin;
    // }
}