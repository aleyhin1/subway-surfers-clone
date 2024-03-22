using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameState state;
    [SerializeField] private FloatVariableSO _endlessMovementSpeed;
    [SerializeField] private float _defaultEndlessMovementSpeed;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private DatabaseManager _databaseManager;
    [SerializeField] private float _speedUpIncrease;
    [SerializeField] private float _speedUpTimeInterval;
    private IEnumerator _speedUpCoroutine;

    private void Start()
    {
        UpdateGameState(state);
    }

    public void UpdateGameState(GameState nextState)
    {
        switch (nextState)
        {
            case GameState.Running:
                _scoreManager.SetCount(true);
                _endlessMovementSpeed.Value = _defaultEndlessMovementSpeed;
                StartCoroutine(_speedUpCoroutine = SpeedUpGame(_speedUpIncrease, _speedUpTimeInterval));
                break;
            case GameState.Over:
                _scoreManager.SetCount(false);
                StopCoroutine(_speedUpCoroutine);
                _endlessMovementSpeed.Value = 0;
                _scoreManager.SetHighestScore();
                _databaseManager.SaveHighestScore();
                break;
        }
    }

    private IEnumerator SpeedUpGame(float increase, float timeInterval)
    {
        while(true)
        {
            _endlessMovementSpeed.Value += increase;
            yield return new WaitForSeconds(timeInterval);
        }
    }
}
