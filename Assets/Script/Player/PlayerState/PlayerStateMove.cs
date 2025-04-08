using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMove : MonoBehaviour ,  IPlayerState
{
    private NewPlayerController _playerController;
    private static readonly int Move = Animator.StringToHash("Move");

    public void Enter(NewPlayerController playerController)
    {
        _playerController = playerController;
        _playerController.Animator.SetBool(Move, true);
    }
    public void Updatae()
    {

    }

    public void Exit()
    {
        _playerController.Animator.SetBool(Move, false);
        _playerController = null;
    }

}
