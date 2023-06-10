using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region vars
    [Header("General Vars")]
    public GameObject playermodel;
    public Transform camtransform;

    [Header("Health")]
    public HealthBar healthBar;
    public float maxHealth;
    public float currenthealth;
    public float regenerationAmount;
    public float regenerationTime;
    float lastTimeHit;
    int lastTimeHitSecs;

    [Header("Inventory")]
    [SerializeField] private UI_Inventory uiInventory;
    public Inventory inventory;


    [Header("Movement")]
    public float jumpheight = 3f;
    public float speed = 6f;
    public float gravity = 10f;
    public CharacterController charcontroller;

    public Transform groundCheck;
    public LayerMask groundMask;
    thirdpersonmovement mvmt;

    [Header("Attacking")]
    public Gun gun;
    /*
    public Transform gunTransform;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;*/

    //Gun gun;



    #endregion
    void Awake()
    {
        currenthealth = maxHealth;
    }

    void Start()
    {

        healthBar.SetMaxHealth(maxHealth);

        inventory = new Inventory(UseItem, this);
        uiInventory.SetInventory(inventory);
        uiInventory.SetPlayer(this);

        mvmt = new thirdpersonmovement(this.transform, speed, gravity, charcontroller, camtransform, groundCheck, groundMask);

        //gun = new Gun(muzzleFlash, impactEffect, 10f);

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        mvmt.Movement(speed, jumpheight);

        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(5f);
        }
        /*if (Input.GetAxisRaw("Fire1") != 0 || Input.GetKeyDown(KeyCode.L))
        {
            gun.shoot(camtransform, damagemod);
        }*/


        //regen
        if (currenthealth < maxHealth)
        {
            if ((int)(Time.time % 60) >= lastTimeHitSecs + regenerationTime)
            {
                Heal(regenerationAmount * Time.deltaTime);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currenthealth -= damage;
        healthBar.SetHealth(currenthealth);
        lastTimeHit = Time.time;
        lastTimeHitSecs = (int)(Time.time % 60);

    }

    public void Heal(float amt)
    {
        currenthealth += amt;
        healthBar.SetHealth(currenthealth);

        if (currenthealth >= maxHealth)
            currenthealth = maxHealth;
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
            case Item.Type.JumpBoost: jumpheight += item.intensity; break;
            case Item.Type.SpeedBoost: speed += item.intensity; break;
            case Item.Type.RegenBoost:
                regenerationTime -= item.intensity;
                regenerationAmount *= 1.5f;
                break;
            case Item.Type.AttackBoost:
                gun.setDamageMod(gun.getDamageMod() + item.intensity);
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