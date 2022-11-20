using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public enum RegionCode
{
    AUTO,
    CAE,
    EU,
    US,
    USW,
    SA
}
public class ConnectCrtl : MonoBehaviourPunCallbacks
{
    [SerializeField] string gameVersion = "1";
    [SerializeField] string regionCode;
    [SerializeField] private Button button;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = RegionCode.CAE.ToString();
    }

    public void SetRegion(int index)
    {
        var region = (RegionCode)index;

        regionCode = region == RegionCode.AUTO ? RegionCode.CAE.ToString() : region.ToString();

        Debug.Log("Region selected: " + regionCode);
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = regionCode;

    }

   public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

   private void SetButton(bool state, string msg)
    {
        button.GetComponentInChildren<TMPro.TMP_Text>().text = msg;
        button.GetComponent<Button>().enabled = state;
    }

    public void SetReady()
    {
        var propsToSet = new ExitGames.Client.Photon.Hashtable() { { "ready", true } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(propsToSet);
    }

    #region MonoBehaviourPunCallbacks Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN");
        SetButton(true, "Go to room");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
        PhotonNetwork.CreateRoom(null, new RoomOptions());
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Lobby");
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            Debug.Log("Room is Ready");
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " Joined the room, Players: " + PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(2);
        }
    }
    #endregion

}
