using UnityEngine;
using Photon.Pun;

public class HealthSystem : MonoBehaviourPunCallbacks
{
    public HealthBar healthBar;
    [SerializeField] public float maxHealth;
    [SerializeField] private float currentHealth;
    public float regenerationAmount;
    public float regenerationTime;
    [SerializeField] float lastTimeHit;
    [SerializeField] int lastTimeHitSecs;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        healthBar.SetHealth(this.currentHealth);
        if (currentHealth < maxHealth) {
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

    [PunRPC]
    private void setCurrentHealth(float value)
    {
        this.currentHealth = value;
    }

    [PunRPC]
    private void removeHealth(float amount)
    {
        this.currentHealth -= amount;
        currentHealth -= amount;
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
    }

    [PunRPC]
    private void onDeath()
    {
        if (photonView.IsMine) {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }

    public void setCurrentHealthRPC(float value)
    {
        photonView.RPC("setCurrentHealth", RpcTarget.All, value);
    }

    public void removeHealthRPC(float amount)
    {
        photonView.RPC("removeHealth", RpcTarget.All, amount);
    }

    public void addHealthRPC(float amount)
    {
        photonView.RPC("addCurrentHealth", RpcTarget.All, amount);
    }

    public void onDeathRPC()
    {
        photonView.RPC("onDeath", RpcTarget.All);
    }
}
