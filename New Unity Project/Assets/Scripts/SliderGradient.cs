using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class SliderGradient : MonoBehaviour
{
    [SerializeField] private Image Fill;
    [SerializeField] private Slider Slider;
    private Color MaxHealthColor = Color.green;
    private Color MinHealthColor = Color.red;

    public void Start()
    {
        Slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    public void ValueChangeCheck()
    {
        Fill.color = Color.LerpUnclamped(MinHealthColor, MaxHealthColor, Slider.value / Slider.maxValue);

    }
}
