using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public ObjectPool Pool {  get; set; }
    public Stack<PooledObject> Stack { get; set; }

    public void Release()
    {
        Pool.ReturnToPool(this);
    }
}
