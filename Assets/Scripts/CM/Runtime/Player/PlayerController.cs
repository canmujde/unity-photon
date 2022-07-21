using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 450;
    public LayerMask IgnoreMe;
    public Transform turret;
    private Camera _cam;

    public GameObject ball;
    private Vector3 _input;
    private Rigidbody _rb;

    public PhotonView View;
    private static readonly int IsRunningAnimatorKey = Animator.StringToHash("isRunning");


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _cam = Camera.main;
    }

    private void Start()
    {
        View = GetComponent<PhotonView>();


        if (View.IsMine)
        {
            gameObject.name = "Mine";
            CameraFollower.Instance.SetTarget(transform);
        }
    }

    private void Update()
    {
        if (!View.IsMine) return;
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    private void Fire()
    {
        Ball ball = PhotonNetwork.Instantiate("Ball", turret.position, Quaternion.identity).GetComponent<Ball>();
        ball.Initialize(-lastDirection);
    }

    private void FixedUpdate()
    {
        if (!View.IsMine) return;
        HandleRotation();
    }

    #region Movement

    private Vector3 MouseHitPoint()
    {
        var ray = _cam.ScreenPointToRay(Input.mousePosition);
        return !Physics.Raycast(ray, out var hit, 1000f, ~IgnoreMe) ? Vector3.zero : hit.point;
    }

    private Vector3 Direction()
    {
        var dir = MouseHitPoint() == Vector3.zero ? lastDirection :  MouseHitPoint() - _rb.position;
        dir.y = 0;
        return dir;
    }
    

    #endregion

    #region Rotation

    private Vector3 lastDirection;
    private void HandleRotation()
    {
        lastDirection = Direction() == Vector3.zero ? lastDirection : Direction();
        var rotation = Quaternion.LookRotation(lastDirection);
        turret.rotation = Quaternion.Lerp(turret.rotation, rotation, Time.deltaTime * _rotationSpeed);
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

    #endregion
}