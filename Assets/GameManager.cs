using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform player1;
    [SerializeField] Transform player2;
    [SerializeField] public GameObject ball;
    [SerializeField] public Transform respawnReferenceObject;
    [SerializeField] public float respawnOffsetY = 0f;

    [SerializeField] TextMeshProUGUI scoreBlueText;
    [SerializeField] TextMeshProUGUI scoreRedText;
    [SerializeField] private int scoreBlue = 0;
    [SerializeField] private int scoreRed = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GoalRed"))
        {
            scoreBlue++;
            UpdateScoreText();
            Debug.Log("Goal for team Blue ! Score: " + scoreBlue);
            RespawnBall();
            RepositionPlayers();
        }

        if (other.CompareTag("GoalBlue"))
        {
            scoreRed++;
            UpdateScoreText();
            Debug.Log("Goal for team Red ! Score: " + scoreRed);
            RespawnBall();
            RepositionPlayers();
        }
    }

    // Fonction pour replacer les joueurs
    private void RepositionPlayers()
    {
        Vector3 player1Position = new Vector3(-5f, 0f, 0f);
        Vector3 player2Position = new Vector3(5f, 0f, 0f);

        // Appliquez les nouvelles positions aux joueurs
        player1.position = player1Position;
        player2.position = player2Position;
    }
    private void RespawnBall()
    {
        Vector3 respawnPosition = new Vector3(
            respawnReferenceObject.position.x,
            respawnReferenceObject.position.y + respawnOffsetY,
            respawnReferenceObject.position.z 
        );

        // Respawn ball
        ball.transform.position = respawnPosition;

        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
        if (ballRigidbody != null)
        {
            ballRigidbody.velocity = Vector3.zero;
            ballRigidbody.angularVelocity = Vector3.zero;
        }
        ball.transform.rotation = Quaternion.identity;
    }
    private void UpdateScoreText()
    {
        // Mettre à jour le texte du score sur le canvas
        scoreRedText.text = scoreRed.ToString();
        scoreBlueText.text = scoreBlue.ToString();
    }
}