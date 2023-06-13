using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetCodeManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    public GameObject player;
    public Transform spawnPoint;

    private GameObject playerClient;

    void Start()
    {
        Debug.Log("Connecting to host...");
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to server");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        PhotonNetwork.JoinOrCreateRoom("Test", null, null);

        Debug.Log("Connected to lobby now");
   
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        Debug.Log("Connected to room now");

        playerClient = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
        playerClient.GetComponentInChildren<PlayerSetup>().isLocalPlayer();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
    }
}
