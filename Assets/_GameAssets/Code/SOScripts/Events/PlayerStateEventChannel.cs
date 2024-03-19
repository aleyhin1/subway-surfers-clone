using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObject/Events/PlayerState Event")]
public class PlayerStateEventChannelSO : ScriptableObject
{
    public event UnityAction<PlayerState> OnEventRaised;

    public void RaiseEvent(PlayerState state)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(state);
        }
    }
}
