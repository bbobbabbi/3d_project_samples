using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateJump : MonoBehaviour, IPlayerState
{
    private NewPlayerController _playerController;
    private static readonly int Jump = Animator.StringToHash("Jump");
    public void Enter(NewPlayerController playerController)
    {
        _playerController = playerController;
        _playerController.Animator.SetBool(Jump, true);
        _playerController.Jump();
    }
    public void Updatae()
    {
        var distanceToGround = _playerController.GetDistanceToGround();
        if (distanceToGround < 0.1f)
        {
            _playerController.SetState(PlayerState.Idle);
        }
        else
        {
            _playerController.Animator.SetFloat("GroundDistance", distanceToGround);
        }

    }

    public void Exit()
    {
        _playerController?.Animator.SetBool(Jump, false);
        _playerController = null;
    }
}
