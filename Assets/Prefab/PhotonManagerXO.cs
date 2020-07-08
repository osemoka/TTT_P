﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonManagerXO : Photon.MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject lobbyCamera;
    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("1.0");
    }

    void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions() { MaxPlayers = 2 }, TypedLobby.Default);
    }

    void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("PlayerXO", player.transform.position, Quaternion.identity, 0);
    }
}
