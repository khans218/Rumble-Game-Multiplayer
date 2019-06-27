using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DiscoveryController : NetworkDiscovery {

    public MainMenuController menu;

    public void Awake()
    {
        DontDestroyOnLoad(this);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        if (menu == null)
        {
            menu = GameObject.Find("MainMenuController").GetComponent<MainMenuController>();
            if (menu == null) return;
        }
        if (broadcastsReceived.ContainsKey(fromAddress))
        {
            int length = broadcastsReceived[fromAddress].broadcastData.Length / 2;
            data = data.Substring(0, length);
            menu.AddRoom(fromAddress, data);
        }
    }

}
