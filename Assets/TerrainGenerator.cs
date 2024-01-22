using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int width = 256; // Largeur du terrain
    public int height = 256; // Hauteur du terrain
    public float scale = 20f; // Échelle du bruit de Perlin
    public float offsetX = 100f; // Décalage X du bruit de Perlin
    public float offsetY = 100f; // Décalage Y du bruit de Perlin

    void Start()
    {
        GenerateTerrain(); // Appel de la fonction de génération du terrain au démarrage
    }

    void GenerateTerrain()
    {
        Terrain terrain = GetComponent<Terrain>(); // Obtenir la référence du composant Terrain
        terrain.terrainData = GenerateTerrain(terrain.terrainData); // Appliquer les données générées au terrain
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        // Définir la résolution du terrain et sa taille
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, 20, height);

        // Générer les hauteurs du terrain
        terrainData.SetHeights(0, 0, GenerateHeights());

        return terrainData; // Retourner les données du terrain
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height]; // Tableau pour stocker les hauteurs du terrain

        // Parcourir chaque point du terrain et calculer la hauteur
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y); // Appeler la fonction pour calculer la hauteur
            }
        }

        return heights; // Retourner le tableau de hauteurs
    }

    float CalculateHeight(int x, int y)
    {
        // Calculer les coordonnées de bruit de Perlin en fonction de la position du point
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        // Utiliser le bruit de Perlin pour déterminer la hauteur
        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}