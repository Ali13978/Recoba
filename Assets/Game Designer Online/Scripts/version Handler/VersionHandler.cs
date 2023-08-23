using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class VersionHandler : MonoBehaviour
{
    public static VersionHandler Instance;
    public bool isFree = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetVersion(bool isFreeVersion)
    {
        isFree = isFreeVersion;
        PhotonNetwork.LoadLevel("LoginScreen"); // Load the login scene after selecting version
    }
}
