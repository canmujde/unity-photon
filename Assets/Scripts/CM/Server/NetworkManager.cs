using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void KillEnemy(GameObject enemy)
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.Destroy(enemy);
    }
}