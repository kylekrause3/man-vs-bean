using System.Collections;
using System.Collections.Generic;

using Unity.Netcode;

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject container;

    public void hostGame()
    {
        NetworkManager.Singleton.StartHost();
        container.SetActive(false);
    }

    public void findGame()
    {
        NetworkManager.Singleton.StartClient();
        container.SetActive(false);
    }

    public void Exit()
    {
        // TODO
    }
}
