using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserManager : MonoBehaviour
{
    [SerializeField] private Text menuCoinIndicator;
    [SerializeField] private Text shopCoinIndicator;
    [SerializeField] private Text gameCoinIndicator;
    int c = 0;

    public List<int> ballArrayAdded = new List<int>(); // TO SAVE
    public int coins; // TO SAVE
    public static int ballIndexUsed = 0; // TO SAVE
    private void Start()
    {
        //UpdateCoinIndicator(0);
        ballArrayAdded.Add(0);
    }
    private void OnEnable()
    {
        ballIndexUsed = AlternatingShopBtn.ballArrayIndex;
        c = 0;
        GameManager.PlayerHasScored += UpdateCoinIndicator;
    }
    private void FixedUpdate()
    {
        if (LiquidDetector.playerScored && !LiquidDetector.imAnIsla)
        {
           StartCoroutine(UpdatingPlayerHasScored(false));
        }
        if(LiquidDetector.playerScored && LiquidDetector.imAnIsla)
        {
            StartCoroutine(UpdatingPlayerHasScored(true));
        }       
    }

    private IEnumerator UpdatingPlayerHasScored(bool itWasIsla)
    {
        yield return new WaitForSeconds(2f);
        if (!itWasIsla)
        {
            GameManager.PlayerHasScored -= SuperUpdateCoinIndicator;
            GameManager.PlayerHasScored += UpdateCoinIndicator;
        }
        if (itWasIsla)
        {
            GameManager.PlayerHasScored -= UpdateCoinIndicator;
            GameManager.PlayerHasScored += SuperUpdateCoinIndicator;
        }
    }
    public void UpdateCoinIndicator(int coinAmount)
    {
        Debug.Log("[User Manager] Updating coin count.");
        coins += coinAmount;
        menuCoinIndicator.text = coins.ToString();
        gameCoinIndicator.text = coins.ToString();
        shopCoinIndicator.text = coins.ToString();
    }

    public void SuperUpdateCoinIndicator(int coinAmount)
    {
        Debug.Log("[User Manager] Super updating coin count.");
        coins += coinAmount;
        menuCoinIndicator.text = coins.ToString();
        gameCoinIndicator.text = coins.ToString();
        shopCoinIndicator.text = coins.ToString();
    }

    public void UpdateAfterBuying(int price,int indexToAdd)
    {
        ballArrayAdded.Add(indexToAdd);
        coins -= price;
        Debug.Log("[User Manager] You have bought something!.");
        menuCoinIndicator.text = coins.ToString();
        gameCoinIndicator.text = coins.ToString();
        shopCoinIndicator.text = coins.ToString();
    }

}