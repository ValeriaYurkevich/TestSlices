using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RandomCenterSliceGeneration sliceGeneration;
    [SerializeField] private AllSlicesInitializator slicesInitializator;
    [SerializeField] private Slider timeProgressSlider;
    [SerializeField] private GameObject deathWindow;
    [SerializeField] private Text scoreText;

    private int score = 0;
    private const float timeOffset = 0.1f;
    private const float startTime = 5f;
    private const int fullColorCirclePoints = 10;
    private const int simpleCirclePoints = 5;
    private const float minimalTimerValue = 1f;

    private void Start()
    {
        EventHandler.Current.SubscribeToEvent(EventHandler.Event.OnCircleCompletedSuccesfully, () => Score(fullColorCirclePoints));
        EventHandler.Current.SubscribeToEvent(EventHandler.Event.OnCircleCompletedWithDifColors, () => Score(simpleCirclePoints));
        EventHandler.Current.SubscribeToEvent(EventHandler.Event.OnSlicePlaced, OnSlicePlace);
    }

    private void Update()
    {
        if (timeProgressSlider.value == timeProgressSlider.minValue)
            GameOver();

        timeProgressSlider.value -= Time.deltaTime;
    }

    private void OnSlicePlace()
    {
        sliceGeneration.Generate();
        ResetTimer();
    }

    private void ResetTimer()
    {
        if (timeProgressSlider.maxValue > minimalTimerValue)
            timeProgressSlider.maxValue -= timeOffset;

        timeProgressSlider.value = timeProgressSlider.maxValue;
    }

    private void GameOver()
    {
        deathWindow.SetActive(true);
        slicesInitializator.ClearSlices();
    }

    private void StartGame()
    {
        deathWindow.SetActive(false);

        slicesInitializator.ClearSlices();

        timeProgressSlider.maxValue = startTime;
        ResetTimer();

        sliceGeneration.Generate();
    }

    private void Score(int count)
    {
        score += count;
        scoreText.text = score.ToString();
    }
}
