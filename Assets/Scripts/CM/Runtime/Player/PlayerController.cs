using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;

    private PhotonView _view;
    private static readonly int IsRunningAnimatorKey = Animator.StringToHash("isRunning");

    private void Start()
    {
        _view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!_view.IsMine) return;

        Move();
    }

    private void Move()
    {
        var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        var movement = speed * Time.deltaTime * input.normalized;

        transform.position += new Vector3(movement.x, 0, movement.y);

        animator.SetBool(IsRunningAnimatorKey, movement == Vector2.zero);
    }
}