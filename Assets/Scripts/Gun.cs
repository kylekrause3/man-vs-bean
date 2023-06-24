using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    /* Raycasting */
    RaycastHit hitInfo;

    public Player player;
    public Transform cam;

    public float damage;
    float damagemodifier = 1f;
    public float range;
    public float fireRate = 5f; //bullets per second
    public float impactforce;

    public ParticleSystem muzzleFlash;
    //public GameObject impactEffect;

    private float nextFireTime = 0f;
    private bool mouseInUse = false;
    
    void Start()
    {
        if (range <= 0f)
            range = Mathf.Infinity;
        if (fireRate <= 0f)
            fireRate = 5f;
        if (impactforce <= 0f)
            impactforce = 25f;
    }

    void Update()
    {
        gameObject.transform.rotation = cam.transform.rotation;
        //this logic is so that it's like input.getbuttondown (semi-auto fire). To make it automatic, you need to get rid of the stuff using mouseInUse
        if (Input.GetAxisRaw("Fire1") != 0)
        {
            if (mouseInUse == false)
            {
                shoot(player.getCamTransform());
                mouseInUse = true;
            }
        }
        if (Input.GetAxisRaw("Fire1") == 0)
        {
            mouseInUse = false;
        }
    }


    public void shoot(Transform origin)
    {
        if (nextFireTime <= Time.time)
        {
            muzzleFlash.Play();

            origin.position += origin.transform.forward * VirtualCamera.getCamDistance();
            nextFireTime = Time.time + (1f / fireRate);
            if (Physics.Raycast(origin.position, origin.transform.forward, out hitInfo, range))
            {
                Enemy enemyHit = hitInfo.transform.GetComponent<Enemy>();
                Player playerHit = hitInfo.transform.GetComponent<Player>();
                Debug.DrawLine(origin.position, hitInfo.point, Color.red, .5f);
                enemyHit?.TakeDamage(damage * damagemodifier);
                playerHit?.TakeDamage(damage * damagemodifier);


                hitInfo.rigidbody?.AddForce(-hitInfo.normal * impactforce);

                //GameObject impact = Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                //Destroy(impact, 1f);
            }
        }
    }

    public void setDamageMod(float x)
    {
        damagemodifier = x;
    }

    public float getDamageMod()
    {
        return damagemodifier;
    }
}