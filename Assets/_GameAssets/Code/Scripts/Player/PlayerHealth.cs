using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private PlayerStateEventChannelSO _onPlayerDeath;

    public void TakeDamage()
    {
        _health--;

        if (_health == 0)
        {
            _onPlayerDeath.RaiseEvent(PlayerState.Death);
        }
    }
}
