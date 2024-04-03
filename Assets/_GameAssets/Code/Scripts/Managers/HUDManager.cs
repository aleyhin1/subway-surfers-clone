using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreCountUI;
    [SerializeField] private TextMeshProUGUI _newRecordText;
    [SerializeField] private IntVariableSO _score;
    [SerializeField] private IntVariableSO _goldScore;
    [SerializeField] private FloatVariableSO _goldGainDelay;
    private bool _canUpdateScoreCount = true;

    private void LateUpdate()
    {
        UpdateScoreCount();
    }

    public void StartAddGoldScore()
    {
        StartCoroutine(AddGoldScore(_goldGainDelay.Value));
    }

    private IEnumerator AddGoldScore(float time)
    {
        _canUpdateScoreCount = false;
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            _scoreCountUI.text = _score.Value.ToString() + " + " + _goldScore.Value.ToString();

            yield return new WaitForEndOfFrame();

            elapsedTime += Time.deltaTime;
        }

        _canUpdateScoreCount = true;
    }

    private void UpdateScoreCount()
    {
        if (!_canUpdateScoreCount) return;

        _scoreCountUI.text = _score.Value.ToString();
    }

    public void OpenNewRecordText()
    {
        StartCoroutine(OneShotText(_newRecordText, 10));
        StartCoroutine(RainbowText(_newRecordText));
    }

    public void StartRainbowScoreText()
    {
        StartCoroutine(RainbowText(_scoreCountUI));
    }

    private IEnumerator RainbowText(TextMeshProUGUI text)
    {
        float h = 0;

        while (true)
        {
            h = (h + .01f) % 1;

            Color newColor = Color.HSVToRGB(h, 1f, 1f);
            text.color = newColor;

            yield return new WaitForSeconds(.1f);
        }
    }

    private IEnumerator OneShotText(TextMeshProUGUI text, float seconds)
    {
        text.gameObject.SetActive(true);

        yield return new WaitForSeconds(seconds);

        text.gameObject.SetActive(false);
    }

}
