using UnityEngine;

[RequireComponent(typeof(FirstPersonController))]
public class NetworkCharacter : Photon.MonoBehaviour {

    FirstPersonController _characterController;

    Vector3 _realPosition = Vector3.zero;
    Quaternion _realRotation = Quaternion.identity;
    Vector3 _velocity = Vector3.zero;

    private void Awake()
    {
        _characterController = GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update () {
        if (photonView.isMine)
        {
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, _realPosition, 0.1f) + _velocity * Time.deltaTime;
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
            stream.SendNext(_characterController.Velocity);
        }
        else
        {
            // This is someone elses player, we need to receive their position and update their position on our side
            _realPosition = (Vector3)stream.ReceiveNext();
            _realRotation = (Quaternion)stream.ReceiveNext();
            _velocity = (Vector3)stream.ReceiveNext();
        }
    }
}
