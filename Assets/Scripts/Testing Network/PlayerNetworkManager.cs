using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerNetworkManager : MonoBehaviourPunCallbacks
{
    public HealthSystem playerHealth;
    public thirdpersonmovement playerMovement;
    public CamMovement camMovement;
    public Gun gun;
    public GameObject virtualCamera;
    public GameObject normalCamera;
    public GameObject HUD;

    private Transform worldSpawn;

    private void Awake()
    {
        if (!photonView.IsMine) {
            virtualCamera.SetActive(false);
            HUD.SetActive(false);
            playerMovement.enabled = false;
            camMovement.enabled = false;
            gun.enabled = false;
            this.tag = "Enemy";
        }
        else
        {
            this.tag = "Player";
        }
        Vector3Serialization.RegisterVector3();
    }

    private void Update()
    {
        if (!photonView.IsMine) {
            return;
        }



        if (Input.GetButtonDown("Fire1")) {
            gun.shootRPC(normalCamera.transform.position, normalCamera.transform.forward);
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            this.Respawn();
        }

        if (Input.GetKeyDown(KeyCode.J)) {
            playerHealth.removeHealthRPC(10); // Remove 10 from current health
        }

        if (Input.GetKeyDown(KeyCode.K)) {
            playerHealth.addHealthRPC(20); // Add 20 to current health
        }
    }

    public void Despawn()
    {
        photonView.RPC("DespawnRPC", RpcTarget.All);
    }

    public void Respawn()
    {
        photonView.RPC("RespawnRPC", RpcTarget.All);
    }

    [PunRPC]
    private void DespawnRPC()
    {
        this.Deactivate();
    }

    [PunRPC]
    private void RespawnRPC()
    {
        this.Despawn();
        this.virtualCamera.SetActive(false);
        this.normalCamera.SetActive(false);
        this.Activate();

        this.playerMovement.SetPosition(worldSpawn.position);

        this.playerHealth.setHealthRPC(playerHealth.getMaxHealth());

        this.virtualCamera.SetActive(true);
        this.normalCamera.SetActive(true);
    }

    private void Deactivate()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        HUD.SetActive(false);
        playerMovement.enabled = false;
        camMovement.enabled = false;
        gun.enabled = false;

    }

    private void Activate()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        HUD.SetActive(true);
        playerMovement.enabled = true;
        camMovement.enabled = true;
        gun.enabled = true;
    }

    public void setWorldSpawn(Transform position)
    {
        this.worldSpawn = position;
    }
}