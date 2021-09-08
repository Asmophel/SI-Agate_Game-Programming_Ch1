using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Tombol gerak atas
    public KeyCode upButton = KeyCode.W;

    // Tombol gerak bawah
    public KeyCode downButton = KeyCode.S;

    // Kecepatan gerak
    public float speed = 10.0f;

    // Batas atas & bawah game scene (batas bawah menggunakan minus (-))
    public float yBoundary = 9.0f;

    // Rigidbody 2D raket ini
    private Rigidbody2D rigidbody2D;

    // skor pemain
    private int score;

    private ContactPoint2D lastContactPoint;

    public ContactPoint2D LastContactPoint
    {
        get { return lastContactPoint; }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // dapatkan kecepatan racket sekarang
        Vector2 velocity = rigidbody2D.velocity;

        // Jika pemain menekan tombol keatas, beri kecepatan positif ke komponen y (keatas)
        if (Input.GetKey(upButton))
        {
            velocity.y = speed;
        }

        // Jika pemain menekan tombol keatas, beri kecepatan positif ke komponen y (kebawah)
        else if (Input.GetKey(downButton))
        {
            velocity.y = -speed;
        }

        // Jika tidak menekan apa", kec = 0
        else
        {
            velocity.y = 0.0f;
        }

        // Masukkan kembali kecepatannya ke rigidBody2D
        rigidbody2D.velocity = velocity;

        // Dapatkan posisi raket skrg
        Vector3 position = transform.position;

        // Jika posisi raket melewati batas bawah (-yBoundary), kembalikan ke batas atas tsb.
        if (position.y > yBoundary)
        {
            position.y = yBoundary;
        }

        // Jika posisi raket melewati batas bawah (-yBoundary), kembalikan ke batas atas tsb.
        else if (position.y < -yBoundary)
        {
            position.y = -yBoundary;
        }

        // Masukkan kembali posisinya ke transform
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Ball"))
        {
            lastContactPoint = collision.GetContact(0);
        }
    }

    // Menaikkan skor sebanyak 1 poin
    public void IncrementScore()
    {
        score++;
    }

    // Mengembalikan skor menjadi 0
    public void ResetScore()
    {
        score = 0;
    }

    // Mendapatkan nilai skor
    public int Score
    {
        get { return score; }
    }
}
