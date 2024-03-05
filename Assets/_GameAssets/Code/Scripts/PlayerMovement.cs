using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FloatVariableSO _movementDistance;
    [SerializeField] private AnimationCurve _movementCurve;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private BoolVariableSO _isMoving;


    public void MoveLeft()
    {
        StartCoroutine(Move(-_movementDistance.Value));
    }

    public void MoveRight()
    {
        StartCoroutine(Move(_movementDistance.Value));
    }

    private IEnumerator Move(float distance)
    {
        _isMoving.Value = true;
        Vector3 initialPosition = transform.position;
        Vector3 positionToMove = new Vector3(initialPosition.x + distance, initialPosition.y, initialPosition.z);

        while (Mathf.Abs(transform.position.x - positionToMove.x) > .0001f)
        {
            float distanceMoved = Mathf.Abs(transform.position.x - initialPosition.x);
            float step = _movementCurve.Evaluate(distanceMoved);
            transform.position = Vector3.MoveTowards(transform.position, positionToMove, step * Time.deltaTime * _movementSpeed);

            yield return new WaitForEndOfFrame();
        }
        _isMoving.Value = false;
    }
}
