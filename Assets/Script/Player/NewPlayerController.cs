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
    public PlayerState CurrentState { get; private set; } = PlayerState.Idle;
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
        Animator  =  GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        _playerStateIdle = new PlayerStateIdle();
        _playerStateJump = new PlayerStateJump();
        _playerStateMove = new PlayerStateMove();
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

    #region 에니메이터 관련
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
    private float GetDistanceToGround()
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
