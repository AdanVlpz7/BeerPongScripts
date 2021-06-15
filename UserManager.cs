using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserManager : MonoBehaviour
{
    [SerializeField] private Text menuCoinIndicator;
    [SerializeField] private Text shopCoinIndicator;
    [SerializeField] private Text gameCoinIndicator;
    protected int coins = 0;
    public static int ballIndexUsed = 0;
    private void OnEnable()
    {
        ballIndexUsed = AlternatingShopBtn.ballArrayIndex;
    }

    private void Start()
    {
        GameManager.playerScoring += UpdateCoinIndicator;
    }
    public void UpdateCoinIndicator()
    {
        coins += 5;
        menuCoinIndicator.text = coins.ToString();
        gameCoinIndicator.text = coins.ToString();
        shopCoinIndicator.text = coins.ToString();
    }


}
