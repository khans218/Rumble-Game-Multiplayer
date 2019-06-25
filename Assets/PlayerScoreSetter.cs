using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreSetter : MonoBehaviour {

    public Text NameText;
    public Text ScoreText;
    public Image image;
    public Color localColor;

    public void SetScore(string name, int score, bool islocalplayer)
    {
        NameText.text = name;
        ScoreText.text = score.ToString();
        if (islocalplayer)
        {
            image.color = localColor;
        }
    }

}
