using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SkinnedMeshRenderer _renderer;
    [SerializeField] private Color _damageColor;
    [SerializeField] private float _blinkTime;

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

    public IEnumerator HitAnimation(float time)
    {
        Color initialColor = _renderer.material.color;
        float timeElapsed = 0;

        while (timeElapsed < time)
        {
            _renderer.material.color = _damageColor;

            yield return new WaitForSeconds(_blinkTime * .5f);

            _renderer.material.color = initialColor;

            yield return new WaitForSeconds(_blinkTime * .5f);

            timeElapsed += _blinkTime;
        }
    }
}
