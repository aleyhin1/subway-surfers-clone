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
    [SerializeField] private BoolVariableSO _isGrounded;
    private PlayerMovement _playerMovement;
    private PlayerAnimation _playerAnimation;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void FixedUpdate()
    {
        CheckGround();
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

    private void CheckGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit result, _playerMovement.PlayerHeight * .5f + .2f))
        {
            float angle = Vector3.Angle(Vector3.up, result.normal);

            if (angle == 0)
            {
                _isGrounded.Value = true;
                _playerAnimation.SetAnimationOneShot("IsGrounded", true);
            }
        }
        else
        {
            _isGrounded.Value = false;
            _playerAnimation.SetAnimationOneShot("IsGrounded", false);
        }
    }
}
