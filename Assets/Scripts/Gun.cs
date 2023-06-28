using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gun : MonoBehaviourPunCallbacks
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
    public GameObject impactEffect;

    private float nextFireTime = 0f;

    [PunRPC]
    private void shoot(Vector3 originPositon, Vector3 originForward)
    {
        if (nextFireTime <= Time.time) {
            muzzleFlash.Play();

            originPositon += originForward * VirtualCamera.getCamDistance();
            nextFireTime = Time.time + (1f / fireRate);
            if (Physics.Raycast(originPositon, originForward, out hitInfo, range)) {
                Enemy enemyHit = hitInfo.transform.GetComponent<Enemy>();
                PlayerNetworkManager playerHit = hitInfo.transform.GetComponent<PlayerNetworkManager>();
                Debug.DrawLine(originPositon, hitInfo.point, Color.red, .5f);
                if (photonView.IsMine) {
                    enemyHit?.TakeDamageRPC(damage * damagemodifier);
                    playerHit?.playerHealth.removeHealthRPC(damage * damagemodifier);
                }
                hitInfo.rigidbody?.AddForce(-hitInfo.normal * impactforce);
                GameObject impact = Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                impact.transform.parent = hitInfo.transform; //this can and usually is in the instantiate, but its here to keep scale of hit impacts relative to world not object
                Destroy(impact, 10f);
            }
        }
    }

    public void shootRPC(Vector3 originPositon, Vector3 originForward)
    {
        photonView.RPC("shoot", RpcTarget.All, originPositon, originForward);
    }

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
        if (!photonView.IsMine)
        {
            return;
        }
        gameObject.transform.rotation = cam.transform.rotation;
        //this logic is so that it's like input.getbuttondown (semi-auto fire). To make it automatic, you need to get rid of the stuff using mouseInUse
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