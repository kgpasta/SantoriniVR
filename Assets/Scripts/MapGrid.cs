using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MapGrid : MonoBehaviour {

    public GameObject MapTileButtonPrefabReference = null;
    public Dictionary<Coordinate, MapTileButton> MapTiles = new Dictionary<Coordinate, MapTileButton>();

    #region Event Definition

    public class MapGridEventArgs : EventArgs
    {
        private Coordinate m_Coordinate = null;
        public Coordinate coordinate
        {
            get { return m_Coordinate; }
            set { m_Coordinate = value; }
        }

        public MapGridEventArgs(Coordinate coordinate)
        {
            m_Coordinate = coordinate;
        }

    }

    public event EventHandler<MapGridEventArgs> OnMapTileButtonPressed;
    private void RaiseOnMapTileButtonPressed(Coordinate coordinate)
    {
        if (coordinate != null && OnMapTileButtonPressed != null)
        {
            OnMapTileButtonPressed(this, new MapGridEventArgs(coordinate));
        }
    }

    #endregion

    public void AddMapTile(Coordinate coordinate)
    {
        // Instantiate MapTileButton prefab
        GameObject mapTile = Instantiate(MapTileButtonPrefabReference);
        mapTile.name = "Tile_" + (coordinate.x).ToString() + "," + (coordinate.y).ToString();

        // Set Coordinate
        mapTile.GetComponent<MapTileButton>().coordinate = coordinate;

        // Set prefab instance's parent to this transform
        mapTile.transform.SetParent(transform);

        // Set Button On Click Listener
        mapTile.GetComponent<Button>().onClick.AddListener(delegate { MapTileButtonOnClick(mapTile.GetComponent<MapTileButton>().coordinate); });

        // Adjust rotation and scale
        RectTransform mapTileRectTransform = mapTile.GetComponent<RectTransform>();
        mapTileRectTransform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        mapTileRectTransform.localScale = new Vector3(1f, 1f, 1f);

        MapTiles.Add(coordinate, mapTile.GetComponent<MapTileButton>());
    }

    private void MapTileButtonOnClick(Coordinate coordinate)
    {
        RaiseOnMapTileButtonPressed(coordinate);
    }
}
