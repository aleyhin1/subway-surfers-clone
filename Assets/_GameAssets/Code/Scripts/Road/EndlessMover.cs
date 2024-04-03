using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessMover : MonoBehaviour
{
    [SerializeField] private FloatVariableSO _movementSpeed;

    protected virtual void Update()
    {
        transform.Translate(Vector3.back * _movementSpeed.Value * Time.deltaTime);
    }
}
