using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorldSpawner : MonoBehaviour
{
    public Item item; //to use this in editor(change fields) make Item.cs serializable

    void Start()
    {
        ItemWorld.SpawnItemWorld(transform.position, item);
        Destroy(this.gameObject);
    }

    void Update()
    {

    }
}
