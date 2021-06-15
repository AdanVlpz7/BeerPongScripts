using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public delegate void GoingTo2PlayersGame(string name1, string name2);

    public static string player1Name;
    public static string player2Name;

    public int coins;
}
