using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private IntVariableSO _score;
    [SerializeField] private IntVariableSO _highestScore;
    [SerializeField] private FloatVariableSO _gameSpeed;
    [SerializeField] private VoidEventChannelSO _onNewRecord;
    [SerializeField] private IntVariableSO _goldScore;
    [SerializeField] private FloatVariableSO _goldGainDelay;
    private bool _canCount;
    private bool _isNewRecordReached = false;

    public void SetCount(bool value)
    {
        _canCount = value;
    }

    public IEnumerator CalculateScore()
    {
        while (_canCount)
        {
            _score.Value += (int)(_gameSpeed.Value);

            if (IsNewRecord() && !_isNewRecordReached)
            {
                _onNewRecord.RaiseEvent();
                _isNewRecordReached = true;
            }

            yield return new WaitForSeconds(1);
        }
        
    }

    public void SetHighestScore()
    {
        _highestScore.Value = Mathf.Max(_score.Value, _highestScore.Value);
    }

    public void GainGoldScore()
    {
        StartCoroutine(GainGoldScoreWithDelay(_goldGainDelay.Value));
    }

    private IEnumerator GainGoldScoreWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        _score.Value += _goldScore.Value;
    }

    private bool IsNewRecord()
    {
        return _score.Value > _highestScore.Value;
    }
}
