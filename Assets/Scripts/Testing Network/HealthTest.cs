using UnityEngine;

public class HealthTest : MonoBehaviour
{
    [SerializeField] private int currentHealth = 100; // Initial health value
    public HealthBar healthBar;

    private void Update()
    {
        healthBar.SetHealth(currentHealth);
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public void setCurrentHealth(int value)
    {
        currentHealth = value;
    }

    public void removeHealth(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Health removed. Current Health: " + currentHealth);
    }

    public void addHealth(int amount)
    {
        currentHealth += amount;
        Debug.Log("Health added. Current Health: " + currentHealth);
    }
}
