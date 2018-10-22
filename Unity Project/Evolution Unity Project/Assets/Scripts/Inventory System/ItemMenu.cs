using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMenu : MonoBehaviour
{
    bool CursorOnMenu = false;

    void OnMouseEnter() => CursorOnMenu = true;

    void OnMouseExit() => CursorOnMenu = false;

    void Update()
    {
        // Checking if we clicked somewhere else than the menu
        if (Input.GetButtonDown("Fire1") && !CursorOnMenu)
        {
            // Closing the menu
            this.gameObject.SetActive(false);
        }
    }

}
