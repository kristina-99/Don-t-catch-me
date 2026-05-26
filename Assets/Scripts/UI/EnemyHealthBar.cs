using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider healthBarSlider;
    public EnemyStats enemyStats;

    private void Start()
    {
        enemyStats.OnHealthChanged += UpdateHealthBar;

        healthBarSlider.maxValue = enemyStats.MaxHealthPoints;
        healthBarSlider.value = enemyStats.MaxHealthPoints;
    }

    private void UpdateHealthBar(int healthPoints)
    {
        healthBarSlider.value = healthPoints;
    }
}
