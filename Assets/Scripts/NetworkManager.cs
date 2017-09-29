using System.Linq;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

    public GameObject StandbyCamera;
    private UIManager _uiManager;
    private SpawnSpot[] _spawnSpots;

	// Use this for initialization
	void Start () {
        _spawnSpots = FindObjectsOfType<SpawnSpot>();
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        Connect();
	}
	
	// Update is called once per frame
	void Connect () {
        PhotonNetwork.ConnectUsingSettings("0.0.3");
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
        _uiManager.DisplayClassSelect();
    }

    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    public void SpawnMyPlayer(PlayerClass teamId)
    {
        if (_spawnSpots == null)
        {
            Debug.Log("_spawnSpots is null, no where to spawn");
            return;
        }

        SpawnSpot[] teamSpots = _spawnSpots.Where(t => t.ClassID == teamId).ToArray();
        SpawnSpot mySpawnSpot = teamSpots[Random.Range(0, teamSpots.Length)];

        GameObject player = PhotonNetwork.Instantiate("Player", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
        GameObject graphics = null;
        switch (teamId)
        {
            case PlayerClass.Rock:
                graphics = PhotonNetwork.Instantiate("RockModel", player.transform.position, player.transform.rotation, 0);
                break;
            case PlayerClass.Paper:
                graphics = PhotonNetwork.Instantiate("PaperPerson", player.transform.position, player.transform.rotation, 0);
                break;
            case PlayerClass.Scissors:
                graphics = PhotonNetwork.Instantiate("ScissorsPerson", player.transform.position, player.transform.rotation, 0);
                break;
        }
        graphics.transform.SetParent(player.transform);

        StandbyCamera.SetActive(false);

        player.transform.Find("Main Camera").gameObject.SetActive(true);
        player.GetComponent<FirstPersonController>().enabled = true;

        _uiManager.DisplayHUD(teamId);
    }
}
