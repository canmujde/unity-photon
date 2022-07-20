using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothFactor;

    public static CameraFollower Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SetTarget(Transform t)
    {
        target = t;
        transform.position = target.transform.position + offset;

    }
    private void FixedUpdate () 
    {
        if (!target) return;

        Move();
        
    }

    private void Move()
    {
        var lerp = Vector3.Lerp(transform.position, target.transform.position + offset, Time.deltaTime * smoothFactor);
        transform.position = lerp;
    }
}
