using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    public string[] addresses;

    Dictionary<string, int> myDictionary = new Dictionary<string, int>();

    private void Start()
    {
        myDictionary.Add("192.168.1.1", 0);
        myDictionary.Add("192.168.1.1", 1);
    }

    // Update is called once per frame
    void Update () {
        Debug.Log(myDictionary["192.168.1.1"]);
	}
}
