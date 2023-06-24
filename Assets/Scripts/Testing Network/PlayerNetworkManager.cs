using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerNetworkManager : MonoBehaviourPunCallbacks, IPunObservable
{
    private HealthTest healthTest;
    public GameObject camera;

    private void Awake()
    {
        healthTest = this.gameObject.GetComponent<HealthTest>();

        if (!photonView.IsMine) {
            camera.SetActive(false);
        }
    }

    private void Update()
    {
        if (!photonView.IsMine) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.J)) {
            healthTest.removeHealth(10); // Remove 10 from current health
        }

        if (Input.GetKeyDown(KeyCode.K)) {
            healthTest.addHealth(20); // Add 20 to current health
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) {
            stream.SendNext(healthTest.getCurrentHealth());
        } else {
            int receivedHealth = (int)stream.ReceiveNext();
            healthTest.setCurrentHealth(receivedHealth);
        }
    }
}