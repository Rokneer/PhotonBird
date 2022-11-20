using Photon.Pun;
using UnityEngine;

public class PipeSpawner : MonoBehaviourPun
{
    public float maxTime = 1;
    public float timer;
    public GameObject pipe;
    public float height;

    void Update()
    {
        if (timer > maxTime)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                var randomNum = Random.Range(-height, height);
                photonView.RPC("SpawnPipe", RpcTarget.AllViaServer, randomNum);
                timer = 0;
            }
        }
        timer += Time.deltaTime;
    }

    [PunRPC]
    private void SpawnPipe(float number)
    {
        var newPipe = Instantiate(pipe);
        newPipe.transform.position = transform.position + new Vector3(0, number, 0);
        Destroy(newPipe, 15);
        timer = 0;
    }
}
