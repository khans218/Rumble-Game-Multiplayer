using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffThis : MonoBehaviour {

    public float Time;

    private void OnEnable()
    {
        Invoke("TurnOff", Time);
    }

    void TurnOff()
    {
        this.gameObject.SetActive(false);
    }

    public void DestroyThis()
    {
        GameObject.Destroy(this.gameObject);
    }

}
