using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Global Variables/Bool Variable")]
public class BoolVariableSO : ScriptableObject
{
    private void OnEnable()
    {
        Value = false;
    }

    public bool Value;
}
