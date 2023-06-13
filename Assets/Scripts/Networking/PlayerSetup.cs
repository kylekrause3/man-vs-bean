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
	public GameObject HUD;
	public AudioListener audioListener;

	public void isLocalPlayer()
	{
		this.gameObject.name = "Local Player Client";

		playerMovement.enabled = true;
		playerData.enabled = true;
		playerRotation.enabled = true;
		audioListener.enabled = true;
		HUD.SetActive(true);
		camera.SetActive(true);
	}
}
