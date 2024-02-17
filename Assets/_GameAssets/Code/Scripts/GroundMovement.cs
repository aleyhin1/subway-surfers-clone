using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    private void Update()
    {
        if (transform.position.z < -72)
        {
            transform.position = Vector3.zero;
        }

        transform.Translate(Vector3.back * _movementSpeed * Time.deltaTime);
    }
}
