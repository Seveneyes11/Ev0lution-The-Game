using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

	public Image icon;
	public Button removeButton;
    public GameObject itemMenu;

	Item item;

	public void AddItem (Item newItem)
	{
		item = newItem;

		icon.sprite = item.icon;
		icon.enabled = true;

		//removeButton.interactable = true;
	}

	public void ClearSlot ()
	{
		item = null;

		icon.sprite = null;
		icon.enabled = false;

		//removeButton.interactable = false;
	}

	public void OnRemoveButton ()
	{
        itemMenu.SetActive(false);
		Inventory.instance.Remove(item);
	}

    public void OnSlotClick ()
    {
        // Checking if there's an icon in the slot. This is the kinda cheat way, but whatever ;)
        if (icon.enabled)
        {
            itemMenu.SetActive(!itemMenu.activeSelf);
        }
    }

	public void UseItem ()
	{
		if (item != null)
		{
			item.Use();
		}
	}

}
