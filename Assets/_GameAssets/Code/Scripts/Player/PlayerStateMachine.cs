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
    [SerializeField] private FloatVariableSO _rollTime;
    private PlayerMovement _playerMovement;
    private PlayerAnimation _playerAnimation;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAnimation = GetComponent<PlayerAnimation>();
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
            case PlayerState.Roll:
                StartCoroutine(_playerMovement.Roll());
                StartCoroutine(_playerAnimation.SetAnimationOneShot("isRolling", true, _rollTime.Value));
                break;
        }
    }
}
