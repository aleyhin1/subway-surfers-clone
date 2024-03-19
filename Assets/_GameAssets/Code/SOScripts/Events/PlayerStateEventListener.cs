using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStateEventListener : MonoBehaviour
{
    [SerializeField] private PlayerStateEventChannelSO _channel = default;

    public UnityEvent<PlayerState> OnEventRaised;

    private void OnEnable()
    {
        if (_channel != null)
        {
            _channel.OnEventRaised += Respond;
        }
    }

    private void OnDisable()
    {
        if (_channel != null)
        {
            _channel.OnEventRaised -= Respond;
        }
    }

    private void Respond(PlayerState state)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(state);
        }
    }
}
