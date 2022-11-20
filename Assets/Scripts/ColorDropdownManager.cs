using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColorDropdownManager : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        gameObject.GetComponent<TMP_Dropdown>().value = 0;
    }

    public void SelectColor(int index)
    {
        var propsToSet = new ExitGames.Client.Photon.Hashtable { { "color", gameObject
            .GetComponent<TMP_Dropdown>().options[index].text } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(propsToSet);
    }
}
