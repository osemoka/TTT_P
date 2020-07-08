using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworkingXO : MonoBehaviour
{
    [SerializeField] private GameObject playerCamera;

    private PhotonView photonView;

    // Use this for initialization
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        Initialize();
    }
    void Initialize()
    {
        if (photonView.isMine)
        {

        }
        else
        {
            playerCamera.SetActive(false);
        }
    }
}
