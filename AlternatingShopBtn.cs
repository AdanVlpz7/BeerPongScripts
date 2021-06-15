using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlternatingShopBtn : MonoBehaviour
{
    public static int ballArrayIndex = 0;
    // Start is called before the first frame update
    public Text[] textos = new Text[5];
    public int arbitrary = ballArrayIndex;
    public bool changingBall = false;
    public void ChangeArrayIndex(int num)
    {
        if(arbitrary != num)
        {
            ResettingText();
            ballArrayIndex = num;
            arbitrary = ballArrayIndex;
        }
  
    }
    public void ChangingTextOfButtonUse(Text x)
    {
        x.text = "Using";
    }
    public void ResettingText()
    {
        textos[ballArrayIndex].text = "Use";
    }
}
