using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private float spawnRadius;
    [SerializeField] private float spawnY;
    

    private void Start()
    {
        var randomPosition = (Vector3)Random.insideUnitCircle * spawnRadius;
        randomPosition.y = spawnY;

        PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
    }
}
