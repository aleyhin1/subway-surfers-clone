using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float _movementDistance;
    private float _leftPositionX;
    private float _rightPositionX;

    private void Start()
    {
        _leftPositionX = -_movementDistance;
        _rightPositionX = _movementDistance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > _leftPositionX)
        {
            Move(_leftPositionX);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < _rightPositionX)
        {
            Move(_rightPositionX);
        }
    }

    private void Move(float distance)
    {
        float positionToMoveX = transform.position.x + distance;
        Vector3 positionToMove = new Vector3(positionToMoveX, 0, 0);

        transform.position = positionToMove;
    }
}
