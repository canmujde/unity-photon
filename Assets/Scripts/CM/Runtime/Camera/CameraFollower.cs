using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    public static CameraFollower Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }
    private void Update () 
    {
        if (target is null) return;

        Move();
        
    }

    private void Move()
    {
        transform.position = target.transform.position + offset;
    }
}
