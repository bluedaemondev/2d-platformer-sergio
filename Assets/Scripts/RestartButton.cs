using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    UnityEngine.UI.Button btn;
    void Awake()
    {
        btn = GetComponent<UnityEngine.UI.Button>();
        btn.onClick.AddListener(FindObjectOfType<CustomSceneManager>().Reload);
    }
}
