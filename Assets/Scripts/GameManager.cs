using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject gameOverCanvas;
    public TMP_Text winner;

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void GameOver(GameObject player)
    {
        if (!player.GetPhotonView().IsMine)
        {
            var winnerName = PhotonNetwork.LocalPlayer.NickName;
            photonView.RPC("SetWinner", RpcTarget.AllViaServer, winnerName);
        }
        gameOverCanvas.SetActive(true);
    }
    
    public void Replay()
    {
        Time.timeScale = 1;
        PhotonNetwork.LeaveRoom();
    }
    
    public override void OnLeftRoom()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        base.OnLeftRoom();
        PhotonNetwork.Disconnect();
    }
    
    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene(0);
    }
    
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        base.OnLeftRoom();
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }

    [PunRPC]
    public void SetWinner(string text)
    {
        winner.gameObject.SetActive(true);
        winner.text = text + " has won";
        Time.timeScale = 0;
    }
}
