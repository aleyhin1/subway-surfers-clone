using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameState state;
    [SerializeField] private FloatVariableSO _gameSpeed;
    [SerializeField] private FloatVariableSO _endlessMovementSpeed;
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
                _gameSpeed.Value = 1;
                StartCoroutine(_speedUpCoroutine = SpeedUpGame(_speedUpIncrease, _speedUpTimeInterval));
                _endlessMovementSpeed.Value = 10;
                break;
            case GameState.Over:
                _scoreManager.SetCount(false);
                StopCoroutine(_speedUpCoroutine);
                ChangeGameSpeed(1);
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
            _gameSpeed.Value += increase;
            ChangeGameSpeed(_gameSpeed.Value);
            yield return new WaitForSeconds(timeInterval);
        }
    }

    private void ChangeGameSpeed(float value)
    {
        Time.timeScale = value;
        Time.fixedDeltaTime = 0.02f / value;
    }
}
