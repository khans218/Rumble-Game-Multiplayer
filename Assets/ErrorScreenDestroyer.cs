using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorScreenDestroyer : MonoBehaviour {

    public GameObject Canvas;

    // Use this for initialization
    private void Awake()
    {
        DontDestroyOnLoad(Canvas);
    }

    public void DestroyIt()
    {
        MainMenuController menu = GameObject.FindObjectOfType<MainMenuController>();
        if (menu == null) return;
        menu.Canvas.SetActive(true);
        Destroy(Canvas);
    }

}
