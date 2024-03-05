using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private BoolVariableSO _isMoving;
    [SerializeField] private FloatVariableSO _movementDistance;
    private PlayerStateMachine _stateMachine;

    private void Start()
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
        else if (Input.GetKeyUp(KeyCode.W))
        {

        }
        else if (Input.GetKeyUp(KeyCode.S))
        {

        }
    }

    private bool CanMoveLeft()
    {
        return (Math.Round(transform.position.x, 2) > -_movementDistance.Value) && !_isMoving.Value;
    }

    private bool CanMoveRight()
    {
        return (Math.Round(transform.position.x, 2) < _movementDistance.Value) && !_isMoving.Value;
    }
}
