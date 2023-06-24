using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerNetworkManager : MonoBehaviourPunCallbacks
{
    public HealthSystem playerHealth;
    public thirdpersonmovement playerMovement;
    public CamMovement playerRotation;
    public Gun gun;
    public GameObject camera;
    public GameObject HUD;

    private void Awake()
    {
        if (!photonView.IsMine) {
            camera.SetActive(false);
            HUD.SetActive(false);
            playerMovement.enabled = false;
            playerRotation.enabled = false;
        }
        Vector3Serialization.RegisterVector3();
    }

    private void Update()
    {
        if (!photonView.IsMine) {
            return;
        }



        if (Input.GetButtonDown("Fire1")) {
            gun.shootRPC(camera.transform.position, camera.transform.forward);
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