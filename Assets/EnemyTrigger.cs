using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{

    private Enemy _enemyController;
    
    public void Initialize(Enemy controller)
    {
        _enemyController = controller;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var pc = collision.gameObject.GetComponent<PlayerController>();
            if (!pc.View.IsMine) return;
            
            PhotonNetwork.Destroy(_enemyController.gameObject);
        }
    }
    
}
