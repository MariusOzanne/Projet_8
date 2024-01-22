using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public GameObject objectPrefab; // Pr�fabriqu� de l'objet que tu veux g�n�rer
    public int numberOfObjects = 10; // Nombre d'objets � g�n�rer
    public float radius = 10f; // Rayon de la zone de g�n�ration
    public float minDistance = 2f; // Distance minimale entre deux objets
    public float maxDistance = 23f; // Distance maximale entre deux objets
    public float minScale = 0.3f; // Taille minimale des objets g�n�r�s
    public float maxScale = 3f; // Taille maximale des objets g�n�r�s

    void Start()
    {
        StartCoroutine(GenerateObjectsWithDelay()); // Appel de la fonction de g�n�ration des objets avec d�lai au d�marrage
    }

    System.Collections.IEnumerator GenerateObjectsWithDelay()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            // G�n�rer un angle al�atoire en radians
            float angle = Random.Range(0f, Mathf.PI * 2);

            // Calculer les coordonn�es polaires autour de l'objet central
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            // Appliquer les coordonn�es � la position de l'objet central
            Vector3 randomPosition = transform.position + new Vector3(x, 0f, z);

            // V�rifier la distance minimale et maximale avec les objets existants
            if (CheckMinMaxDistance(randomPosition))
            {
                Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
                float randomScale = Random.Range(minScale, maxScale);

                GameObject newObject = Instantiate(objectPrefab, randomPosition, randomRotation); // Instancier l'objet
                newObject.transform.localScale = new Vector3(randomScale, randomScale, randomScale); // Ajuster l'�chelle de l'objet

                yield return new WaitForSeconds(0.1f); // Ajouter un d�lai entre chaque g�n�ration
            }
        }
    }

    bool CheckMinMaxDistance(Vector3 position)
    {
        // R�cup�rer tous les objets dans la sc�ne du type objectPrefab
        GameObject[] existingObjects = GameObject.FindGameObjectsWithTag(objectPrefab.tag);

        foreach (GameObject obj in existingObjects)
        {
            // V�rifier la distance entre le nouvel objet et les objets existants
            float distance = Vector3.Distance(position, obj.transform.position);

            if (distance < minDistance || distance > maxDistance)
            {
                return false; // La distance minimale ou maximale n'est pas respect�e, ne pas g�n�rer l'objet
            }
        }

        return true; // La distance minimale et maximale est respect�e, g�n�rer l'objet
    }
}