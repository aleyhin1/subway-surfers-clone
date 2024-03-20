using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreCountUI;
    [SerializeField] private IntVariableSO _score;

    private void LateUpdate()
    {
        UpdateScoreCount();
    }

    private void UpdateScoreCount()
    {
        _scoreCountUI.text = _score.Value.ToString();
    }
}
