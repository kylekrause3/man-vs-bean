using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting.Antlr3.Runtime;

using UnityEngine;

public class PlayerSetup : MonoBehaviour
{

    public thirdpersonmovement playerMovement;
    public Player playerData;
    public CamMovement playerRotation;
    public GameObject camera;

    public void isLocalPlayer()
    {
        playerMovement.enabled = true;
        playerData.enabled = true;
        playerRotation.enabled = true;
        camera.SetActive(true);
    }
}
