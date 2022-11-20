using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class FlyBird : MonoBehaviourPunCallbacks
{
    private GameManager _gameManager;
    public float velocity = 1;
    private Rigidbody2D _rb;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
        
        if (photonView.IsMine) ColorEventManager.Instance.SetCustomColor(PhotonNetwork.LocalPlayer.
            CustomProperties["color"].ToString(), photonView.ViewID);
    }

    private void Update()
    {
        if (!photonView.IsMine) return;
        if (Input.GetMouseButtonDown(0)) _rb.velocity = Vector3.up * velocity;
        else if (Input.GetKeyDown(KeyCode.Space)) _rb.velocity = Vector3.up * velocity;
    }
    
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A player has joined");
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe"))
        {
            _gameManager.GameOver(gameObject);
        }
    }
}
