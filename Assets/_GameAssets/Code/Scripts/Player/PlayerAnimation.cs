using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private BoolVariableSO _isGrounded;

    private void OnTriggerEnter(Collider other)
    {
        _isGrounded.Value = true;
        _animator.SetBool("IsGrounded", true);
    }

    private void OnTriggerExit(Collider other)
    {
        _isGrounded.Value = false;
        _animator.SetBool("IsGrounded", false);
    }

    public IEnumerator SetAnimationOneShot(string animName, bool value, float time)
    {
        _animator.SetBool(animName, value);

        yield return new WaitForSeconds(time);

        _animator.SetBool(animName, !value);
    }
}
