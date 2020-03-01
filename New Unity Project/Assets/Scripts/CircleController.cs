using Assets.Scripts.Providers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Slices = Assets.Scripts.Providers.CircleDataProvider.Slices;

public class CircleController : MonoBehaviour
{
    [SerializeField] private Transform centerCircle;

    private CircleDataProvider circleDataProvider = new CircleDataProvider();

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

            var clickedOnThisCircle = rayHit && rayHit.collider && rayHit.collider.gameObject.name == this.name;

            if (clickedOnThisCircle)
            {
                MoveSlicesToThisCircle();
            }
        }
    }

    private void MoveSlicesToThisCircle()
    {
        var slicesOfCurrentCircle = circleDataProvider.GetSlicesOfCircle(this.transform,Slices.All);
        var activeSlicesOfCenterCircle = circleDataProvider.GetSlicesOfCircle(centerCircle, Slices.Active);
        var matchingSlices = circleDataProvider.GetMatchingSlices(slicesOfCurrentCircle, activeSlicesOfCenterCircle);

        if (matchingSlices.Any() && matchingSlices.Count() == activeSlicesOfCenterCircle.Count())
        {
            EnableMatchingSlicesInCurrentCircle(activeSlicesOfCenterCircle, matchingSlices);
            DisableSlicesInCenterCircle(activeSlicesOfCenterCircle);

            if (slicesOfCurrentCircle.All(x => x.gameObject.activeSelf))
            {
                ClearCurrentCircleAndAddPoints(slicesOfCurrentCircle);
            }

            EventHandler.Current.Dispatch(EventHandler.Event.OnSlicePlaced);
        }
    }

    private void ClearCurrentCircleAndAddPoints(List<Transform> slicesOfCurrentCircle)
    {
        var colors = slicesOfCurrentCircle.GroupBy(slice => slice.GetComponent<SpriteRenderer>().color);
        var allColorsAreSame = colors.Count() == 1;

        var @event = allColorsAreSame
            ? EventHandler.Event.OnCircleCompletedSuccesfully
            : EventHandler.Event.OnCircleCompletedWithDifColors;

        EventHandler.Current.Dispatch(@event);

        ClearSlicesInCircle(slicesOfCurrentCircle);
    }

    private void ClearSlicesInCircle(List<Transform> slicesOfCurrentCircle)
    {
        slicesOfCurrentCircle.ForEach(slice => slice.gameObject.SetActive(false));
    }

    private void DisableSlicesInCenterCircle(List<Transform> activeSlicesInCenterCircle)
    {
        activeSlicesInCenterCircle.ForEach(slice => slice.gameObject.SetActive(false));
    }

    private void EnableMatchingSlicesInCurrentCircle(List<Transform> activeSlicesInCenterCircle, IEnumerable<Transform> matchingInactiveSlices)
    {
        foreach (var slice in matchingInactiveSlices)
        {
            var matchingSlice = activeSlicesInCenterCircle.FirstOrDefault(centalSlice => centalSlice.gameObject.activeSelf && centalSlice.transform.rotation == slice.rotation);

            slice.gameObject.SetActive(true);
            slice.GetComponent<SpriteRenderer>().color = matchingSlice.GetComponent<SpriteRenderer>().color;

        }
    }
}
