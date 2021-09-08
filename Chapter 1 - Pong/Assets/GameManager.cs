using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // pemain 1
    public PlayerControl player1;
    private Rigidbody2D player1Rigidbody;

    // pemain 2
    public PlayerControl player2;
    private Rigidbody2D player2Rigidbody;

    // Bola
    public BallControl ball;
    private Rigidbody2D ballRigidBody;
    private CircleCollider2D ballCollider;

    // Skor max
    public int maxScore;

    // Apakah debug window ditampilkan?
    private bool isDebugWindowShown = false;

    // Objek untuk menggambar prediksi lintasan bola
    public Trajectory trajectory;

    // init rigidbody dan collider
    private void Start()
    {
        player1Rigidbody = player1.GetComponent<Rigidbody2D>();
        player2Rigidbody = player2.GetComponent<Rigidbody2D>();
        ballRigidBody = ball.GetComponent<Rigidbody2D>();
        ballCollider = ball.GetComponent<CircleCollider2D>();
    }

    // GUI
    private void OnGUI()
    {
        // Tampilkan skor pemain 1 di kiri atas dan pemain 2 di kanan atas
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + player2.Score);
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + player1.Score);

        // Tombol Restart
        if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
        {
            // reset skor pemain jika restart di tekan
            player1.ResetScore();
            player2.ResetScore();

            // Restart game
            ball.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
        }

        // jika p1 menang (skor max)
        if (player2.Score == maxScore)
        {
            // tampilkan teks "PLAYER ONE WINS" di kiri layar
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "PLAYER ONE WINS");

            // kembalikan bola ditengah
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }

        // jika p2 menang (skor max)
        if (player1.Score == maxScore)
        {
            // tampilkan teks "PLAYER TWO WINS" di kiri layar
            GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 10, 2000, 1000), "PLAYER TWO WINS");

            // kembalikan bola ditengah
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }

        // Jika isDebugWindowShown == true, tampilkan text area untuk debug window.
        if (isDebugWindowShown)
        {
            // Simpan nilai warna lama GUI
            Color oldColor = GUI.backgroundColor;

            // Beri warna baru
            GUI.backgroundColor = Color.red;

            // Simpan variabel-variabel fisika yang akan ditampilkan. 
            float ballMass = ballRigidBody.mass;
            Vector2 ballVelocity = ballRigidBody.velocity;
            float ballSpeed = ballRigidBody.velocity.magnitude;
            Vector2 ballMomentum = ballMass * ballVelocity;
            float ballFriction = ballCollider.friction;

            float impulsePlayer1X = player1.LastContactPoint.normalImpulse;
            float impulsePlayer1Y = player1.LastContactPoint.tangentImpulse;
            float impulsePlayer2X = player2.LastContactPoint.normalImpulse;
            float impulsePlayer2Y = player2.LastContactPoint.tangentImpulse;

            // Tentukan debug text-nya
            string debugText =
                "Ball mass = " + ballMass + "\n" +
                "Ball velocity = " + ballVelocity + "\n" +
                "Ball speed = " + ballSpeed + "\n" +
                "Ball momentum = " + ballMomentum + "\n" +
                "Ball friction = " + ballFriction + "\n" +
                "Last impulse from player 1 = (" + impulsePlayer1X + ", " + impulsePlayer1Y + ")\n" +
                "Last impulse from player 2 = (" + impulsePlayer2X + ", " + impulsePlayer2Y + ")\n";

            // Tampilkan debug window
            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
            guiStyle.alignment = TextAnchor.UpperCenter;
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 110), debugText, guiStyle);

            // Kembalikan warna lama GUI
            GUI.backgroundColor = oldColor;
        }

        // Toggle nilai debug window ketika pemain mengeklik tombol ini.
        if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 73, 120, 53), "TOGGLE\nDEBUG INFO"))
        {
            isDebugWindowShown = !isDebugWindowShown;
            trajectory.enabled = !trajectory.enabled;
        }
    }
}
