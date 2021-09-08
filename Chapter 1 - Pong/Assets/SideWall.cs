using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWall : MonoBehaviour
{
    // Pemain yg akan bertambah skornya jika bola menyentuh dinding ini
    public PlayerControl player;
    [SerializeField] private GameManager gameManager;

    // Akan dipanggil saat objek lain collide dg dinding (bola)
    private void OnTriggerEnter2D(Collider2D anotherCollider)
    {
        if (anotherCollider.name == "Ball")
        {
            // Tambahkan skor ke pemain
            player.IncrementScore();

            // Jika skor pemain belum mencapai skor maksimal
            if (player.Score < gameManager.maxScore)
            {
                // restart bola stlh kena dinding
                anotherCollider.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);
            }
        }
    }
}
