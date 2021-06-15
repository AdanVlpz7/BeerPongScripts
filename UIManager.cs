using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Declaración de variables. 
    [Tooltip("El objeto vacío que engloba todo lo del juego de 1 jugador")] [SerializeField] private GameObject v1gameParentObject;
    [Tooltip("El objeto vacío que engloba todo lo del juego de 1 jugador")] [SerializeField] private GameObject v1PlayerGameParent;
    [Tooltip("El objeto vacío que engloba todo lo del juego de 1 jugador")] [SerializeField] private GameObject v1PlayerGameOverParent;

    [Tooltip("El objeto vacío que engloba todo el menu principal")]                             [SerializeField] private GameObject menuParentObject;
    [Tooltip("El objeto vacío que engloba todo la tienda del juego")]                           [SerializeField] private GameObject shopParentObject;
    
    [Tooltip("El objeto vacío que engloba todo respecto al juego de 2 jugadores")]      [SerializeField] private GameObject v2GameTotalGOParent;
    [Tooltip("El objeto vacío que engloba todo el menu previo a un juego 2 jugadores")] [SerializeField] private GameObject v2GamePreviousMenu;
    [Tooltip("El objeto vacío que engloba todo lo del juego 2 jugadores")]              [SerializeField] private GameObject v2GameParentMenu;
    [Tooltip("El objeto vacío que engloba todo lo del Game Over de 2 jugadores")]       [SerializeField] private GameObject v2GameOverMenu;

    [Tooltip("El InputField donde el usuario escribe el nombre el jugador 1.")] [SerializeField] private InputField player1field;
    [Tooltip("El InputField donde el usuario escribe el nombre el jugador 2.")] [SerializeField] private InputField player2field;

    [SerializeField] private Text v1GameOverMessage;


    public static bool onV1Game = false;
    public static bool onV2Game = false;

    public int coins = 0;
    #endregion

    #region 1 player mode respectic UI.

    //Added to a button in PrincipalMenu to go to the 1 player mode game.
    public void GoTo1v1Game()
    {
        onV1Game = true;
        v1gameParentObject.SetActive(true);
        v1PlayerGameParent.SetActive(true);
        v1PlayerGameOverParent.SetActive(false);
        menuParentObject.SetActive(false); 
        GameManager.firstPlayerGlasses = 6;
        GameManager.secondPlayerGlasses = 6;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Destroy(player);
    }

    //Added to a button in the GameOverCanvas to go back to the menu.
    public void GoToMenuAfter1PlayerModeGameOver()
    {
        GameObject GameArea = GameObject.FindGameObjectWithTag("GameObjectParent");
        menuParentObject.SetActive(true);
        GameArea.SetActive(false);
        v1PlayerGameOverParent.SetActive(false);
        v1PlayerGameParent.SetActive(true);
    }

    #endregion

    #region 2 players mode respectic UI.
    public void GoTo2v2Menu()
    {
        HUDManager.player1Name = null;
        HUDManager.player2Name = null;
        onV2Game = true;
        v2GameTotalGOParent.SetActive(true);
        v2GamePreviousMenu.SetActive(true);
        menuParentObject.SetActive(false);
    }

    public void GoTo2PlayersGame()
    {
        HUDManager.player1Name = player1field.text;
        HUDManager.player2Name = player2field.text;
        GameManager.firstPlayerGlasses = 6;
        GameManager.secondPlayerGlasses = 6;        
        v2GamePreviousMenu.SetActive(false);
        v2GameParentMenu.SetActive(true);
    }
    #endregion

    #region Other methods

    //Added to a button in the Game HUD to quit game
    public void QuitGame()
    {
        Debug.Log("Pressing Quiting Button of 1 Player Mode.");
        GameObject GameArea = GameObject.FindGameObjectWithTag("GameObjectParent");
        menuParentObject.SetActive(true);
        GameArea.SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (GameManager.firstPlayerGlasses == 0)
        {
            v1GameOverMessage.text = "You lost. Loser";
        }
        if(GameManager.secondPlayerGlasses == 0)
        {
            v1GameOverMessage.text = "You win. GG bro";
        }
    }

    public void GoToMenu()
    {
        menuParentObject.SetActive(true);
        shopParentObject.SetActive(false);
    }

    public void GoToShop()
    {
        menuParentObject.SetActive(false);
        shopParentObject.SetActive(true);
    }
    #endregion
}
