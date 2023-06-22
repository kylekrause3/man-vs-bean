using Photon.Pun;

using UnityEngine;

public class Enemy : MonoBehaviourPunCallbacks, IPunObservable
{
    [Header("Health")]
    public HealthBar healthBar;
    public float maxHealth;
    public float currentHealth;
    public float regenerationAmount;
    public float regenerationTime;
    float lastTimeHit;
    int lastTimeHitSecs;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) {
            photonView.RPC("TakeDamageRPC", RpcTarget.All, 5f);
        }

        // Regen
        if (currentHealth < maxHealth) {
            if ((int)(Time.time % 60) >= lastTimeHitSecs + regenerationTime) {
                //photonView.RPC("HealRPC", RpcTarget.All, regenerationAmount * Time.deltaTime);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        lastTimeHit = Time.time;
        lastTimeHitSecs = (int)(Time.time % 60);

        if (currentHealth <= 0f) {
            photonView.RPC("DeathRPC", RpcTarget.All);
        }
    }

    [PunRPC]
    private void TakeDamageRPC(float damage)
    {
        TakeDamage(damage);
    }

    [PunRPC]
    private void HealRPC(float amt)
    {
        Heal(amt);
    }

    [PunRPC]
    private void DeathRPC()
    {
        Death();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) {
            // Send the health data to other clients
            stream.SendNext(currentHealth);
        } else if (stream.IsReading) {
            // Receive the health data from the owner client
            currentHealth = (float)stream.ReceiveNext();
            healthBar.SetHealth(currentHealth);
        }
    }

    public void Heal(float amt)
    {
        currentHealth += amt;
        healthBar.SetHealth(currentHealth);

        if (currentHealth >= maxHealth) {
            currentHealth = maxHealth;
        }
    }

    public void Death()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
