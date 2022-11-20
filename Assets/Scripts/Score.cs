using Photon.Pun;
using UnityEngine.UI;

public class Score : MonoBehaviourPun, IPunObservable
{
    public int score = 0;
    public Text scoreText;
    public Text owner;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) stream.SendNext(score);
        else if(stream.IsReading) score = (int)stream.ReceiveNext();
    }

    void Awake()
    {
        score = 0;
    }
    
    private void Start()
    {
        if (photonView.IsMine) owner.gameObject.SetActive(true);
    }
    
    void Update()
    {
       scoreText.text = score.ToString();
    }
}
