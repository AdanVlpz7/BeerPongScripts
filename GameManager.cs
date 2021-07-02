using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Member Variables.

    #region Static members.

    //the bools who tells us who has the turn.
    public static bool isFirstPlayerTurn = true;
    public static bool isSegundPlayerTurn = false;

    //the bools who tells us if any game have gone to game over.
    public static bool GameFinished = false;

    //the float members who represent have many glassesSpawnerPositions any player have.
    public static float firstPlayerGlasses = 6;
    public static float secondPlayerGlasses = 6;

    public static bool pressingBtn = false;

    public bool ableToShootIsla = false;

    #endregion

    [Header("UI objects.")]
    [Tooltip("El objeto vacío que engloba todo lo del juego de 1 jugador")] [SerializeField] private GameObject v1gameParentObject;
    [Tooltip("El objeto vacío que engloba todo el menu de game over en un juego de 1 jugador")] [SerializeField] private GameObject v1GameOverMenu;
    [Tooltip("The pause panel.")] [SerializeField] private GameObject pausePanelObject;
    [Tooltip("The array which have all the glassesSpawnerPositions of 1player mode.")] [SerializeField] private GameObject[] pauseButtons;

    [Header("Ball needed variables.")]
    [Tooltip("The ball prefab to instantiate.")]           [SerializeField] private GameObject[] ball;
    [Tooltip("The first player ball spawner position.")]   [SerializeField] private GameObject ballPlayerSpawnerPos;
    [Tooltip("The second player ball spawner position.")]  [SerializeField] private GameObject ballRivalSpawnerPos;
   
    [Tooltip("The audiosource.")]  [SerializeField] private AudioSource gameAudio;

    [Tooltip("The array which have all the glassesSpawnerPositions of 1player mode.")] [SerializeField] private GameObject[] glassesSpawnerPositions;
    [SerializeField] private GameObject playerOneGlassPrefab;
    [SerializeField] private GameObject playerTwoGlassPrefab;

    [SerializeField] private Text firstPlayerGlassesTxt;
    [SerializeField] private Text secondPlayerGlassesTxt;
    [SerializeField] private Text turnTxt;

    public delegate void FinishingGame();
    public event FinishingGame GameHasFinished;

    public delegate void PlayerScoring(int coinAmount);
    public static event PlayerScoring PlayerHasScored;
    

    #endregion

    private void FixedUpdate()
    {
        firstPlayerGlassesTxt.text = firstPlayerGlasses.ToString();
        secondPlayerGlassesTxt.text = secondPlayerGlasses.ToString();
        if (isFirstPlayerTurn)
        {
            firstPlayerGlassesTxt.GetComponentInParent<Image>().color = Color.red;
            secondPlayerGlassesTxt.GetComponentInParent<Image>().color = new Color(0.6556604f, 1f, 0.9404028f, 1f);
        }
        else
        {
            firstPlayerGlassesTxt.GetComponentInParent<Image>().color = new Color(0.9529412f, 0.6509804f, 0.6509804f, 1f);
            secondPlayerGlassesTxt.GetComponentInParent<Image>().color = Color.blue;
        }
        if (!GameObject.FindGameObjectWithTag("Player") && isFirstPlayerTurn)
        {
            Debug.Log("Game Manager said is your turn bro");
            Instantiate(ball[AlternatingShopBtn.ballArrayIndex], ballPlayerSpawnerPos.transform.position + new Vector3(0,0.1f,0), Quaternion.identity);
        }
        if (!GameObject.FindGameObjectWithTag("Player") && !isFirstPlayerTurn)
        {
            GameObject rivalBall;
            rivalBall = Instantiate(ball[AlternatingShopBtn.ballArrayIndex], ballRivalSpawnerPos.transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity);
            rivalBall.GetComponent<Ball>().StartCoroutine("ThrowingRandomBall");
            
        }          
        if ((secondPlayerGlasses == 0 || firstPlayerGlasses == 0))
        {
            //this means that 1 player mode has already finished
            GameFinished = true;
            GameHasFinished += GoingOutOfGameMode;
            GameHasFinished += GameOverSettingMenu;
            GameHasFinished();
        }
        if (LiquidDetector.playerScored)
        {
            if (LiquidDetector.imAnIsla)
                WhenFirstPlayerScore(25);
            else
            {
                //if(islaMechanic.GetComponent<IslaMechanic>().islaCount == 1)
                //  islaMechanic.GetComponent<IslaMechanic>().CleaningLists();
                Debug.Log("[GameManager] You scored a regular shot.");
                WhenFirstPlayerScore(5);
            }
        }
        if (LiquidDetector.secondPlayerScored)
        {
            WhenSecondPlayerScore();
        }
    }
    public void GoingOutOfGameMode(){
        GameObject ballClone = GameObject.FindGameObjectWithTag("Player");
        Destroy(ballClone);
    }
    
    void WhenFirstPlayerScore(int coinAmount)
    {
        //isFirstPlayerTurn = false;
        //secondPlayerGlasses -= 1;
        LiquidDetector.playerScored = false;
        PlayerHasScored?.Invoke(coinAmount);
    }
    void WhenSecondPlayerScore()
    {
        //isFirstPlayerTurn = false;
        //secondPlayerGlasses -= 1;
        LiquidDetector.secondPlayerScored = false;
        PlayerHasScored?.Invoke(0);
    }

    public void OnGameManagerEnable()
    {
        GameObject ballClone = GameObject.FindGameObjectWithTag("Player");
        Destroy(ballClone);
        isFirstPlayerTurn = true;
        secondPlayerGlasses = 6;
        firstPlayerGlasses = 6;
        GameFinished = false;
    }
    private void OnEnable()
    {
        for (int i = 0; i < 6; i++)
        {
            //glassesSpawnerPositions[i].SetActive(true);
            Instantiate(playerTwoGlassPrefab, glassesSpawnerPositions[i].transform.position, Quaternion.identity);
        }
        for (int i = 6; i < 12; i++)
        {
            //glassesSpawnerPositions[i].SetActive(true);
            Instantiate(playerOneGlassPrefab, glassesSpawnerPositions[i].transform.position, Quaternion.identity);
        }
        Resuming();
    }

    #region HUD
    private void GameOverSettingMenu()
    {
        v1gameParentObject.SetActive(false);
        v1GameOverMenu.SetActive(true);
    }

    public void Pausing()
    {
        Time.timeScale = 0;
        pausePanelObject.SetActive(true);
        pauseButtons[0].SetActive(false);
        pauseButtons[1].SetActive(true);
        pressingBtn = true;
    }
    public void Resuming()
    {
        Time.timeScale = 1;
        pausePanelObject.SetActive(false);
        pauseButtons[0].SetActive(true);
        pauseButtons[1].SetActive(false);
        pressingBtn = true;
        StartCoroutine("QuittinBtn");
    }
    IEnumerator QuittinBtn()
    {
        yield return new WaitForSeconds(0.3f);
        pressingBtn = false;
    }

    public void MutingAudio()
    {
        gameAudio.Pause();
        pauseButtons[2].SetActive(false);
        pauseButtons[3].SetActive(true);
    }

    public void PlayingAudio()
    {
        gameAudio.Play();
        pauseButtons[2].SetActive(true);
        pauseButtons[3].SetActive(false);
    }
}
#endregion

