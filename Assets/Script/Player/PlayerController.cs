using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float jumpForce = 5;
    [SerializeField]
    private float rotateSpeed = 100f;
    [SerializeField]
    private LayerMask groundLayer;

    private const float _gravity = -9.81f;

    private static readonly int Move = Animator.StringToHash("Move");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int GroundDistance = Animator.StringToHash("GroundDistance");

    private Animator _animator;
    private CharacterController _characterController  ;  
    private Vector3 _velocity;
    private float _groundDistance;
    private float runForce = 1f;
    private float addForce=5;
    private float _groundedMinDistance = 0.1f;
    private float _runSpeed;

    private bool IsGrounded
    {
        get
        {
            _groundDistance = GetDistanceToGround();
            return _groundDistance < _groundedMinDistance;
        }
    }
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        //커서락 해재
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        HandleMovement();
        CheckRun();


        _animator.SetFloat(GroundDistance, GetDistanceToGround());
        //  ApplyGravity();
    }

    // 플레이어 입력처리 이동 메소드
    private void HandleMovement() {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (Mathf.Abs(vertical) > 0)
        {
            _animator.SetBool(Move, true);
        }
        else { 
            _animator.SetBool(Move, false);
        }

        Vector3 movement = transform.forward * vertical * runForce;
        transform.Rotate(0, horizontal * rotateSpeed * Time.deltaTime, 0);


        //_characterController.Move(movement * Time.deltaTime);


        if (Input.GetButtonDown("Jump"))
        {
            _velocity.y = Mathf.Sqrt(jumpForce * -2f * _gravity);
            _animator.SetTrigger(Jump);
        }

        /*
        //달리기
        float speed = 0;
       
        if (Input.GetKey(KeyCode.LeftShift))
        {
            runForce = Mathf.Lerp(runForce, addForce, Time.deltaTime * 2f);
            speed = (runForce - 1) / (addForce - 1);
        }
        else {
            runForce = Mathf.Lerp(runForce, 1, Time.deltaTime * 2f);
            speed = (runForce - 1) / (addForce - 1);
        }
        _animator.SetFloat(Speed, speed);*/


    }


    //달리기 처리
    private void CheckRun() {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _runSpeed += Time.deltaTime;
            _runSpeed = Mathf.Clamp01(_runSpeed);
        }
        else {
            _runSpeed -= Time.deltaTime;
            _runSpeed = Mathf.Clamp01(_runSpeed);
        }
            _animator.SetFloat(Speed, _runSpeed);
    }

    // 중력 적용 메소드
    private void ApplyGravity() { 
        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }

  
    // 바닥과의 거리 계산 메소드
    private float GetDistanceToGround() {
        float maxDistance = 5f;
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, maxDistance,groundLayer))
        {
            return hit.distance;
        }
        else {
            return maxDistance;

        }
    }
   
    private void RotatePlayerToCameraForward() { 
    
    }

    #region Animator Method
    private void OnAnimatorMove()
    {
        Vector3 movePosition;
        movePosition = _animator.deltaPosition;
        
        if(IsGrounded)
        {
            movePosition = _animator.deltaPosition;
        }
        else
        {
            movePosition = _characterController.velocity * Time.deltaTime;
        }

        //중력 적용
        _velocity.y += _gravity * Time.deltaTime;
        movePosition.y = _velocity.y;
        _characterController.Move(movePosition);
    }
    #endregion

    #region Debug
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector3.down * 5f);
    }

    #endregion
}
