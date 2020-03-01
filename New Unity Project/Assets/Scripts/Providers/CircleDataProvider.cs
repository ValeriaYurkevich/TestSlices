using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Providers
{
    public class CircleDataProvider
    {
        public enum Slices
        {
            All,
            Active,
            Inactive
        }

        public List<Transform> GetAvailableSlicesOfCircles(IEnumerable<Transform> circles)
        {
            var result = new List<Transform>();

            foreach (var circle in circles)
                result.AddRange(GetSlicesOfCircle(circle, Slices.Active));

            return result;
        }

        public List<Transform> GetSlicesOfCircle(Transform circle, Slices slices)
        {
            var result = new List<Transform>();

            foreach (Transform child in circle)
            {
                bool needToAdd = true;

                switch (slices)
                {
                    case Slices.All:
                        break;
                    case Slices.Active:
                        needToAdd = child.gameObject.activeSelf;
                        break;
                    case Slices.Inactive:
                        needToAdd = !child.gameObject.activeSelf;
                        break;
                }

                if (needToAdd)
                    result.Add(child);
            }

            return result;
        }
        
        public List<Transform> GetMatchingSlices(List<Transform> slicesOfCurrentCircle, List<Transform> activeSlicesOfCenterCircle)
        {
            return slicesOfCurrentCircle
                         .Where(slice => !slice.gameObject.activeSelf && activeSlicesOfCenterCircle.Any(centralSlice => slice.rotation == centralSlice.rotation))
                         .ToList();
        }
    }
}
