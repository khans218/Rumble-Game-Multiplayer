using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {
    public Animator anim;
    public GameObject core;
    bool enabled = false;

    private void Update()
    {
        if (enabled)
        {
            if (!core.activeSelf)
            {
                anim.SetBool("Attacking", false);
                enabled = false;
            }
        }
        if (anim.GetBool("Attacking"))
        {
            if (!core.activeSelf)
            {
                enabled = true;
                core.SetActive(true);
            }
        }
    }
}
