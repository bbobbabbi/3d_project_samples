using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateJump : MonoBehaviour , IPlayerState
{
    private NewPlayerController _playerController;
    private static readonly int Jump = Animator.StringToHash("Jump");   
    public void Enter(NewPlayerController playerController)
    {
        _playerController = playerController;
        _playerController.Animator.SetBool(Jump, true);
    }
    public void Updatae()
    {

    }

    public void Exit()
    {
        _playerController.Animator.SetBool(Jump, false);
        _playerController = null;
    }
}
