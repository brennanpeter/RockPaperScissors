using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpot : MonoBehaviour {

    public int TeamID = 0;
    private Vector3 _offset = new Vector3(0f, 0.5f, 0f);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position + _offset, new Vector3(1, 1, 1));
    }
}
