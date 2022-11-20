using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab, p1Canva, p2Canva;
    [SerializeField] private Transform playerSpawn;

    public static GameObject CurrentCanva;

    private void Start()
    {
        if (!PhotonNetwork.IsConnected) SceneManager.LoadScene(0);
        if (playerPrefab == null) Debug.LogError("Missing Prefab");
        else
        {
            var spawnCanva = (PhotonNetwork.IsMasterClient) ? p1Canva : p2Canva;
            
            var initData = new object[1];
            initData[0] = "Data instanciacion";

            var flybird = PhotonNetwork.Instantiate(playerPrefab.name, playerSpawn.position, Quaternion.identity, 0, initData);
            flybird.GetComponent<SpriteRenderer>().color = ChangeColor(PhotonNetwork.LocalPlayer.CustomProperties["color"].ToString());

            CurrentCanva = PhotonNetwork.Instantiate(spawnCanva.name, spawnCanva.transform.position, Quaternion.identity, 0, initData);
        }
    }

    private static Color ChangeColor(string color)
    {
        return (Color)typeof(Color).GetProperty(color.ToLowerInvariant())!.GetValue(null, null);
    }

}
