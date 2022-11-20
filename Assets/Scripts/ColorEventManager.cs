using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

public class ColorEventManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    private const byte ColorEventCode = 1;
    public static ColorEventManager Instance;

    public void Awake()
    {
        Instance = this;
    }
    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void SetCustomColor(string myColor, int id)
    {
        var data = new object[2];
        data[0] = myColor;
        data[1] = id;
        var eventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.All,
            CachingOption = EventCaching.AddToRoomCache,
        };

        PhotonNetwork.RaiseEvent(ColorEventCode, data, eventOptions, SendOptions.SendReliable);
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code != ColorEventCode) return;
        var data = (object[])photonEvent.CustomData;
        ReceiveCustomColor(data);
    }

    public void ReceiveCustomColor(object[] dataReceive)
    {
        var color = ChangeColor((string)dataReceive[0]);
        var listPhotonViews = FindObjectsOfType<FlyBird>();
        var objectID = (int)dataReceive[1];
        foreach (var item in listPhotonViews)
        {
            if(item.GetComponent<PhotonView>().ViewID == objectID)
            {
               item.GetComponent<SpriteRenderer>().color = color;
            }
        }
    }

    private static Color ChangeColor(string color)
    {
        return (Color)typeof(Color).GetProperty(color.ToLowerInvariant())!.GetValue(null, null);
    }
}
