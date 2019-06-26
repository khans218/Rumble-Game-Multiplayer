using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HostEndGameAnim : MonoBehaviour {

    public Image background;
    public MasterController master;
    float currentAlpha = 0f;
    public int TargetAlpha;
    public float TotalTime;
    float rate;
    public InvectorJoystick joystick;

    private void Awake()
    {
        if (master.CurrentPlayer != null)
        {
            joystick.DisableAxes();
        }
        rate = TargetAlpha / TotalTime;
        if (master.isHost)
        {
            Invoke("LeaveGame", TotalTime + 1.5f);
        } else
        {
            Invoke("LeaveGame", TotalTime + 0.5f);
        }
    }

    // Update is called once per frame
    void Update () {
        if (currentAlpha >= TargetAlpha) { return; }
        currentAlpha += rate * Time.deltaTime;
        background.color = new Color(0f, 0f, 0f, currentAlpha / 255f);
	}

    void LeaveGame()
    {
        master.net.LeaveGame();
    }

}
