using Photon.Pun;

using UnityEngine;

public class PhotonObjectEnemy : MonoBehaviour, IPunObservable
{
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) {
            // Send the health data to other clients
            stream.SendNext(enemy.currenthealth);
        } else if (stream.IsReading) {
            // Receive the health data from the owner client
            float newHealth = (float)stream.ReceiveNext();

            // Update the health value on other clients
            enemy.currenthealth = newHealth;

            // Destroy the object if health falls below zero
            if (newHealth <= 0f) {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
