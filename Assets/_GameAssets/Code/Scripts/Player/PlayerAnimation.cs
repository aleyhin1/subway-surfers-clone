using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public IEnumerator SetAnimationOneShot(string animName, bool value, float time)
    {
        _animator.SetBool(animName, value);

        yield return new WaitForSeconds(time);

        _animator.SetBool(animName, !value);
    }

    public void SetAnimationOneShot(string animName, bool value)
    {
        _animator.SetBool(animName, value);
    }
}
