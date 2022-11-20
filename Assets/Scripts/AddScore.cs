using Photon.Pun;
using UnityEngine;

public class AddScore : MonoBehaviourPun
{
    private bool _entered;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Entrance") || !photonView.IsMine) return;
        
        if (!collision.isTrigger || _entered) return;
        GameController.CurrentCanva.GetComponent<Score>().score++;
        Debug.Log(GameController.CurrentCanva.GetComponent<Score>().score);
        _entered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _entered = false;
    }
}
