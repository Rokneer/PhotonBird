using UnityEngine;
using Photon.Pun;

public class UsernameManager : MonoBehaviour
{
    private const string PlayerNamePrefKey = "PlayerName";

    private void Start()
    {
        var defaultName = string.Empty;
        var inputField = GetComponent<TMPro.TMP_InputField>();

        if(inputField != null)
        {
            if (PlayerPrefs.HasKey(PlayerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(PlayerNamePrefKey);
                inputField.text = defaultName;
            }
        }
        PhotonNetwork.NickName = defaultName;
    }

    public void SetPlayerName(string value)
    {
        if (string.IsNullOrEmpty(value)) return;

        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString(PlayerNamePrefKey, value);
    }

}
