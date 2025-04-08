using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAttack : MonoBehaviour , IPlayerState
{
    private NewPlayerController _playerController;
    public void Enter(NewPlayerController playerController)
    {
        _playerController = playerController;
        _playerController.Animator.SetTrigger("Attack");
    }
    public void Updatae()
    {

    }

    public void Exit()
    {
        _playerController = null;
    }
}
