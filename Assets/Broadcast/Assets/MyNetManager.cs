using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MyNetManager : NetworkManager
{
    public NetworkDiscovery discovery;

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
        GameObject master = GameObject.Find("MasterController");
        if (master != null)
        {
            master.GetComponent<MasterController>().ConnectionLostScreen.SetActive(true);
        } else
        {
            //unable to join game
        }
    }

    public void SearchMatch()
    {
        if (discovery.running)
        {
            discovery.StopBroadcast();
            discovery.running = false;
        }
        Invoke("listenBroadcast", 1f);
    }

    void listenBroadcast()
    {
        discovery.Initialize();
        discovery.StartAsClient();
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
