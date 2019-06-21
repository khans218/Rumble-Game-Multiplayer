﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour {

    MasterController MasterTemp;


	// Use this for initialization
	void Start () {
        MasterTemp = GameObject.FindGameObjectWithTag("Master").GetComponent<MasterController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MyEnemyTag" || other.tag == "OtherEnemyTag" || other.tag == "Player")
        {
            if (other.transform.parent == null)
            {
                Destroy(other.gameObject);
            } else
            {
                Destroy(other.transform.parent.gameObject);
            }
        }

        /*
        if (other.tag == "MyEnemyTag")
        {
            Destroy(other.gameObject.transform.parent.gameObject);
            //MasterTemp.CallNewEnemyCreation();
        }
        if (other.tag == "OtherEnemyTag")
        {
            Destroy(other.gameObject.transform.parent.gameObject);
            //MasterTemp.CallNewEnemyCreation();
        }

        if (other.tag == "Player")
        {
            if (MasterTemp.CurrentPlayer != other.transform)
            {
                Destroy(other.gameObject.transform.parent.gameObject);
            } else
            {
                MasterTemp.CallGameOver(2);
            }
        }
        */

    }

}
