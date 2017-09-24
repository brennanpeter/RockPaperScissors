using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkCharacter : Photon.MonoBehaviour {

    Vector3 _realPosition = Vector3.zero;
    Quaternion _realRotation = Quaternion.identity;
	
	// Update is called once per frame
	void Update () {
		if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, _realPosition, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, _realRotation, 0.1f);
        }
	}

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // This is our player, we need to send our actual position
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            // This is someone elses player, we need to receive their position and update their position on our side
            _realPosition = (Vector3)stream.ReceiveNext();
            _realRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
