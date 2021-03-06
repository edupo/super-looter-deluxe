﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public bool generateOnStart = false;
    public bool randomOffset = false;

    [Header("Noise")]
    [Range(1f,20f)]
    public float noiseSize = 1f;
    public Vector2 noiseOffset;

    [Header("Map")]
    public Vector2Int mapSize;
    [Range(1, 20)]
    public int worldBorder = 10;
    public int playerClearance = 10;

    [Header("Thresholds")]
    [Range(0f, 1f)]
    public float wallThreshold = .5f;
    [Range(0f, 1f)]
    public float grassThreshold = .8f;
    [Range(0f, 1f)]
    public float treeThreshold = .8f;
    [Range(0f, 1f)]
    public float treeChance = .1f;
    [Range(0f, 1f)]
    public float waterThreshold = .8f;
    [Range(0f, 1f)]
    public float chestThreshold = .1f;
    [Range(0f, 1f)]
    public float itemChance = .1f;
    [Range(0f, 1f)]
    public float thiefChance = .01f;
    [Range(0f, 1f)]
    public float timeChance = .05f;
    [Header("References")]
    public Tilemap wallTileMap;
    public Tilemap floorTileMap;
    public Tilemap objectTileMap;
    [Header("Tiles")]
    public TileBase wallTile;
    public TileBase floorTile;
    public TileBase grassTile;
    public TileBase treeTile;
    public TileBase waterTile;
    public TileBase chestTile;
    public TileBase objectTile;
    public TileBase thiefTile;
    public TileBase timeTile;
    [Header("Events")]
    public GlobalEvent generated;

    public void Clear()
    {
        wallTileMap.ClearAllTiles();
        floorTileMap.ClearAllTiles();
        objectTileMap.ClearAllTiles();
    }

    public void Start()
    {
        if (generateOnStart)
        {
            Generate();
            generated.Raise();
        }
        else
        {
            generated.Raise();
        }
    }

    public void Generate()
    {
        Clear();

        if (randomOffset)
            noiseOffset = new Vector2(Random.Range(0f, 9999999f), Random.Range(0f, 9999999f));

        Vector3Int[] positions = new Vector3Int[mapSize.x * mapSize.y];
        TileBase[] wallTiles = new TileBase[positions.Length];
        TileBase[] floorTiles = new TileBase[positions.Length];
        TileBase[] objectTiles = new TileBase[positions.Length];

        Vector2 origin = (Vector2)mapSize / 2f;

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                int i = x + (y * mapSize.x);
                var coord = new Vector3Int(x, y, 0);
                positions[i] = coord;

                float xCoord = (x + noiseOffset.x) / noiseSize;
                float yCoord = (y + noiseOffset.y) / noiseSize;

                float height = Mathf.PerlinNoise(xCoord, yCoord);
                float humidity = Mathf.PerlinNoise(xCoord + mapSize.x, yCoord + mapSize.y);

                float d = Vector2.Distance(origin, (Vector3)coord);

                floorTiles[i] = floorTile;
                if (d < playerClearance) // Skip player clearange
                {
                }
                else if (x < worldBorder || y < worldBorder || x > mapSize.x - worldBorder || y > mapSize.y - worldBorder || height > wallThreshold)
                {
                    wallTiles[i] = wallTile;
                }
                else if (humidity > waterThreshold) // Too humid == water
                {
                    wallTiles[i] = waterTile;
                }
                else if (humidity > treeThreshold && Random.value < treeChance)
                {
                    wallTiles[i] = treeTile;
                }
                else if (Random.value < chestThreshold)
                {
                    objectTiles[i] = chestTile;
                }
                else if (humidity > grassThreshold) // Humid == grass
                {
                    floorTiles[i] = grassTile;
                    if (Random.value < itemChance)
                        objectTiles[i] = objectTile;
                }
                else
                {
                    if (Random.value < chestThreshold)
                        objectTiles[i] = chestTile;
                    else if (Random.value < itemChance)
                        objectTiles[i] = objectTile;
                    else if (Random.value < thiefChance)
                        objectTiles[i] = thiefTile;
                    else if (Random.value < timeChance)
                        objectTiles[i] = timeTile;
                }

            }
        }

        wallTileMap.SetTiles(positions, wallTiles);
        floorTileMap.SetTiles(positions, floorTiles);
        objectTileMap.SetTiles(positions, objectTiles);

        transform.position = -(Vector2)mapSize / 2f;

    }

}
