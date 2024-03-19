using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitboxController : MonoBehaviour
{
    [SerializeField] private BoxCollider _runningHitbox;
    [SerializeField] private BoxCollider _rollHitbox;
    [SerializeField] private PlayerStateEventChannelSO _onPlayerHit;

    private void Start()
    {
        _runningHitbox.enabled = true;
    }

    public IEnumerator ToggleRollHitbox(float time)
    {
        _runningHitbox.enabled = false;
        _rollHitbox.enabled = true;

        yield return new WaitForSeconds(time);

        _rollHitbox.enabled = false;
        _runningHitbox.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        _onPlayerHit.RaiseEvent(PlayerState.Hit);
    }
}
