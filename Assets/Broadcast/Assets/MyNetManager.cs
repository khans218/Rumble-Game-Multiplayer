using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MyNetManager : NetworkManager
{
    public NetworkDiscovery discovery;
    bool SearchInvoked = false;

    private void Start()
    {
        StartClient();
        StopClient();
    }

    public void JoinMatch(string IP)
    {
        networkPort = 7777;
        networkAddress = IP;
        StartClient();
    }

    public void StartGame()
    {
        StartHost();
        discovery.Initialize();
        discovery.StartAsServer();
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        MasterController master = GameObject.FindObjectOfType<MasterController>();
        if (master != null)
        {
            master.ConnectionLostScreen.SetActive(true);
        } else
        {
            MainMenuController menu = GameObject.FindObjectOfType<MainMenuController>();
            menu.TimeoutScreen.SetActive(true);
            menu.LoadingPanel.SetActive(false);
        }
    }

    public void SearchMatch()
    {
        if (SearchInvoked) return;
        if (discovery.running)
        {
            discovery.StopBroadcast();
            discovery.running = false;
        }
        SearchInvoked = true;
        Invoke("listenBroadcast", 0.1f);
    }

    void listenBroadcast()
    {
        discovery.Initialize();
        discovery.StartAsClient();
        SearchInvoked = false;
    }

	public override void OnStartClient(NetworkClient client)
	{
        if (discovery.running) { discovery.StopBroadcast(); }
        //discovery.showGUI = false;
	}

	public override void OnStopClient()
	{
        if (discovery.running) { discovery.StopBroadcast(); }
		//discovery.showGUI = true;
	}

}
