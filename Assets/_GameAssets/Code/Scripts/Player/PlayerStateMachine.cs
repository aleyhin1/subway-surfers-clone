using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Codice.Client.Commands.WkTree.WorkspaceTreeNode;

public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] private FloatVariableSO _rollTime;
    [SerializeField] private BoolVariableSO _isGrounded;
    [SerializeField] private GameStateEventChannelSO _onPlayerDeath;
    private PlayerMovement _playerMovement;
    private PlayerAnimation _playerAnimation;
    private PlayerHitboxController _playerHitboxController;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerHitboxController = GetComponentInChildren<PlayerHitboxController>();
    }

    private void Update()
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
                StartCoroutine(_playerMovement.Roll(_rollTime.Value));
                StartCoroutine(_playerHitboxController.ToggleRollHitbox(_rollTime.Value));
                StartCoroutine(_playerAnimation.SetAnimationOneShot("isRolling", true, _rollTime.Value));
                break;
            case PlayerState.Hit:
                break;
            case PlayerState.Death:
                _playerAnimation.SetAnimationOneShot("IsDeath", true);
                _onPlayerDeath.RaiseEvent(GameState.Over);
                break;
        }
    }

    private void CheckGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        if (IsOnGround(ray))
        {
            _isGrounded.Value = true;
            _playerAnimation.SetAnimationOneShot("IsGrounded", true);
        }
        else if (IsOnSlope(ray))
        {
            _isGrounded.Value = true;
            _playerAnimation.SetAnimationOneShot("IsGrounded", true);
        }
        else
        {
            _isGrounded.Value = false;
            _playerAnimation.SetAnimationOneShot("IsGrounded", false);
        }
    }

    private bool IsOnGround(Ray ray)
    {
        float? surfaceAngle = GetSurfaceAngle(ray);
        
        if (surfaceAngle.HasValue)
        {
            return surfaceAngle.Value == 0;
        }
        else
        {
            return false;
        }
    }

    private bool IsOnSlope(Ray ray)
    {
        float? surfaceAngle = GetSurfaceAngle(ray);

        if (surfaceAngle.HasValue)
        {
            return surfaceAngle.Value > 0;
        }
        else
        {
            return false;
        }
    }

    private float? GetSurfaceAngle(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit result, _playerMovement.PlayerHeight * .5f + .05f))
        {
            float angle = Vector3.Angle(Vector3.up, result.normal);

            return angle;
        }

        return null;
    }
}
