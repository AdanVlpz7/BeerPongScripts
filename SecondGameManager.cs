using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondGameManager : MonoBehaviour
{
    #region
    public static bool isFirstPlayerTurn = true;
    public static bool isSegundPlayerTurn = false;

    //the bools who tells us if any game have gone to game over.
    public static bool mode1playerFinished = false;
    public static bool mode2playersFinished = false;

    //the float members who represent have many glassesSpawnerPositions any player have.
    public static float firstPlayerGlasses = 6;
    public static float secondPlayerGlasses = 6;
    #endregion

    [Tooltip("The ball prefab to instantiate.")] [SerializeField] private GameObject ball;
    [Tooltip("The first player ball spawner position.")] [SerializeField] private GameObject ballPlayerSpawnerPos;
    [Tooltip("The second player ball spawner position.")] [SerializeField] private GameObject ballRivalSpawnerPos;

    public GameObject[] glasses;
    public Camera firstPlayerCamera;
    public Camera secondPlayerCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //when we are playing with 2 players
        if (UIManager.onV2Game)
        {
            if ((secondPlayerGlasses == 0 || firstPlayerGlasses == 0))
            {
                //this means that 1 player mode has already finished
                mode1playerFinished = true;
            }
            if (isFirstPlayerTurn)
            {
                firstPlayerCamera.gameObject.SetActive(true);
                secondPlayerCamera.gameObject.SetActive(false);
            }
            else
            {
                firstPlayerCamera.gameObject.SetActive(false);
                secondPlayerCamera.gameObject.SetActive(true);
            }

        }
    }
    public void EnablingGlassesAtStart()
    {
        foreach (var glass in glasses)
        {
            if (!glass.activeSelf)
                glass.SetActive(true);
        }
    }
}
