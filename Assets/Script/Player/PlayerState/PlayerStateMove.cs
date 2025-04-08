using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMove : MonoBehaviour ,  IPlayerState
{
    private NewPlayerController _playerController;
    private static readonly int Move = Animator.StringToHash("Move");
    private float _speed = 0f;


    public void Enter(NewPlayerController playerController)
    {
        _playerController = playerController;
        _playerController.Animator.SetBool(Move, true);
    }
    public void Updatae()
    {
        var inputVertical = Input.GetAxis("Vertical");
        var inputHorizontal = Input.GetAxis("Horizontal");

        if (inputVertical != 0 || inputHorizontal != 0)
        {
            _playerController.Rotate(inputHorizontal, inputVertical);
        }
        else { 
            _playerController.SetState(PlayerState.Idle);
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift)) {
            if (_speed < 1f)
            {
                _speed += Time.deltaTime;
                _speed = Mathf.Clamp01(_speed);
            }
        }
        else
        {
            if (_speed > 0f)
            {
                _speed -= Time.deltaTime;
                _speed = Mathf.Clamp01(_speed);
            }
        }
        _playerController.Animator.SetFloat("Speed", _speed);

        if (Input.GetButtonDown("Jump") && _playerController.IsGrounded)
        {
            _playerController.SetState(PlayerState.Jump);
            return;
        }

        if (Input.GetButtonDown("Fire1") && _playerController.IsGrounded)
        {
            _playerController.SetState(PlayerState.Attack);
            return;
        }

    }

    public void Exit()
    {
        _playerController?.Animator.SetBool(Move, false);
        _playerController = null;
    }

}
