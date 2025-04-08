using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Move,
    Jump,
    Attack,
    Hit,
    Dead,
    None
}

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class NewPlayerController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int attackPower = 10;

    [Header("Movement")]
    [SerializeField] private float jumpSpeed = 2f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float maxGroundCheckDistance = 10f;

    [Header("Attach Points")]
    [SerializeField] private Transform leftHandTransform;
    [SerializeField] private Transform headTransform;


    public Animator Animator { get; private set; }
    public bool IsGrounded { get { return GetDistanceToGround() < 0.2f; }}
    public PlayerState CurrentState { get; private set; }
    private Dictionary<PlayerState, IPlayerState> _playerStates;


    private CharacterController _characterController;
    private const float _gravity = -9.81f;
    private Vector3 _velocity = Vector3.zero;
    private int _currentHealth = 0;
    
    private PlayerStateIdle _playerStateIdle;
    private PlayerStateJump _playerStateJump;  
    private PlayerStateMove _playerStateMove;
    private PlayerStateAttack _playerStateAttack;
    private PlayerStateHit _playerStateHit;
    private PlayerStateDead _playerStateDead;
    
    private void Awake()
    {
        Animator = this.GetComponent<Animator>();
        _characterController = this.GetComponent<CharacterController>();
    }

    private void Start()
    {
        _playerStateIdle = new PlayerStateIdle();
        _playerStateMove = new PlayerStateMove();
        _playerStateJump = new PlayerStateJump();
        _playerStateAttack = new PlayerStateAttack();
        _playerStateHit = new PlayerStateHit();
        _playerStateDead = new PlayerStateDead();

        _playerStates = new Dictionary<PlayerState, IPlayerState>
        {
            { PlayerState.Idle, _playerStateIdle },
            { PlayerState.Jump, _playerStateJump },
            { PlayerState.Move, _playerStateMove },
            { PlayerState.Attack, _playerStateAttack },
            { PlayerState.Hit, _playerStateHit },
            { PlayerState.Dead, _playerStateDead }
        };

        SetState(PlayerState.Idle);
        _currentHealth = maxHealth;

    }

    public  void  SetState(PlayerState state)
    {
        if (CurrentState != PlayerState.None) { 
            _playerStates[CurrentState].Exit();
        }
        CurrentState = state; 
        _playerStates[CurrentState].Enter(this);
    }

    private void Update()
    {
        if(CurrentState != PlayerState.None)
        {
            _playerStates[CurrentState].Updatae();
        }
    }

    #region  동작 관련

    public void Rotate(float x, float z) {
        var cameraTransform = Camera.main.transform;
        var cameraForward = cameraTransform.forward;
        var cameraRight = cameraTransform.right;

        // Y  값을 0으로  하여 수직 수평 방향만 고려하게 끔
        cameraForward.y = 0;
        cameraRight.y = 0;

        // 입력  방향에  따라 카메라 기준으로 이동 방향 계산
        var moveDirection = cameraForward * z + cameraRight * x;

        if(moveDirection != Vector3.zero)
        {
            // 회전
            moveDirection.Normalize();
            transform.rotation = Quaternion.LookRotation(moveDirection);  
        }
    }

    public void Jump() { 
        _velocity.y =  Mathf.Sqrt(jumpSpeed * -2f * _gravity);
    }

    private void OnAnimatorMove()
    {
        if (Animator == null) return;
        Vector3 movePosition;
        movePosition = Animator.deltaPosition;

        if (IsGrounded)
        {
            movePosition = Animator.deltaPosition;
        }
        else
        {
            movePosition = _characterController.velocity * Time.deltaTime;
        }

        //중력 적용
        _velocity.y += _gravity * Time.deltaTime;
        movePosition.y = _velocity.y * Time.deltaTime;
        _characterController.Move(movePosition);
    }



    private void MeleeAttackStart()
    {
    }

    private void MeleeAttackEnd()
    {
    }

    #endregion


    #region 계산관련
    // 바닥과의 거리 계산 메소드
    public float GetDistanceToGround()
    {
        float maxDistance = 5f;
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, maxDistance, groundLayer))
        {
            return hit.distance;
        }
        else
        {
            return maxDistance;

        }
    }

    #endregion



}
