using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlternatingShopBtn : MonoBehaviour
{
    public static int ballArrayIndex = 0;

    public Text[] coindIndicators = new Text[5];
    private int arbitrary = ballArrayIndex;
    private bool changingBall = false;

    public void ChangeArrayIndex(int num)
    {
        if(arbitrary != num)
        {
            ResettingText();
            ballArrayIndex = num;
            arbitrary = ballArrayIndex;
        }
  
    }
    public void ChangingTextOfButtonUse(Text TextOfButton)
    {
        TextOfButton.text = "Using";
    }
    public void ResettingText()
    {
        coindIndicators[ballArrayIndex].text = "Use";
    }
}
