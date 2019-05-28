using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Required when Using UI elements.

public class Cooldown : MonoBehaviour
{
    public Image cooldownIMG;
    public bool coolingDown;
    public float waitTime = 5.0f;
    public GameObject specialBtn;
    public GameObject specialBack;
    public GameObject CoolDownCover;

    void Start()
    {
    
    }

    void Update()
    {
        if (coolingDown == true)
        {
            cooldownIMG.fillAmount += 0.1f / waitTime * Time.deltaTime;
        }
        if (cooldownIMG.fillAmount == 1)
        {
            CooldownNO();
        }
    }

    public void CooldownYES()
    {

        coolingDown = true;
        specialBack.SetActive(true);
        specialBtn.gameObject.GetComponent<Button>().interactable = false;
        specialBtn.gameObject.GetComponent<Image>().fillAmount = 0;
        CoolDownCover.SetActive(true);
    }

    void CooldownNO()
    {
        coolingDown = false;
        specialBack.SetActive(false);
        specialBtn.gameObject.GetComponent<Button>().interactable = true;
        CoolDownCover.SetActive(false);
    }

}
