using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<PooledObject> ObjectsToPool;
    [SerializeField] private uint _poolSizePerObject;
    private Dictionary<PooledObject, Stack<PooledObject>> _objectStackPairs =
        new Dictionary<PooledObject, Stack<PooledObject>>();

    private void Start()
    {
        SetupPool();
    }

    public PooledObject GetPooledObject(PooledObject objToGet)
    {
        Stack<PooledObject> stack = _objectStackPairs.GetValueOrDefault(objToGet);

        if (stack.Count == 0)
        {
            PooledObject newInstance = Instantiate(objToGet, transform);
            newInstance.Pool = this;
            newInstance.Stack = stack;
            _objectStackPairs.Add(newInstance, stack);
            Debug.Log("I'm adding a new object to the pool");
            return newInstance;
        }

        PooledObject nextInstance = stack.Pop();
        nextInstance.gameObject.SetActive(true);
        return nextInstance;
    }

    public void ReturnToPool(PooledObject pooledObject)
    {
        pooledObject.Stack.Push(pooledObject);
        pooledObject.gameObject.SetActive(false);
    }

    private void SetupPool()
    {
        foreach (PooledObject objectToPool in ObjectsToPool)
        {
            Stack<PooledObject> stack = new Stack<PooledObject>();
            PooledObject instance = null;

            for (int i = 0; i < _poolSizePerObject; i++)
            {
                instance = Instantiate(objectToPool, transform);
                instance.Pool = this;
                instance.Stack = stack;
                instance.gameObject.SetActive(false);
                stack.Push(instance);
            }

            _objectStackPairs.Add(objectToPool, stack);
        }
    }
}
