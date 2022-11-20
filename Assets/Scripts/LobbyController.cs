using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class LobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField] string gameVersion = "1";
    [SerializeField] private Button continueButton;
    [SerializeField] TMP_Text roomReadyText;
    [SerializeField] TMP_Text usernameText;
    
    void Awake()
    {
        usernameText.text = PhotonNetwork.NickName;
    }
    public void Connect()
    {
        if (PhotonNetwork.IsConnected) PhotonNetwork.JoinRandomRoom();
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    void SetButton(bool state, string msg)
    {
        continueButton.GetComponentInChildren<TMP_Text>().text = msg;
        continueButton.GetComponent<Button>().enabled = state;
    }

    public override void OnJoinedRoom()
    {
        SetButton(false, "Waiting Players");
        Debug.Log("PlayerCount:" + PhotonNetwork.CurrentRoom.PlayerCount);

        if (PhotonNetwork.CurrentRoom.PlayerCount != 2) return;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        roomReadyText.gameObject.SetActive(true);
        roomReadyText.text = "Room is Ready";
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined the room, Players: " + PhotonNetwork.CurrentRoom.PlayerCount);

        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
        {
            SetButton(true, "Ready!");
        }

        if (PhotonNetwork.CurrentRoom.PlayerCount != 2) return;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        roomReadyText.gameObject.SetActive(true);
        roomReadyText.text = "Room is Ready";
    }

    public void LoadGameLevel()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LoadLevel(2);
    }
}
