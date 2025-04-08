using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateIdle :  MonoBehaviour , IPlayerState
{
    private NewPlayerController _playerController;
    private static readonly int Idle = Animator.StringToHash("Idle");
    public void Enter(NewPlayerController playerController)
    {
        _playerController = playerController;
        _playerController.Animator.SetBool(Idle, true);
    }
    public void Updatae()
    {
        var inputVertical = Input.GetAxis("Vertical");
        var inputHorizontal = Input.GetAxis("Horizontal");
        
        if(inputVertical != 0 || inputHorizontal != 0)
        {
            _playerController.Rotate(inputHorizontal, inputVertical);
            _playerController.SetState(PlayerState.Move);
            return;
        }
        
        if (Input.GetButtonDown("Jump"))
        {
            _playerController.SetState(PlayerState.Jump);
            return;
        }

        if(Input.GetButtonDown("Fire1"))
        {
            _playerController.SetState(PlayerState.Attack);
            return;
        }

    }

    public void Exit()
    {
        _playerController?.Animator.SetBool(Idle, false);
        _playerController = null;
    }

}
