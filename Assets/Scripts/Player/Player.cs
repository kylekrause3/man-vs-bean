using Photon.Pun;

using UnityEngine;

public class Player : MonoBehaviourPunCallbacks
{
    [Header("General Variables")]
    public GameObject playermodel;
    //public Transform camtransform;

    [Header("Inventory")]
    public Inventory inventory;

    void Awake()
    {
    }

    void Start()
    {

        inventory = new Inventory(UseItem, this);

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) {

        }
    }

    public void UseItem(Item item)
    {
        if (!photonView.IsMine) return;

        switch (item.itemType) {
            // Handle item usage and removal
        }
    }

    public void AddBuff(Item item)
    {
        if (!photonView.IsMine) return;

        switch (item.itemType) {
            // Handle adding buffs
        }
    }

    /*
    public Transform getCamTransform()
    {
        return camtransform;
    }
    */

    private void OnTriggerEnter(Collider col)
    {
        if (!photonView.IsMine) return;

        ItemWorld itemWorld = col.GetComponent<ItemWorld>();
        if (itemWorld != null) {
            Item item = itemWorld.GetItem();
            inventory.AddItem(item);
            itemWorld.DestroySelf();
        }
    }
}
