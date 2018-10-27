using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    public static ItemInfo instance;

    void Awake()
    {
        instance = this;
    }

    public GameObject infoPanelPrefab;

    public void OpenInfoPanel (Item item, Interactable interactable)
    {
        GameObject panel;

        infoPanelPrefab.transform.GetChild(0).GetComponent<Image>().sprite = item.icon;
        infoPanelPrefab.transform.GetChild(1).GetComponent<Text>().text = item.name;
        infoPanelPrefab.transform.GetChild(2).GetComponent<Text>().text = item.description;

        panel = Instantiate(infoPanelPrefab, interactable.transform.position, interactable.transform.rotation);
        //panel.transform.SetParent(GameObject.Find(item.name).transform);
    }
}
