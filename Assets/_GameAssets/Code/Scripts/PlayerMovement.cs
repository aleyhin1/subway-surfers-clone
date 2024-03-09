using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FloatVariableSO _movementDistance;
    [SerializeField] private FloatVariableSO _jumpDistance;
    [SerializeField] private BoolVariableSO _isMoving;
    [SerializeField] private BoolVariableSO _isGrounded;
    [SerializeField] private float _movementForce;
    [SerializeField] private float _jumpForce;
    private Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void MoveLeft()
    {
        StartCoroutine(Move(false));
    }

    public void MoveRight()
    {
        StartCoroutine(Move(true));
    }

    public void Jump()
    {
        _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    public IEnumerator Move(bool toRight)
    {
        _isMoving.Value = true;

        int direction = toRight ? 1 : -1;
        Vector3 initialPosition = transform.position;

        _rigidBody.AddForce(direction * Vector3.right * _movementForce, ForceMode.Impulse);

        while (Mathf.Abs(transform.position.x - initialPosition.x) < _movementDistance.Value)
        {
            yield return new WaitForFixedUpdate();
        }

        Vector3 stoppedVelocity = new Vector3(0, _rigidBody.velocity.y, _rigidBody.velocity.z);
        _rigidBody.velocity = stoppedVelocity;

        _isMoving.Value = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        _isGrounded.Value = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _isGrounded.Value = false;
    }
}
