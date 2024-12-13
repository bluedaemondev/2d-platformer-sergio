using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory2
{
    // 1.Definir los atributos privados ----> atributo (variable, lista, vector)
    private Dictionary<ItemTypes, int> items = new Dictionary<ItemTypes, int>();
    private Dictionary<ItemTypes, int> itemMaxes = new Dictionary<ItemTypes, int>();
    private ItemTypes currentItem = ItemTypes.Empty;
    
    private InventoryHUD hud;

    /*En la programación orientada a objetos (POO), los atributos son las características
     * o propiedades que representan a los objetos. Los atributos definen las características
     * únicas de cada objeto creado a partir de una clase.*/

    // 2. 


}
