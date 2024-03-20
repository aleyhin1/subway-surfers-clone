using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObject/Events/GameState Event")]
public class GameStateEventChannelSO : ScriptableObject
{
    public event UnityAction<GameState> OnEventRaised;

    public void RaiseEvent(GameState state)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(state);
        }
    }
}
