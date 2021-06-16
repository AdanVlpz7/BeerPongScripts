using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Member Variables.

    #region Static members.

    //the bools who tells us who has the turn.
    public static bool isFirstPlayerTurn = true;
    public static bool isSegundPlayerTurn = false;

    //the bools who tells us if any game have gone to game over.
    public static bool GameFinished = false;

    //the float members who represent have many glasses any player have.
    public static float firstPlayerGlasses = 6;
    public static float secondPlayerGlasses = 6;

    public static bool pressingBtn = false;

    #endregion

    [Header("UI objects.")]
    [Tooltip("El objeto vacío que engloba todo lo del juego de 1 jugador")] [SerializeField] private GameObject v1gameParentObject;
    [Tooltip("El objeto vacío que engloba todo el menu de game over en un juego de 1 jugador")] [SerializeField] private GameObject v1GameOverMenu;
    [Tooltip("The pause panel.")] [SerializeField] private GameObject pausePanelObject;
    [Tooltip("The array which have all the glasses of 1player mode.")] [SerializeField] private GameObject[] pauseButtons;

    [Header("Ball needed variables.")]
    [Tooltip("The ball prefab to instantiate.")]           [SerializeField] private GameObject[] ball;
    [Tooltip("The first player ball spawner position.")]   [SerializeField] private GameObject ballPlayerSpawnerPos;
    [Tooltip("The second player ball spawner position.")]  [SerializeField] private GameObject ballRivalSpawnerPos;
   
    [Tooltip("The audiosource.")]  [SerializeField] private AudioSource gameAudio;

    [Tooltip("The array which have all the glasses of 1player mode.")] [SerializeField] private GameObject[] glasses;
    
    public delegate void FinishingGame();
    public event FinishingGame GameHasFinished;

    public delegate void PlayerScoring();
    public static event PlayerScoring PlayerHasScored;
    #endregion

    private void FixedUpdate()
    {
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
            WhenPlayerScore();
        }
    }
    public void GoingOutOfGameMode(){
        GameObject ballClone = GameObject.FindGameObjectWithTag("Player");
        Destroy(ballClone);
        for(int i = 0; i < glasses.Length; i++)
        {
            glasses[i].SetActive(false);
        }
    }
    
    void WhenPlayerScore()
    {
        //isFirstPlayerTurn = false;
        //secondPlayerGlasses -= 1;
        LiquidDetector.playerScored = false;
        PlayerHasScored?.Invoke();
    }

    private void OnEnable()
    {
        GameObject ballClone = GameObject.FindGameObjectWithTag("Player");
        Destroy(ballClone);
        isFirstPlayerTurn = true;
        secondPlayerGlasses = 6;
        firstPlayerGlasses = 6;
        for (int i = 0; i < glasses.Length; i++)
        {
            glasses[i].SetActive(true);
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

