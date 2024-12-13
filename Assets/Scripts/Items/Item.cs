using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// La clase abstracta no se puede instanciar
public abstract class Item : MonoBehaviour
{
    protected int uses;
    

    // Un metodo abstracto
    // es un "titulo" de accion, pero no puede definir cual
    // es su comportamiento.
    public abstract void OnPickUp();
    public abstract void OnDispose();
}

