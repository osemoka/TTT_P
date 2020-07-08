using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardNetworking : MonoBehaviour
{
    private PhotonView photonView;
    [SerializeField] private GameObject[] goList;
    public bool currentTurn = true;

    public Text playerOnePoints;
    public Text playerTwoPoints;
    List<combination> possibleWins = new List<combination>();
    public int[] points = new int[2];
    public GameManager GM;
    public Text currentPlayerLabel;

    struct combination
    {
        public int tic;
        public int tac;
        public int toe;

        public combination(int tic, int tac, int toe)
        {
            this.tic = tic;
            this.tac = tac;
            this.toe = toe;
        }
    }

    private bool onetime = false;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        prepareGame();
    }

    [PunRPC]
    public IEnumerator WinReached(int playerNumber)
    {
        onetime = false; // zmienna bool pozwalająca na jednorazowe wykonanie metody
        yield return new WaitForSeconds(2f); //poczekaj 2 sekundy
        if (!onetime)
        {   //obsługa punktacji
            points[playerNumber-1] += 1;
            playerOnePoints.text = ("Player 1 points: " + points[0]);
            playerTwoPoints.text = ("Player 2 points: " + points[1]);
            onetime = true;
            BroadcastMessage("Win"); //wykonaj metodę Win na wszystkich obiektach, które posiadają tę metodę
        }
    }

    [PunRPC]
    public IEnumerator DrawReached()
    {
        onetime = false;
        yield return new WaitForSeconds(2f);
        if (!onetime)
        {
            onetime = true;
            BroadcastMessage("Win");
        }
    }

    void prepareGame()
    {
        possibleWins.Add(new combination(0, 1, 2));
        possibleWins.Add(new combination(3, 4, 5));
        possibleWins.Add(new combination(6, 7, 8));
        possibleWins.Add(new combination(0, 3, 6));
        possibleWins.Add(new combination(1, 4, 7));
        possibleWins.Add(new combination(2, 5, 8));
        possibleWins.Add(new combination(0, 4, 8));
        possibleWins.Add(new combination(2, 4, 6));
    }

    int counter = 0;

    void checkTilesToDraw()
    {
        //for (int i = 0; i < 9; i++)
        //{
        //    if (goList[i].GetComponent<SpriteRenderer>().sprite.name != "0")
        //    {
        //        counter++;
        //        Debug.Log("Counter:   " + counter);
        //    }
        //    if (counter == 9)
        //    {
        //        GM.gameState = true;
        //        StartCoroutine(DrawReached());
        //    }
        //}
        if (goList[0].GetComponent<SpriteRenderer>().sprite.name != "0" && goList[1].GetComponent<SpriteRenderer>().sprite.name != "0" && goList[2].GetComponent<SpriteRenderer>().sprite.name != "0" &&
            goList[3].GetComponent<SpriteRenderer>().sprite.name != "0" && goList[4].GetComponent<SpriteRenderer>().sprite.name != "0" && goList[5].GetComponent<SpriteRenderer>().sprite.name != "0" &&
            goList[6].GetComponent<SpriteRenderer>().sprite.name != "0" && goList[7].GetComponent<SpriteRenderer>().sprite.name != "0" && goList[8].GetComponent<SpriteRenderer>().sprite.name != "0")
        { 
            GM.gameState = true;
            StartCoroutine(DrawReached());
        }
    }
    


    void Update()
    {
        currentPlayerLabel.text = "Current turn: " + GM.currentTurn.ToString();

        checkTilesToDraw();

        foreach (var possibleWin in possibleWins)
        {
            if(goList[possibleWin.tic].GetComponent<SpriteRenderer>().sprite.name.Equals(goList[possibleWin.tac].GetComponent<SpriteRenderer>().sprite.name) &&
                goList[possibleWin.tac].GetComponent<SpriteRenderer>().sprite.name.Equals(goList[possibleWin.toe].GetComponent<SpriteRenderer>().sprite.name) && !GM.gameState)
            {
                switch (goList[possibleWin.tic].GetComponent<SpriteRenderer>().sprite.name)
                {
                    case "0":
                        break;
                    case "1":
                        GM.gameState = true;
                        StartCoroutine(WinReached(1));
                        break;
                    case "2":
                        GM.gameState = true;
                        StartCoroutine(WinReached(2));
                        break;
                    default:
                        Debug.Log("there is something else in the field");
                        break;
                }
            }
        }
    }
}
