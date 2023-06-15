using Photon.Pun;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPunObservable
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
    //[SerializeField] private UI_Inventory uiInventory;
    public Inventory inventory;

    //[Header("Attacking")]
    //public Gun gun;
    /*
    public Transform gunTransform;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;*/

    //Gun gun; only used for perk damage modification

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Start()
    {

        healthBar.SetMaxHealth(maxHealth);

        inventory = new Inventory(UseItem, this);
        //uiInventory.SetInventory(inventory);
        //uiInventory.SetPlayer(this);

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    { 

        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(5f);
        }

        //regen
        if (currentHealth < maxHealth)
        {
            if ((int)(Time.time % 60) >= lastTimeHitSecs + regenerationTime)
            {
                Heal(regenerationAmount * Time.deltaTime);
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
            Death();
        }

    }

    public void Death()
    {
        PhotonNetwork.Destroy(gameObject);
    }

    public void Heal(float amt)
    {
        currentHealth += amt;
        healthBar.SetHealth(currentHealth);

        if (currentHealth >= maxHealth) {
            currentHealth = maxHealth;
        }
            
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) {
            // Send the health data to other clients
            stream.SendNext(currentHealth);
        } else if (stream.IsReading) {
            // Receive the health data from the owner client
            currentHealth = (float)stream.ReceiveNext();
        }
    }

    public void UseItem(Item item)
    {
        switch (item.itemType)
        {
            //ENTER USE STUFF HERE, LIKE ADDING BUFFS
            //EX:
            case Item.Type.SpeedBoost:
                //movement.addspeed or whatever to apply the buff
                inventory.RemoveItem(new Item { itemType = Item.Type.SpeedBoost, amt = 1 });
                break;
        }
    }

    public void AddBuff(Item item)
    {
        switch (item.itemType)
        {
            case Item.Type.JumpBoost: //jumpheight += item.intensity; break; needs fix
                break;
            case Item.Type.SpeedBoost: //speed += item.intensity; break; needs fix
                break;
            case Item.Type.RegenBoost:
                regenerationTime -= item.intensity;
                regenerationAmount *= 1.5f;
                break;
            case Item.Type.AttackBoost:
                //gun.setDamageMod(gun.getDamageMod() + item.intensity);
                break;
            default: break;
        }
    }

    public Transform getCamTransform()
    {
        return camtransform;
    }

    private void OnTriggerEnter(Collider col)
    { 
        ItemWorld itemWorld = col.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            Item item = itemWorld.GetItem();
            inventory.AddItem(item);
            itemWorld.DestroySelf();
        }
    }

}