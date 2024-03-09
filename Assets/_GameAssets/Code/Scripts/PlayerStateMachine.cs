using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    TurnLeft,
    TurnRight,
    Jump,
    Roll,
    Fall,
}

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    public void UpdateState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.TurnLeft:
                _playerMovement.MoveLeft();
                break;
            case PlayerState.TurnRight:
                _playerMovement.MoveRight();
                break;
            case PlayerState.Jump:
                _playerMovement.Jump();
                break;
        }
    }
}
