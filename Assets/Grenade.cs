using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject sparks;
    public GameObject smoke;

    private float startTime;
    private bool firstPass = false;
    
    private void Awake()
    {
        startTime = Time.time;

    }

    private void Update()
    {
        Enemy[] enemy = FindObjectsOfType<Enemy>();
        PlayerNetworkManager[] players = FindObjectsOfType<PlayerNetworkManager>();
        float elapsedTime = Time.time - startTime;
        if (elapsedTime >= 4.75f && !firstPass) {
            for(int i = 0; i < enemy.Length; i++) {
                if (Vector3.Distance(transform.position, enemy[i].transform.position) <= 5f) {
                    enemy[i].TakeDamage(50);
                }
            }
            for (int i = 0; i < players.Length; i++) {
                if (Vector3.Distance(transform.position, players[i].transform.position) <= 5f) {
                    players[i].playerHealth.removeHealth(50);
                }
            }
            sparks.SetActive(true);
            smoke.SetActive(true);
            firstPass = true;
        }
    }
}
