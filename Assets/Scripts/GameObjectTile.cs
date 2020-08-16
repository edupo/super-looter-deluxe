using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Game Object Tile", menuName = "2D/Tiles/GameObject Tile", order = 80)]
public class GameObjectTile : TileBase
{
    public GameObject gameObject;
    public Sprite sprite;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
        tileData.sprite = sprite;
        tileData.gameObject = gameObject;
        tileData.color = Color.white;
        tileData.colliderType = Tile.ColliderType.None;
        tileData.flags = TileFlags.InstantiateGameObjectRuntimeOnly;
    }
}
