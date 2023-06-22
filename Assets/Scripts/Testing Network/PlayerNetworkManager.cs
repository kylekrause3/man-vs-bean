using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ObjectNetworkManager : MonoBehaviourPunCallbacks, IPunObservable
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

        if (Input.GetKeyDown(KeyCode.W)) {
            this.transform.position += new Vector3(0f, 0.5f, 0f);
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            this.transform.position -= new Vector3(0f, 0.5f, 0f);
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