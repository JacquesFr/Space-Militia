using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private Image health_stats;

    [SerializeField]
    private Image stamina_stats;

    public void DisplayHealthStats(float healthValue)
    {
        healthValue /= 100f;

        health_stats.fillAmount = healthValue;
    }

    public void DisplayStaminaStats(float staminaValue)
    {
        staminaValue /= 100f;

        stamina_stats.fillAmount = staminaValue;
    }
}
