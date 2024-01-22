using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform player1;
    [SerializeField] Transform player2;
    [SerializeField] private GameObject ball;
    [SerializeField] public Transform respawnReferenceObject;
    [SerializeField] public float respawnOffsetY = 0f;

    [SerializeField] TextMeshProUGUI scoreBlueText;
    [SerializeField] TextMeshProUGUI blueVictoryText;
    [SerializeField] TextMeshProUGUI scoreOrangeText;
    [SerializeField] TextMeshProUGUI orangeVictoryText;
    [SerializeField] private int scoreBlue = 0;
    [SerializeField] private int scoreOrange = 0;
    [SerializeField] private float waitBeforeRemake = 1f;
    [SerializeField] public int winScore = 2;
    [SerializeField] private bool canGoal = true;

    private void Update()
    {
        if (!canGoal && scoreOrange == winScore)
        {
            orangeVictoryText.gameObject.SetActive(true);
            StartCoroutine(ActivateOrangeVictoryTextWithDelay(waitBeforeRemake, orangeVictoryText));
        }
        
        if (!canGoal && scoreBlue == winScore)
        {
            blueVictoryText.gameObject.SetActive(true);
            StartCoroutine(ActivateBlueVictoryTextWithDelay(waitBeforeRemake, blueVictoryText));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (canGoal)
        {
            if (other.CompareTag("GoalOrange"))
            {
                scoreBlue++;
                UpdateScoreText();
                Debug.Log("Goal for team Blue ! Score: " + scoreBlue);

                if (scoreBlue < winScore)
                {
                    RepositionPlayers();
                    RespawnBall();
                }

                if (scoreBlue == winScore)
                {
                    canGoal = false;
                }
            }

            if (other.CompareTag("GoalBlue"))
            {
                scoreOrange++;
                UpdateScoreText();
                Debug.Log("Goal for team Orange ! Score: " + scoreOrange);

                if (scoreOrange < winScore)
                {
                    RepositionPlayers();
                    RespawnBall();
                }

                if (scoreOrange == winScore)
                {
                    canGoal = false;
                }
            }
        }
    }

    // Fonction pour replacer les joueurs
    private void RepositionPlayers()
    {
        Vector3 player1Position = new Vector3((float)3.52999997, 1, (float)-1.00999999);
        Vector3 player2Position = new Vector3((float)-18.0799999, 1, -1);

        Quaternion player1Rotation = Quaternion.Euler(0, (float)270.80, 0);
        Quaternion player2Rotation = Quaternion.Euler(0, (float)89.384, 0);

        // Appliquez les nouvelles positions aux joueurs
        player1.position = player1Position;
        player2.position = player2Position;

        player1.rotation = player1Rotation;
        player2.rotation = player2Rotation;
    }
    private void RespawnBall()
    {
        Vector3 respawnPosition = new Vector3(
            respawnReferenceObject.position.x,
            respawnReferenceObject.position.y + respawnOffsetY,
            respawnReferenceObject.position.z
        );
        //Respawn ball
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
        scoreOrangeText.text = scoreOrange.ToString();
        scoreBlueText.text = scoreBlue.ToString();
    }
    private IEnumerator ActivateOrangeVictoryTextWithDelay(float waitBeforeRemake, TextMeshProUGUI orangeVictoryText)
    {
        yield return new WaitForSeconds(waitBeforeRemake);
        orangeVictoryText.gameObject.SetActive(true);
        RelancerScene();
    }
    private IEnumerator ActivateBlueVictoryTextWithDelay(float waitBeforeRemake, TextMeshProUGUI blueVictoryText)
    {
        yield return new WaitForSeconds(waitBeforeRemake);
        blueVictoryText.gameObject.SetActive(true);
        RelancerScene();
    }

    public void RelancerScene()
    {
        // Rechargez la scène actuelle
        Scene sceneActuelle = SceneManager.GetActiveScene();
        SceneManager.LoadScene(sceneActuelle.name);
    }
}
