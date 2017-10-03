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
        PhotonNetwork.ConnectUsingSettings("0.0.4");
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

        GameObject player;
        switch (teamId)
        {
            case PlayerClass.Rock:
                player = PhotonNetwork.Instantiate("RockPlayer", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
                break;
            case PlayerClass.Paper:
                player = PhotonNetwork.Instantiate("PaperPlayer", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
                break;
            case PlayerClass.Scissors:
                player = PhotonNetwork.Instantiate("ScissorsPlayer", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
                break;
            default:
                player = PhotonNetwork.Instantiate("RockPlayer", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
                break;
        }

        StandbyCamera.SetActive(false);

        player.transform.Find("Main Camera").gameObject.SetActive(true);
        player.transform.Find("Graphics").gameObject.SetActive(false);
        player.GetComponent<FirstPersonController>().enabled = true;

        _uiManager.DisplayHUD(teamId);
    }
}
