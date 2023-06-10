using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour 
{
    Inventory inventory;
    Transform itemSlotContainer;
    Transform itemSlotTemplate;
    //Transform itemSlotText;
    private Player player;

    private void Awake()
    {
        itemSlotContainer = transform.Find("Slots");
        itemSlotTemplate = itemSlotContainer.Find("SlotTemplate");
        //itemSlotText = itemSlotTemplate.Find("Text");
    }


    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
    }


    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    void RefreshInventoryItems()
    {
        foreach(Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }


        float x = -4.5f;
        int y = 0;
        float itemSlotCellSize = 50f;
        foreach (Item item in inventory.GetItemList())
        {

            if (Input.GetKeyDown(KeyCode.Z))
            {   //use item skeleton
                inventory.UseItem(item);
            }
            if (Input.GetKeyDown(KeyCode.X))
            {   //drop item skeleton
                Item dupe = new Item { itemType = item.itemType, amt = item.amt};
                inventory.RemoveItem(item);
                ItemWorld.DropItem(player.transform.position, dupe);
            }


            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>(); //put slot in ui
            itemSlotRectTransform.gameObject.SetActive(true);   //Make it visible
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);

            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.getSprite();


            TextMeshProUGUI uiText = itemSlotRectTransform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            //Debug.Log("uitext " + uiText);
            Image textbackground = itemSlotRectTransform.Find("Text Background").GetComponent<Image>();
            //Debug.Log("background " + textbackground);
            if (uiText != null && textbackground != null)
            {
                
                if (item.amt > 1)
                {
                    textbackground.enabled = true;
                    uiText.SetText(item.amt.ToString());
                }
                else
                {
                    textbackground.enabled = false;
                    uiText.SetText("");
                }
            }

            x++;
        }
    }
}
