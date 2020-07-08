using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public enum turn
    {
        Player1,
        Player2
    }

    public turn currentTurn;

    public bool gameState;
}
