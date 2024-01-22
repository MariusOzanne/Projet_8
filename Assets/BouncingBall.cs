using UnityEngine;

public class BouncingBall : MonoBehaviour
{
    public float vitesse = 5f; // Vitesse initiale de la balle
    private Vector3 direction; // Direction de mouvement de la balle
    private float restitutionInitiale; // Restitution initiale de la balle
    public float tauxDecroissance = 0.1f; // Taux de décroissance de la restitution

    void Start()
    {
        // Initialisation de la direction avec une valeur aléatoire
        direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        // Appliquer la vitesse initiale à la balle
        GetComponent<Rigidbody>().velocity = direction * vitesse;

        // Initialisation de la restitution initiale
        restitutionInitiale = GetComponent<Collider>().material.bounciness;
    }

    void Update()
    {
        // Rien à faire dans cette méthode pour le moment
    }

    void OnCollisionEnter(Collision collision)
    {
        // Réduire la restitution avec chaque collision
        GetComponent<Collider>().material.bounciness = Mathf.Lerp(restitutionInitiale, 0f, Time.time * tauxDecroissance);

        // Si la balle entre en collision avec quelque chose, ajuste la direction
        direction = Vector3.Reflect(direction, collision.contacts[0].normal);

        // Applique la nouvelle direction à la balle
        GetComponent<Rigidbody>().velocity = direction * vitesse;
    }
}