using UnityEngine;

public class ItemPickup : Interactable {

	public Item item;

	public override void Interact ()
	{
		base.Interact();

		Pickup();
	}

	void Pickup ()
	{
		bool wasPickedUp = Inventory.instance.Add(item);

		if (wasPickedUp)
			Destroy(gameObject);
	}

    public override void OpenPanel (Interactable interactable)
    {
        //base.OpenPanel();

        ItemInfo.instance.OpenInfoPanel(item, interactable);
    }

}
