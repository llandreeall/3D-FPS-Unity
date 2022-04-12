using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;

    public float min_x = 18f;
    public float max_x = 22f;
    public float ref_y = 2.49f;
    public float min_z = -1f;
    public float max_z = 1f;
    // Start is called before the first frame update
    void Awake()
    {
        Vector3 randomPos = new Vector3(Random.Range(min_x, max_x), ref_y, Random.Range(min_z, max_z));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPos, Quaternion.identity);
    }

}
