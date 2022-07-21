using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private float spawnDuration;
    private float _duration;

    private void Start()
    {
        _duration = spawnDuration;
    }

    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient || PhotonNetwork.CurrentRoom.PlayerCount < 2) return;


        if (_duration <= 0)
        {
            Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            PhotonNetwork.Instantiate(enemyPrefab.name, spawnPosition, Quaternion.identity);

            _duration = spawnDuration;
        }
        else
        {
            _duration -= Time.deltaTime;
        }
    }
}
