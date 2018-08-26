using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid : MonoBehaviour {

    public GameObject MapTileButtonPrefabReference = null;
    public Dictionary<Coordinate, MapTileButton> MapTiles = new Dictionary<Coordinate, MapTileButton>();

    public void AddMapTile(Coordinate coordinate)
    {
        // Instantiate MapTileButton prefab
        GameObject mapTile = Instantiate(MapTileButtonPrefabReference);
        mapTile.name = "Tile_" + (coordinate.x).ToString() + "," + (coordinate.y).ToString();

        // Set Coordinate
        mapTile.GetComponent<MapTileButton>().coordinate = coordinate;

        // Set prefab instance's parent to this transform
        mapTile.transform.SetParent(transform);

        // Adjust rotation and scale
        RectTransform mapTileRectTransform = mapTile.GetComponent<RectTransform>();
        mapTileRectTransform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        mapTileRectTransform.localScale = new Vector3(1f, 1f, 1f);

        MapTiles.Add(coordinate, mapTile.GetComponent<MapTileButton>());
    }
}
