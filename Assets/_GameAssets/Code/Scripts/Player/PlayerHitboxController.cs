using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitboxController : MonoBehaviour
{
    [SerializeField] private BoxCollider _runningHitbox;
    [SerializeField] private BoxCollider _rollHitbox;
    [SerializeField] private PlayerStateEventChannelSO _onPlayerHit;
    [SerializeField] private VoidEventChannelSO _onGoldGained;
    private bool _isHitboxesDeactivated;

    private void Start()
    {
        _runningHitbox.enabled = true;
    }

    public IEnumerator DeactivateHitboxes(float time)
    {
        _isHitboxesDeactivated = true;
        _runningHitbox.enabled = false;
        _rollHitbox.enabled = false;

        yield return new WaitForSeconds(time);

        _isHitboxesDeactivated = false;
        _runningHitbox.enabled = true;
    }

    public IEnumerator ToggleRollHitbox(float time)
    {
        if (_isHitboxesDeactivated) yield break;

        _runningHitbox.enabled = false;
        _rollHitbox.enabled = true;

        yield return new WaitForSeconds(time);

        _rollHitbox.enabled = false;
        _runningHitbox.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gold"))
        {
            _onGoldGained.RaiseEvent();
            PooledObject pooledObject = other.GetComponent<PooledObject>();
            pooledObject.Release();
        }
        else
        {
            _onPlayerHit.RaiseEvent(PlayerState.Hit);
        }
    }
}
