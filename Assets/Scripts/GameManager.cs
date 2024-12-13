using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject hudDeath;
    public GameObject hudWin;

    public void OnDeath()
    {
        Instantiate(hudDeath);
    }
    public void OnVictory()
    {
        Instantiate(hudWin);
        
        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out CharController2D x);
        if (x) x.enabled = false;

        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out PlayerMovement y);
        if (y) y.enabled = false;
    }


}
