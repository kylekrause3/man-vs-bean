using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using System.Collections.Generic;

public class PlayerNetworkManager : MonoBehaviourPunCallbacks
{
    public HealthSystem playerHealth;
    public ThirdPersonMovement playerMovement;
    public CamMovement camMovement;
    public Gun gun;
    public GameObject virtualCamera;
    public GameObject mainCamera;
    public GameObject HUD;
    public CapsuleCollider playerCollider;
    public CharacterController characterController;
    public ProjectileArc projectile;

    private Vector3 worldSpawn;

    private void Awake()
    {
        if (!photonView.IsMine) {
            mainCamera.SetActive(false);
            virtualCamera.SetActive(false);
            HUD.SetActive(false);
            playerMovement.enabled = false;
            camMovement.enabled = false;
            projectile.enabled = false;
            this.tag = "Enemy";
        } else {
            this.tag = "Player";
        }
    }

    private void Update()
    {
        if (!photonView.IsMine) {
            return;
        }
        if (Input.GetButtonDown("Fire1")) {
            gun.shootRPC(mainCamera.transform.position, mainCamera.transform.forward);
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            this.Activate();

            this.virtualCamera.SetActive(false);
            this.mainCamera.SetActive(false);

            this.RespawnRPC();

            this.virtualCamera.SetActive(true);
            this.mainCamera.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.G)) {
            projectile.throwObjectRPC(camMovement.transform.position, camMovement.transform.rotation, camMovement.transform.forward);
        }

        if (Input.GetKeyDown(KeyCode.J)) {
            playerHealth.removeHealthRPC(10); // Remove 10 from current health
        }

        if (Input.GetKeyDown(KeyCode.K)) {
            playerHealth.addHealthRPC(20); // Add 20 to current health
        }
    }

    public void Deactivate()
    {
        HUD.SetActive(false);
        playerMovement.enabled = false;
        camMovement.enabled = false;
        gun.enabled = false;

        //PhotonObject.photonViewInstance.RPC("EnableScript", RpcTarget.All, "CapsuleCollider", false);
        //PhotonObject.photonViewInstance.RPC("EnableScript", RpcTarget.All, "CharacterController", false);
    }

    public void Activate()
    {
        
        HUD.SetActive(true);
        playerMovement.enabled = true;
        camMovement.enabled = true;
        gun.enabled = true;

        //PhotonObject.photonViewInstance.RPC("EnableScript", RpcTarget.All, "CapsuleCollider", true);
        //PhotonObject.photonViewInstance.RPC("EnableScript", RpcTarget.All, "CharacterController", true);
        //this.playerHealth.setHealthRPC(playerHealth.getMaxHealth());
    }

    [PunRPC]
    private void Despawn()
    {
        playerCollider.enabled = false;
        characterController.enabled = false;
        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
            //PhotonObject.photonViewInstance.RPC("SetGameObjectActive", RpcTarget.All, child.gameObject.name, false);
        }
    }

    public void DespawnRPC()
    {
        photonView.RPC("Despawn", RpcTarget.All);
    }

    [PunRPC]
    private void Respawn()
    {
        playerCollider.enabled = true;
        characterController.enabled = true;
        foreach (Transform child in transform) {
            child.gameObject.SetActive(true);
        }
        this.playerMovement.SetPosition(worldSpawn);
        this.playerHealth.setHealthRPC(playerHealth.getMaxHealth());
    }

    public void RespawnRPC()
    {
        photonView.RPC("Respawn", RpcTarget.All);
    }

    [PunRPC]
    private void setWorldSpawn(Vector3 position)
    {
        this.worldSpawn = position;
    }

    public void setWorldSpawnRPC(Vector3 position)
    {
        photonView.RPC("setWorldSpawn", RpcTarget.All, position);
    }
}