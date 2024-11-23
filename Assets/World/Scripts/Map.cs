using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] Vector2 waterPerTurn = new Vector2(0f, 1f);
    [SerializeField] Vector2 sunValues = new Vector2(1f, 2f);

    private Grid grid;
    [SerializeField] Tilemap tilledTilemap;
    [SerializeField] Tilemap groundTilemap;
    [SerializeField] Tilemap hillTilemap;
    
    public Dictionary<Vector3Int, Cell> TilledCells;
    public Dictionary<Vector3Int, Cell> GroundCells;

    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponent<Grid>();
        InitalizeCells();

        TurnManager.NewTurn += GiveCellResources;
    }

    // Assign cell dictionaries based on tilemaps
    private void InitalizeCells()
    {
        GroundCells = new Dictionary<Vector3Int, Cell>();
        TilledCells = new Dictionary<Vector3Int, Cell>();

        HashSet<Vector3Int> tilledTiles = GetAllTiles(tilledTilemap);
        HashSet<Vector3Int> groundTiles = GetAllTiles(groundTilemap);
        HashSet<Vector3Int> hillTiles = GetAllTiles(hillTilemap);

        foreach (Vector3Int tile in groundTiles)
        {
            if (hillTiles.Contains(tile))
            {
                continue;
            }

            if (tilledTiles.Contains(tile))
            {
                TilledCells.Add(tile, null);
            }

            GroundCells.Add(tile, new Cell());
        }
    }

    public void GiveCellResources()
    {
        foreach (Cell cell in GroundCells.Values)
        {
            cell.AddWater(Random.Range(waterPerTurn.x, waterPerTurn.y));
            cell.SetSun(Random.Range(sunValues.x, sunValues.y));
        }
    }

    public Cell GetCell(Vector3Int coord)
    {
        return GroundCells[coord];
    }

    public Cell GetCell(Vector3 position)
    {
        return GroundCells[PosToCellCoord(position)];
    }

    public Vector3Int PosToCellCoord(Vector3 position)
    {
        return grid.WorldToCell(position);
    }

    public Vector3 CellCoordToPos(Vector3Int coord)
    {
        return grid.CellToWorld(coord) + new Vector3(0.5f, 0.5f);
    }

    public bool IsGroundCell(Vector3Int coord)
    {
        return GroundCells.ContainsKey(coord);
    }

    public bool CreateTile(Vector3Int coord)
    {
        if (!IsGroundCell(coord))
        {
            return false;
        }

        return true;
    }

    private HashSet<Vector3Int> GetAllTiles(Tilemap tilemap)
    {
        if (tilemap == null)
        {
            return new HashSet<Vector3Int>();
        }

        HashSet<Vector3Int> tiles = new HashSet<Vector3Int>();

        BoundsInt bounds = tilemap.cellBounds;

        foreach (Vector3Int coord in bounds.allPositionsWithin)
        {
            if (tilemap.HasTile(coord))
            {
                tiles.Add(coord);
            }
        }

        return tiles;
    }
}

public class Cell
{
    private float waterLevel;
    private float sunLevel = 0;

    public float GetWater()
    {
        return waterLevel;
    }

    public float GetSun()
    {
        return sunLevel;
    }

    public void AddWater(float value)
    {
        waterLevel += value;
    }

    public void SetSun(float value)
    {
        sunLevel = value;
    }

    public override string ToString()
    {
        return string.Format("Water: {0}\nSun: {1}", waterLevel, sunLevel);
    }
}
