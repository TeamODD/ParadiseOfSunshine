using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHappiness : MonoBehaviour
{
    public static PlayerHappiness Instance;
    public GameObject player;
    SpriteRenderer playerSprite;

    public float effectTime = 0.5f;
    public int maxHappy = 100;
    public int currentHappy;
    public Slider happySlider;

    void Awake()
    {
        playerSprite = player.gameObject.GetComponent<SpriteRenderer>();
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
        StartCoroutine(purpleEffect());
    }
    private IEnumerator purpleEffect()
    {
        playerSprite.color = new Color(0.6f, 0f, 0.7f); // 보라색
        yield return new WaitForSeconds(effectTime);          // 0.3초 동안 유지
        playerSprite.color = new Color(1f, 1f, 1f);
    }
}
