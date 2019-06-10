using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEnemyManager : MonoBehaviour {

    [HideInInspector]public Invector.vHealthController HealthTemp;
    bool OneTime = true;
    public Rigidbody Body;
    CapsuleCollider Col;
    bool TagChanged = false;
    [SerializeField]
    Behaviour thirdPersonController;

    // Use this for initialization
    void Start () {
        HealthTemp = this.transform.GetComponentInParent<Invector.vHealthController>();
        Col = this.transform.GetComponentInParent<CapsuleCollider>();

    }
	
	// Update is called once per frame
	void Update () {

	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BlastSpherePlayer" && TagChanged == false)
        {
            TagChanged = true;
            Body.tag = "MyEnemyTag";
            if (thirdPersonController != null)
            {
                thirdPersonController.enabled = true;
            }
            HealthTemp.CustomCurrentHealth(0f);
            Invoke("ApplyExtraForce", 0.1f);
        }

        if (other.tag == "BlastSphereEnemy" && TagChanged == false)
        {
            TagChanged = true;
            Body.tag = "OtherEnemyTag";
            HealthTemp.CustomCurrentHealth(0f);
            Invoke("ApplyExtraForce", 0.1f);
        }
    }


    void ApplyExtraForce()
    {
        Body.AddRelativeForce(new Vector3(0,35000,-65000));
    }

    public bool Hit()
    {
        return TagChanged;
    }

}

internal class Public
{

}