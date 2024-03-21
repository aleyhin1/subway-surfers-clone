using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private IntVariableSO _score;
    [SerializeField] private IntVariableSO _highestScore;
    [SerializeField] private FloatVariableSO _endlessMovementSpeed;
    private float _elapsedTime = 0;
    private bool _canCount;

    private void Update()
    {
        CalculateScore();
    }

    public void SetCount(bool value)
    {
        _canCount = value;
    }

    private void CalculateScore()
    {
        if (!_canCount) return;

        int currentScore = (int)(_elapsedTime * _endlessMovementSpeed.Value);
        _score.Value = currentScore;

        _elapsedTime += Time.deltaTime;
    }

    public void SetHighestScore()
    {
        _highestScore.Value = Mathf.Max(_score.Value, _highestScore.Value);
    }
}
