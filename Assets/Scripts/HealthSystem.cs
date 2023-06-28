using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;

public class HealthSystem : MonoBehaviourPunCallbacks
{
    private GameObject localPlayer;
    public Vector3 spawnPoint;
    public HealthBar healthBar;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    public float regenerationAmount;
    public float regenerationTime;
    [SerializeField] float lastTimeHit;
    [SerializeField] int lastTimeHitSecs;
    bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        spawnPoint = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        healthBar.SetHealth(this.currentHealth);
        if (currentHealth < maxHealth && !isDead) {
            if ((int)(Time.time % 60) >= lastTimeHitSecs + regenerationTime) {
                if (photonView.IsMine) {
                    addHealthRPC(regenerationAmount * Time.deltaTime);
                }
            }
        }
    }

    public float getCurrentHealth()
    {
        return this.currentHealth;
    }
    public float getMaxHealth()
    {
        return this.maxHealth;
    }

    [PunRPC]
    private void setCurrentHealth(float value)
    {
        this.currentHealth = value;
    }

    [PunRPC]
    private void removeHealth(float amount)
    {
        this.currentHealth -= amount;
        healthBar.SetHealth(currentHealth);
        lastTimeHit = Time.time;
        lastTimeHitSecs = (int)(Time.time % 60);

        if (currentHealth <= 0f) {
            onDeathRPC();
        }
    }

    [PunRPC]
    private void addHealth(float amount)
    {
        this.currentHealth += amount;

        if(this.currentHealth > maxHealth)
        {
            this.currentHealth = maxHealth;
        }
    }

    [PunRPC]
    private void onDeath()
    {
        if (!photonView.IsMine) {
            return;
        }
        this.GetComponent<PlayerNetworkManager>().Deactivate();
        this.GetComponent<PlayerNetworkManager>().DespawnRPC();
        isDead = true;
    }

    public void setHealthRPC(float value)
    {
        photonView.RPC("setCurrentHealth", RpcTarget.All, value);
    }

    public void removeHealthRPC(float amount)
    {
        photonView.RPC("removeHealth", RpcTarget.All, amount);
    }

    public void addHealthRPC(float amount)
    {
        photonView.RPC("addHealth", RpcTarget.All, amount);
    }

    public void onDeathRPC()
    {
        photonView.RPC("onDeath", RpcTarget.All);
    }
}
