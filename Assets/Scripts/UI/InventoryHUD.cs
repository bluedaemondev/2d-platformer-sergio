using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Type = ItemTypes;
using Random = UnityEngine.Random;


public class InventoryHUD : MonoBehaviour
{
    private Vector2 highligtedItemPos;
    public Type currentlySelectedItem;
    public RectTransform currentlySelectedItemRect;
    public Dictionary<Type, RectTransform> itemDisplayers = new Dictionary<Type, RectTransform>();
    public RectTransform itemsContainer = new RectTransform();

    private void Awake()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();


        var s = itemsContainer.GetComponentsInChildren<Image>();
        itemDisplayers.Add(0, currentlySelectedItemRect);

        for (int i = 2; i < s.Length; i++)
        {
            itemDisplayers.Add((Type)i - 1, s[i].rectTransform);
        }

        currentlySelectedItem = 0;
        currentlySelectedItemRect = itemDisplayers[0];
        highligtedItemPos = currentlySelectedItemRect.localPosition;
    }

    public void UpdateUI(Type newSelection)
    {
        if (currentlySelectedItem != newSelection)
        {
            // ocultar el item seleccionado en la ui
            currentlySelectedItemRect.localPosition = itemDisplayers[newSelection].localPosition;

            //display del nuevo. le ajustamos la posicion y el tipo a "selected item"
            currentlySelectedItem = newSelection;
            currentlySelectedItemRect = itemDisplayers[newSelection];
            currentlySelectedItemRect.localPosition = highligtedItemPos;
        }

    }
}
