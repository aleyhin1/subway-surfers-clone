using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FloatVariableSO _movementDistance;
    [SerializeField] private FloatVariableSO _rollTime;
    [SerializeField] private BoolVariableSO _isTurning;
    [SerializeField] private BoolVariableSO _isRolling;
    [SerializeField] private float _movementForce;
    [SerializeField] private float _jumpForce;
    [SerializeField] private BoxCollider _rollCollider;
    private Rigidbody _rigidBody;
    private BoxCollider _mainCollider;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _mainCollider = GetComponent<BoxCollider>();
    }

    public void MoveLeft()
    {
        StartCoroutine(Turn(false));
    }

    public void MoveRight()
    {
        StartCoroutine(Turn(true));
    }

    public void Jump()
    {
        _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    public IEnumerator Turn(bool toRight)
    {
        _isTurning.Value = true;

        int direction = toRight ? 1 : -1;
        Vector3 initialPosition = transform.position;

        _rigidBody.AddForce(direction * Vector3.right * _movementForce, ForceMode.Impulse);

        while (Mathf.Abs(transform.position.x - initialPosition.x) < _movementDistance.Value)
        {
            yield return new WaitForFixedUpdate();
        }

        Vector3 stoppedVelocity = new Vector3(0, _rigidBody.velocity.y, _rigidBody.velocity.z);
        _rigidBody.velocity = stoppedVelocity;

        _isTurning.Value = false;
    }

    public IEnumerator Roll()
    {
        _mainCollider.enabled = false;
        _rollCollider.enabled = true;

        _isRolling.Value = true;

        yield return new WaitForSeconds(_rollTime.Value);

        _isRolling.Value = false;

        _mainCollider.enabled = true;
        _rollCollider.enabled = false;
    }
}
