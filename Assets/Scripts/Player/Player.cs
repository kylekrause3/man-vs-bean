using Photon.Pun;

using UnityEngine;

public class Player : MonoBehaviourPunCallbacks, IPunObservable
{
    [Header("General Vars")]
    public GameObject playermodel;
    public Transform camtransform;

    [Header("Health")]
    public HealthBar healthBar;
    public float maxHealth;
    public float currentHealth;
    public float regenerationAmount;
    public float regenerationTime;
    float lastTimeHit;
    int lastTimeHitSecs;

    [Header("Inventory")]
    public Inventory inventory;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);

        inventory = new Inventory(UseItem, this);

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) {
            if (photonView.IsMine) {
                TakeDamage(5f);
            }
        }

        if (currentHealth < maxHealth) {
            if ((int)(Time.time % 60) >= lastTimeHitSecs + regenerationTime) {
                if (photonView.IsMine) {
                    Heal(regenerationAmount * Time.deltaTime);
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (!photonView.IsMine) return;

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        lastTimeHit = Time.time;
        lastTimeHitSecs = (int)(Time.time % 60);

        if (currentHealth <= 0f) {
            Death();
        }
    }

    public void Death()
    {
        if (photonView.IsMine) {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    public void Heal(float amt)
    {
        if (!photonView.IsMine) return;

        currentHealth += amt;
        healthBar.SetHealth(currentHealth);

        if (currentHealth >= maxHealth) {
            currentHealth = maxHealth;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) {
            stream.SendNext(currentHealth);
        } else if (stream.IsReading) {
            currentHealth = (float)stream.ReceiveNext();
            healthBar.SetHealth(currentHealth);
        }
    }

    public void UseItem(Item item)
    {
        if (!photonView.IsMine) return;

        switch (item.itemType) {
            // Handle item usage and removal
        }
    }

    public void AddBuff(Item item)
    {
        if (!photonView.IsMine) return;

        switch (item.itemType) {
            // Handle adding buffs
        }
    }

    public Transform getCamTransform()
    {
        return camtransform;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!photonView.IsMine) return;

        ItemWorld itemWorld = col.GetComponent<ItemWorld>();
        if (itemWorld != null) {
            Item item = itemWorld.GetItem();
            inventory.AddItem(item);
            itemWorld.DestroySelf();
        }
    }
}
