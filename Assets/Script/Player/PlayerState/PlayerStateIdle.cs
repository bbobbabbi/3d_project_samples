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

    }

    public void Exit()
    {
        _playerController.Animator.SetBool(Idle, false);
        _playerController = null;
    }

}
