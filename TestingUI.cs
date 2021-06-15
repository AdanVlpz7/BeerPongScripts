using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingUI : MonoBehaviour
{
    public Text turnText;
    public Text rivalGlassesText;
    public Text playerGlassesText;

    public Text xReferenceText;
    public Text yReferenceText;
    public Text zReferenceText;

    public Text xBallText;
    public Text yBallText;
    public Text zBallText;

    public Text timeIntervalText;

    public GameObject ball;
    // Update is called once per frame
    void LateUpdate()
    {
        if (GameManager.isFirstPlayerTurn)
            turnText.text = "Is Player Turn";
        else
            turnText.text = "Is Rival Turn";

        rivalGlassesText.text = "RivalGlasses: " + GameManager.secondPlayerGlasses.ToString();
        playerGlassesText.text = "PlayerGlasses: " + GameManager.firstPlayerGlasses.ToString();

        xReferenceText.text = "XForce: " + Ball.xAddForceReference;
        yReferenceText.text = "YForce: " + Ball.yAddForceReference;
        zReferenceText.text = "ZForce: " + Ball.zAddForceReference;

        ball = GameObject.FindGameObjectWithTag("Player");
        if(ball != null)
        {
            xBallText.text = "X: " + ball.transform.position.x;
            yBallText.text = "Y: " + ball.transform.position.y;
            zBallText.text = "Z: " + ball.transform.position.z;
        }

        timeIntervalText.text = Ball.timeIntervalReference.ToString();
    }
}
