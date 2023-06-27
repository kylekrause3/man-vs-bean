using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerNetworkManager : MonoBehaviourPunCallbacks
{
    public HealthSystem playerHealth;
    public thirdpersonmovement playerMovement;
    public CamMovement playerRotation;
    public Gun gun;
    public GameObject virtualCamera;
    public GameObject normalCamera;
    public GameObject HUD;

    private void Awake()
    {
        if (!photonView.IsMine) {
            virtualCamera.SetActive(false);
            HUD.SetActive(false);
            playerMovement.enabled = false;
            playerRotation.enabled = false;
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

        if (Input.GetButtonDown("Fire2")) {
            playerHealth.onRespawnRPC();
        }

        if (Input.GetKeyDown(KeyCode.J)) {
            playerHealth.removeHealthRPC(10); // Remove 10 from current health
        }

        if (Input.GetKeyDown(KeyCode.K)) {
            playerHealth.addHealthRPC(20); // Add 20 to current health
        }
    }
}