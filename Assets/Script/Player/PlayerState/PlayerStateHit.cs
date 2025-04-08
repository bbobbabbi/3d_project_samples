using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateHit : MonoBehaviour , IPlayerState
{
    private NewPlayerController _playerController;
    public void Enter(NewPlayerController playerController)
    {
        _playerController = playerController;
    }
    public void Updatae()
    {

    }

    public void Exit()
    {
        _playerController = null;
    }

}
