using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameState state;
    [SerializeField] private FloatVariableSO _endlessMovementSpeed;

    public void UpdateGameState(GameState nextState)
    {
        switch (state)
        {
            case GameState.Running:
                break;
            case GameState.Over:
                _endlessMovementSpeed.Value = 0;
                break;
        }
    }
}
