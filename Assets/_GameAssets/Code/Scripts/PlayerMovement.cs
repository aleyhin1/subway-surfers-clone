using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField, Range(0, 100)] private float _movementDistance;
    [SerializeField] private AnimationCurve _movementCurve;
    [SerializeField] private float _movementSpeed;
    private bool _isMoving;

    

    private void Update()
    {
        if (!_isMoving)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > -_movementDistance)
            {
                StartCoroutine(Move(-_movementDistance));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < _movementDistance)
            {
                StartCoroutine(Move(_movementDistance));
            }
        }
    }

    private IEnumerator Move(float distance)
    {
        _isMoving = true;
        Vector3 initialPosition = transform.position;
        Vector3 positionToMove = new Vector3(initialPosition.x + distance, initialPosition.y, initialPosition.z);

        while (Mathf.Abs(transform.position.x - positionToMove.x) > .0001f)
        {
            float distanceMoved = Mathf.Abs(transform.position.x - initialPosition.x);
            float step = _movementCurve.Evaluate(distanceMoved);
            transform.position = Vector3.MoveTowards(transform.position, positionToMove, step * Time.deltaTime * _movementSpeed);

            yield return new WaitForEndOfFrame();
        }
        _isMoving = false;
    }
}
