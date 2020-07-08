using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonManager : Photon.MonoBehaviour {
	
	[SerializeField]private GameObject player;
    [SerializeField]private GameObject lobbyCamera;
    [SerializeField]private Transform spawnPoint;

    void Start () {
		PhotonNetwork.ConnectUsingSettings ("1.0");
    }

    void OnJoinedLobby(){
		PhotonNetwork.JoinOrCreateRoom ("Room", new RoomOptions (){ MaxPlayers = 2 }, TypedLobby.Default);
	}

    void OnJoinedRoom() {
        if (PhotonNetwork.countOfPlayers == 1)
        {
            PhotonNetwork.Instantiate("Player", player.transform.position, Quaternion.identity, 0);
            PhotonNetwork.player.NickName = "Player1";
        }
        else
        {
            PhotonNetwork.Instantiate("Player", spawnPoint.transform.position, spawnPoint.rotation, 0);
            PhotonNetwork.player.NickName = "Player2";
        }
        Debug.Log("lista graczy: " + PhotonNetwork.playerList);
    }
}
