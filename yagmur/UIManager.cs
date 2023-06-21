using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider healthSlider;
    // Can çubuğu Slider bileşeni

    public void Start()
    {
        // Can çubuğunun değerini başlangıçta ayarla
        healthSlider.value = 1f; // 1f tam can değerine karşılık geliyor, isteğinize göre değiştirebilirsiniz
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        // Can çubuğunun değerini güncelle
        healthSlider.value = currentHealth / maxHealth;
    }
}
