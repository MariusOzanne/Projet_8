using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public GameObject objectPrefab; // Préfabriqué de l'objet que tu veux générer
    public int numberOfObjects = 10; // Nombre d'objets à générer
    public float radius = 10f; // Rayon de la zone de génération
    public float minDistance = 2f; // Distance minimale entre deux objets
    public float maxDistance = 23f; // Distance maximale entre deux objets
    public float minScale = 0.3f; // Taille minimale des objets générés
    public float maxScale = 3f; // Taille maximale des objets générés

    void Start()
    {
        StartCoroutine(GenerateObjectsWithDelay()); // Appel de la fonction de génération des objets avec délai au démarrage
    }

    System.Collections.IEnumerator GenerateObjectsWithDelay()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            // Générer un angle aléatoire en radians
            float angle = Random.Range(0f, Mathf.PI * 2);

            // Calculer les coordonnées polaires autour de l'objet central
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            // Appliquer les coordonnées à la position de l'objet central
            Vector3 randomPosition = transform.position + new Vector3(x, 0f, z);

            // Vérifier la distance minimale et maximale avec les objets existants
            if (CheckMinMaxDistance(randomPosition))
            {
                Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
                float randomScale = Random.Range(minScale, maxScale);

                GameObject newObject = Instantiate(objectPrefab, randomPosition, randomRotation); // Instancier l'objet
                newObject.transform.localScale = new Vector3(randomScale, randomScale, randomScale); // Ajuster l'échelle de l'objet

                yield return new WaitForSeconds(0.1f); // Ajouter un délai entre chaque génération
            }
        }
    }

    bool CheckMinMaxDistance(Vector3 position)
    {
        // Récupérer tous les objets dans la scène du type objectPrefab
        GameObject[] existingObjects = GameObject.FindGameObjectsWithTag(objectPrefab.tag);

        foreach (GameObject obj in existingObjects)
        {
            // Vérifier la distance entre le nouvel objet et les objets existants
            float distance = Vector3.Distance(position, obj.transform.position);

            if (distance < minDistance || distance > maxDistance)
            {
                return false; // La distance minimale ou maximale n'est pas respectée, ne pas générer l'objet
            }
        }

        return true; // La distance minimale et maximale est respectée, générer l'objet
    }
}