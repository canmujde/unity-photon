using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody rb;
    public float force;
    public void Initialize(Vector3 dir)
    {
        rb.AddForce(transform.position - dir * force);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        // if (!PhotonNetwork.IsMasterClient) return;
        // if (!View.IsMine) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            
            var enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy.Destroyed) return;
            enemy.Destroyed = true;
            
            NetworkManager.Instance.KillEnemy(enemy.gameObject);
        }
    }
}
