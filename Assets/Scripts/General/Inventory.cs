using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory
{
    // Dict usando el tipo y la cantidad de items de ese tipo
    Dictionary<ItemTypes, int> items = new Dictionary<ItemTypes, int>();
    // Dict para saber los maximos que se puede recojer de cada item
    Dictionary<ItemTypes, int> itemMaxes = new Dictionary<ItemTypes, int>();

    ItemTypes currentSelectedItem = ItemTypes.Empty;

    public ItemTypes SelectedItem { get => currentSelectedItem; set => currentSelectedItem = value; }
    private InventoryHUD hud;

    public Inventory(bool hasBomb, bool hasFood, bool hasKey, InventoryHUD hud)
    {
        if (hasBomb)
        {
            items.Add(ItemTypes.Bomb, 1);
        }
        else
        {
            items.Add(ItemTypes.Bomb, 0);
        }


        if (hasFood)
        {
            items.Add(ItemTypes.Food, 1);
        }

        if (hasKey)
        {
            items.Add(ItemTypes.Key, 1);
        }

        this.hud = hud;
    }





    ///// <summary>
    ///// Genera un nuevo inventario para el nivel.
    ///// </summary>
    ///// <param name="bomb">Si tiene una bomba al iniciar el juego.</param>
    ///// <param name="food">Si tiene una comida al iniciar.</param>
    ///// <param name="key">Si tiene la llave al iniciar.</param>
    ///// <param name="inputClass"></param>
    //public Inventory(bool bomb, bool food, bool key, InventoryHUD hud)
    //{
    //    items.Add(ItemTypes.Bomb, bomb ? 1 : 0);
    //    items.Add(ItemTypes.Food, food ? 1 : 0);
    //    items.Add(ItemTypes.Key, key ? 1 : 0);

    //    itemMaxes.Add(ItemTypes.Bomb, 1);
    //    itemMaxes.Add(ItemTypes.Food, 1);
    //    itemMaxes.Add(ItemTypes.Key, 1);

    //    currentSelectedItem = bomb ? ItemTypes.Bomb : ItemTypes.Food;
    //    currentSelectedItem = food ? ItemTypes.Food : ItemTypes.Key;
    //    currentSelectedItem = key ? ItemTypes.Key : ItemTypes.Empty;

    //    this.hud = hud;

    //    //this.hud.UpdateUI(currentSelectedItem);
    //}

    public void RemoveItem(ItemTypes type)
    {
        if (items[type] - 1 >= 0)
        {
            items[type] -= 1;
        }
        else
        {
            items[type] = 0;
        }
        UpdateUI();
    }

    public bool AddItem(ItemTypes type)
    {
        // si entra uno mas de este item, agregarlo
        if (items[type] + 1 <= itemMaxes[type])
        {
            items[type] += 1;
            currentSelectedItem = type;

            UpdateUI();
            return true;
        }
        else
            return false;
    }
    public void SetItems(ItemTypes type, int count)
    {
        // si entra uno mas de este item, agregarlo
        if (count <= itemMaxes[type])
        {
            items[type] = count;
        }
        else if (count < 0) // si es negativo, resetear a 0
        {
            items[type] = 0;
        }
        else // si se pasa del maximo, usar el maximo
        {
            items[type] = itemMaxes[type];
        }
    }

    public void UpdateUI()
    {
        hud.UpdateUI(this.currentSelectedItem);
    }

    public bool HasItem(ItemTypes selectedItem)
    {
        return items[selectedItem] > 0;
    }

    internal void SwitchActiveItem()
    {
        if ((int)SelectedItem + 1 < Enum.GetNames(typeof(ItemTypes)).Length)
        {
            SelectedItem++;
        }
        else
        {
            SelectedItem = 0;
        }
    }
}

