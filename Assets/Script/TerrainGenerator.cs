using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int width = 256; // Largeur du terrain
    public int height = 256; // Hauteur du terrain
    public float scale = 20f; // �chelle du bruit de Perlin
    public float offsetX = 100f; // D�calage X du bruit de Perlin
    public float offsetY = 100f; // D�calage Y du bruit de Perlin

    void Start()
    {
        GenerateTerrain(); // Appel de la fonction de g�n�ration du terrain au d�marrage
    }

    void GenerateTerrain()
    {
        Terrain terrain = GetComponent<Terrain>(); // Obtenir la r�f�rence du composant Terrain
        terrain.terrainData = GenerateTerrain(terrain.terrainData); // Appliquer les donn�es g�n�r�es au terrain
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        // D�finir la r�solution du terrain et sa taille
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, 20, height);

        // G�n�rer les hauteurs du terrain
        terrainData.SetHeights(0, 0, GenerateHeights());

        return terrainData; // Retourner les donn�es du terrain
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
        // Calculer les coordonn�es de bruit de Perlin en fonction de la position du point
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        // Utiliser le bruit de Perlin pour d�terminer la hauteur
        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}