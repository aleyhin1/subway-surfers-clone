using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameState state;
    [SerializeField] private FloatVariableSO _endlessMovementSpeed;
    [SerializeField] private float _defaultEndlessMovementSpeed;

    private void Start()
    {
        UpdateGameState(state);
    }

    public void UpdateGameState(GameState nextState)
    {
        switch (nextState)
        {
            case GameState.Running:
                _endlessMovementSpeed.Value = _defaultEndlessMovementSpeed;
                break;
            case GameState.Over:
                _endlessMovementSpeed.Value = 0;
                break;
        }
    }
}
