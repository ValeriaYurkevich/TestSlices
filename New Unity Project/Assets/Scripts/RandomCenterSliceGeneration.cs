using Assets.Scripts.Providers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Slices = Assets.Scripts.Providers.CircleDataProvider.Slices;

public class RandomCenterSliceGeneration : MonoBehaviour
{
    [SerializeField] private Transform centerCircle;
    [SerializeField] private Transform[] circles;
    [SerializeField] private Color[] colors;

    private List<Transform> slicesOfCenterCircle;
    private CircleDataProvider circleDataProvider = new CircleDataProvider();
    private int[] randomCountWeights = new int[] { 1, 1, 1, 1, 2, 2, 3 };

    private void Start()
    {
        slicesOfCenterCircle = circleDataProvider.GetSlicesOfCircle(centerCircle, Slices.All);
        Generate();
    }

    public void Generate()
    {
        var availableSlices = circleDataProvider.GetAvailableSlicesOfCircles(circles);
        var avaliablePositions = availableSlices.GroupBy(slice => slice.parent, slice => slice.transform);

        var randomCircle = circles.OrderBy(x => Random.value).FirstOrDefault();
        var availablePositionsInRandomCircle = circleDataProvider.GetSlicesOfCircle(randomCircle, Slices.Inactive);

        var countToGenerate = randomCountWeights.OrderBy(x => Random.value).FirstOrDefault();

        var randomGeneratedSlices = slicesOfCenterCircle
              .Where(centerSlice => availablePositionsInRandomCircle.Any(slice => slice.rotation == centerSlice.rotation))
              .OrderBy(x => Random.value)
              .Take(countToGenerate);

        var color = GetRandomColor();

        ShowGeneratedSlices(randomGeneratedSlices, color);
    }

    private void ShowGeneratedSlices(IEnumerable<Transform> randomGeneratedSlices, Color color)
    {
        foreach (var slice in randomGeneratedSlices)
        {
            slice.gameObject.SetActive(true);
            slice.GetComponent<SpriteRenderer>().color = color;
        }
    }

    private Color GetRandomColor()
    {
        return colors.OrderBy(x => Random.value).FirstOrDefault();
    }
}
