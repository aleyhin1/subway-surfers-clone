using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum MovementLine
{
    Left,
    Middle,
    Right,
}

public class PlayerMovement : MonoBehaviour
{
    public float PlayerHeight { get; private set; }
    [SerializeField] private FloatVariableSO _movementDistance;
    [SerializeField] private BoolVariableSO _isTurning;
    [SerializeField] private BoolVariableSO _isRolling;
    [SerializeField] private float _movementTime;
    [SerializeField] private float _jumpForce;
    [SerializeField] private BoxCollider _rollCollider;
    [SerializeField] private LayerMask _obstacleLayer;
    private Rigidbody _rigidBody;
    private CapsuleCollider _mainCollider;
    Dictionary<MovementLine, float> _linePositionXPairs = new Dictionary<MovementLine, float>();
    private MovementLine _currentLine = MovementLine.Middle;
    private float _forceMagnitude;
    private IEnumerator _moveCoroutine;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _mainCollider = GetComponent<CapsuleCollider>();
        PlayerHeight = _mainCollider.height;

        CalculateLinePositions();
        CalculateForceMagnitude();
    }

    public void MoveLeft()
    {
        StartCoroutine(_moveCoroutine = Move(-1));
    }

    public void MoveRight()
    {
        StartCoroutine(_moveCoroutine = Move(1));
    }

    public void Jump()
    {
        _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    public IEnumerator Move(int direction)
    {
        _isTurning.Value = true;
        _rigidBody.AddForce(direction * Vector3.right * _forceMagnitude, ForceMode.Impulse);

        while (Mathf.Abs(_rigidBody.position.x - _linePositionXPairs[_currentLine + direction]) > .1f)
        {
            yield return new WaitForFixedUpdate();
        }

        _rigidBody.velocity = Vector3.zero;
        _isTurning.Value = false;
        _currentLine += direction;
    }

    public IEnumerator Roll(float time)
    {
        _mainCollider.enabled = false;
        _rollCollider.enabled = true;

        _isRolling.Value = true;

        yield return new WaitForSeconds(time);

        _isRolling.Value = false;

        _mainCollider.enabled = true;
        _rollCollider.enabled = false;
    }

    public void ResetPosition()
    {
        if (_moveCoroutine == null) return;

        StopCoroutine(_moveCoroutine);

        float direction = GetDirection();
        if (direction == 0) return;

        _currentLine = _currentLine - (int)direction;
        StartCoroutine(_moveCoroutine = Move((int)direction));
    }
    
    public IEnumerator DeactivateObstacleCollision(float time)
    {
        _rollCollider.excludeLayers = _obstacleLayer;
        _mainCollider.excludeLayers = _obstacleLayer;

        yield return new WaitForSeconds(time);

        _rollCollider.excludeLayers = 0;
        _mainCollider.excludeLayers = 0;
    }

    private void CalculateLinePositions()
    {
        float positionX = -_movementDistance.Value;

        foreach (MovementLine line in Enum.GetValues(typeof(MovementLine)))
        {
            _linePositionXPairs.Add(line, positionX);

            positionX += _movementDistance.Value;
        }
    }

    private void CalculateForceMagnitude()
    {
        float _acceleration = 2 * _movementDistance.Value / (_movementTime * _movementTime);
        _forceMagnitude = _rigidBody.mass * _acceleration;
    }

    private float GetDirection()
    {
        float threshold = .2f;
        float distanceToGo = _rigidBody.position.x - _linePositionXPairs[_currentLine];
        if (Mathf.Abs(distanceToGo) < threshold)
        {
            return 0;
        }
        else
        {
            return -Mathf.Sign(distanceToGo);
        }
    }
}
