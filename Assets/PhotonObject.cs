using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonObject : MonoBehaviour, IPunObservable
{
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) {
            // Send the health data to other clients
            stream.SendNext(player.currentHealth);
        } else if (stream.IsReading) {
            // Receive the health data from the owner client
            float newHealth = (float)stream.ReceiveNext();
            player.currentHealth = newHealth;
        }
    }
}
