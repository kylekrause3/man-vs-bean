using Photon.Pun;
using Photon.Realtime;

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    enum Panel
    {
        MainMenu,
        HostGame,
        FindGame,
        Settings
    }

    public GameObject mainMenuPanel;
    public GameObject hostGamePanel;
    public GameObject findGamePanel;
    public GameObject settingsPanel;

    Panel panelState;

    private void Start()
    {
        mainMenuPanel.SetActive(true);
        hostGamePanel.SetActive(false);
        //findGamePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            switch (panelState) {
                case Panel.HostGame:
                    hostGamePanel.SetActive(false);
                    panelState = Panel.MainMenu;
                    break;

                case Panel.FindGame:
                    findGamePanel.SetActive(false);
                    panelState = Panel.MainMenu;
                    break;

                case Panel.Settings:
                    settingsPanel.SetActive(false);
                    panelState = Panel.MainMenu;
                    break;
            }
        }
    }

    public void HostGame()
    {
        PhotonNetwork.JoinLobby();

        string roomName = GenerateRoomName();

        RoomOptions roomOptions = new RoomOptions { MaxPlayers = 4 };
        PhotonNetwork.CreateRoom(roomName, roomOptions, null);

        SceneManager.LoadScene("Game");
        //panelState = Panel.HostGame;
        //hostGamePanel.SetActive(true);
    }

    public void FindGame()
    {
        PhotonNetwork.JoinLobby();
        SceneManager.LoadScene("Game");
        //panelState = Panel.FindGame;
        //findGamePanel.SetActive(true);
    }

    public void OpenSettings()
    {
        panelState = Panel.Settings;
        settingsPanel.SetActive(true);
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }

    private string GenerateRoomName()
    {
        // Generate a unique room name using timestamp and random number
        string timestamp = System.DateTime.Now.ToString("yyyyMMddHHmmss");
        string randomNum = Random.Range(1000, 9999).ToString();
        return "Room_" + timestamp + "_" + randomNum;
    }
}
