using UnityEngine;
using UnityEngine.UI;

public class PlayerHappiness : MonoBehaviour
{
    public static PlayerHappiness Instance;

    public int maxHappy = 100;
    public int currentHappy;
    public Slider happySlider;

    void Awake()
    {
        Instance = this;
        currentHappy = 0;
        happySlider.value = currentHappy;
    }

    public void Heal(int amount)
    {
        currentHappy = Mathf.Min(currentHappy + amount, maxHappy);
        happySlider .value = currentHappy;
    }

    public void Damage(int amount)
    {
        currentHappy = Mathf.Max(currentHappy - amount, 0);
        happySlider.value = currentHappy;
    }
}
