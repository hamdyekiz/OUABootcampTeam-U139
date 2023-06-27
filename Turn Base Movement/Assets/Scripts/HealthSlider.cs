using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void UpdateSlider(float healthValue)
    {
        float healthPercentage = healthValue / 100f;
        slider.value = healthPercentage;
    }
}
