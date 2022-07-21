using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float _acceleration = 80;
    [SerializeField] private float _maxVelocity = 10;
    [SerializeField] private float _rotationSpeed = 450;
    private Plane _groundPlane = new(Vector3.up, Vector3.zero);
    private Camera _cam;
    
    private Vector3 _input;
    private Rigidbody _rb;

    public PhotonView View { get; private set; }
    private static readonly int IsRunningAnimatorKey = Animator.StringToHash("isRunning");
    
    
    
    private void Awake() {
        _rb = GetComponent<Rigidbody>();
        _cam = Camera.main;
    }
    private void Start()
    {
        View = GetComponent<PhotonView>();
        
        if (View.IsMine) CameraFollower.Instance.SetTarget(transform);
    }
    private void Update() {
        
        if (!View.IsMine) return;
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        animator.SetBool(IsRunningAnimatorKey, _input != Vector3.zero);
    }

    private void FixedUpdate() {
        if (!View.IsMine) return;
        HandleMovement();
        HandleRotation();
    }

    #region Movement



    private void HandleMovement() {
        _rb.velocity += _input.normalized * (_acceleration * Time.deltaTime);
        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _maxVelocity);
    }

    #endregion

    #region Rotation

    

    private void HandleRotation() {
        var ray = _cam.ScreenPointToRay(Input.mousePosition);

        if (_groundPlane.Raycast(ray, out var enter)) {
            var hitPoint = ray.GetPoint(enter);

            
            var lookPos = hitPoint - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _rotationSpeed);
            
            
            //
            // var dir = hitPoint - transform.position;
            // var rot = Quaternion.LookRotation(dir);
            //
            // transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _rotationSpeed * Time.deltaTime);
        }
    }

    #endregion
}