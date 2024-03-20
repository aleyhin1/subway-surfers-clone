using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateEventListener : MonoBehaviour
{
    [SerializeField] private GameStateEventChannelSO _channel = default;

    public UnityEvent<GameState> OnEventRaised;

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

    private void Respond(GameState state)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(state);
        }
    }
}
