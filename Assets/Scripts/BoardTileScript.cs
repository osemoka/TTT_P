using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardTileScript : MonoBehaviour
{

    private PhotonView photonView;
    public GameManager GM;

    public Sprite[] spriteList;
    private SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        photonView = GetComponent<PhotonView>();
    }
    
    public void OnMouseDown()
    {
        if (PhotonNetwork.player.NickName == GM.currentTurn.ToString())
        ChangeState();
    }

    public void ChangeState()
    {
        if (photonView)
            photonView.RPC("ChangeStateRPC", PhotonTargets.AllBuffered);
    }

    [PunRPC]
    private void ChangeStateRPC()
    {     
        if (!GM.gameState)
        {
            if (GM.currentTurn == GameManager.turn.Player1)
                sr.sprite = spriteList[1];
            if (GM.currentTurn == GameManager.turn.Player2)
                sr.sprite = spriteList[2];

            if (GM.currentTurn == GameManager.turn.Player1)
            {
                GM.currentTurn = GameManager.turn.Player2;
            }
            else if (GM.currentTurn == GameManager.turn.Player2)
            {
                GM.currentTurn = GameManager.turn.Player1;
            }
        }
    }

    public void Win()
    {
        if (photonView)
            photonView.RPC("reset", PhotonTargets.AllBuffered);
    }

    [PunRPC]
    public void reset()
    {
        sr.sprite = spriteList[0];
        GM.gameState = false;
    }
}
