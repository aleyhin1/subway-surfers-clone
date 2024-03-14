using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private BoolVariableSO _isMoving;
    [SerializeField] private FloatVariableSO _movementDistance;
    [SerializeField] private BoolVariableSO _isGrounded;
    [SerializeField] private BoolVariableSO _isRolling;
    private PlayerStateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = GetComponent<PlayerStateMachine>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.A) && CanMoveLeft())
        {
            _stateMachine.UpdateState(PlayerState.TurnLeft);
        }
        else if (Input.GetKeyUp(KeyCode.D) && CanMoveRight())
        {
            _stateMachine.UpdateState(PlayerState.TurnRight);
        }
        else if (Input.GetKeyUp(KeyCode.W) && CanJump())
        {
            _stateMachine.UpdateState(PlayerState.Jump);
        }
        else if (Input.GetKeyUp(KeyCode.S) && CanRoll())
        {
            _stateMachine.UpdateState(PlayerState.Roll);
        }
    }

    private bool CanMoveLeft()
    {
        return (Math.Round(transform.position.x - 1) > -_movementDistance.Value) && !_isMoving.Value;
    }

    private bool CanMoveRight()
    {
        return (Math.Round(transform.position.x + 1) < _movementDistance.Value) && !_isMoving.Value;
    }

    private bool CanJump()
    {
        return _isGrounded.Value && !_isRolling.Value;
    }

    private bool CanRoll()
    {
        return !_isRolling.Value && _isGrounded.Value;
    }
}
