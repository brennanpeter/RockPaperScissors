using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

    public Camera StandbyCamera;
    private SpawnSpot[] _spawnSpots;

	// Use this for initialization
	void Start () {
        _spawnSpots = FindObjectsOfType<SpawnSpot>();
        Connect();
	}
	
	// Update is called once per frame
	void Connect () {
        PhotonNetwork.ConnectUsingSettings("0.0.1");
	}

    void OnJoinedLobby()
    {
        Debug.Log("Lobby Joined");
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Failed Joining Random Room");
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        Debug.Log("Room Joined");
        GameObject.Find("UIManager").GetComponent<UIManager>().DisplayClassSelect();
    }

    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    public void SpawnMyPlayer(int teamId)
    {
        if (_spawnSpots == null)
        {
            Debug.Log("_spawnSpots is null, no where to spawn");
            return;
        }

        SpawnSpot[] teamSpots = _spawnSpots.Where(t => t.TeamID == teamId).ToArray();
        SpawnSpot mySpawnSpot = teamSpots[Random.Range(0, teamSpots.Length)];

        GameObject player = PhotonNetwork.Instantiate("Player", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);

        StandbyCamera.enabled = false;
        player.transform.Find("Main Camera").GetComponent<Camera>().enabled = true;
        player.GetComponent<FirstPersonController>().enabled = true;
    }
}
