using UnityEngine;

public class PlayerHappiness : MonoBehaviour
{
    public static PlayerHappiness Instance;

    public int maxHappy = 100;
    public int currentHappy;

    void Awake()
    {
        Instance = this;
        currentHappy = 0;
    }

    public void Heal(int amount)
    {
        currentHappy = Mathf.Min(currentHappy + amount, maxHappy);
        Debug.Log("ȸ����! ���� ü��: " + currentHappy);
    }

    public void Damage(int amount)
    {
        currentHappy = Mathf.Max(currentHappy - amount, 0);
        Debug.Log("����! ���� ü��: " + currentHappy);
    }
}
