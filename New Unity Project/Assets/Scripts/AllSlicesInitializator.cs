using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSlicesInitializator : MonoBehaviour
{
    private const int slicesCountInCircle = 6;
    [SerializeField] private Transform[] circles;
    [SerializeField] private GameObject slicePrefab;

    public void GenerateSlicesForEditor()
    {
        foreach (Transform circle in circles)
        {
            var sliceRotation = new Vector3(0, 0, 0);
            for (int i = 0; i < slicesCountInCircle; i++)
            {
                var slice = GameObject.Instantiate(slicePrefab, circle, true);

                slice.transform.localPosition = Vector3.zero;//
                slice.transform.eulerAngles = sliceRotation;
                sliceRotation += new Vector3(0, 0, 60);
                slice.SetActive(false);
            }
        }
    }

    public void ClearSlices()
    {
        List<GameObject> slicesToClear = new List<GameObject>();
        foreach (var circle in circles)
        {
            foreach (Transform child in circle.transform)
            {
                slicesToClear.Add(child.gameObject);
            }
        }
        slicesToClear.ForEach(x => x.SetActive(false));
    }
}
