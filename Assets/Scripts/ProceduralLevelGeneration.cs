using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralLevelGeneration : MonoBehaviour
{
    public Camera Main_Camera;
    public Tilemap tilemap;
    public Grid grid;
    public TileBase tileGround;
    public TileBase tileCeiling;
    private TileBase[] tilesGround;
    private TileBase[] tilesCeiling;
    private Vector3Int[] tilePositionsAdd;
    private Vector3Int[] tilePositionsAddUp;
    private Vector3Int[] tilePositionsRemove;
    private Vector3Int[] tilePositionsRemoveUp;
    private TileBase[] tile_null;
    private bool movingRight = true;
    private int tileGenerateXOffset = 5;
    private int tileDestroyXOffset = 6;
    private int groundTileY = -2;
    private int ceilingTileY = 2;
    [SerializeField]
    [Range(1, 15)]
    private int tileGenerationCount = 5;
    [SerializeField]
    private bool generateCeiling = false;
    void Start()
    {
        tilePositionsAdd = new Vector3Int[tileGenerationCount];
        if (generateCeiling) tilePositionsAddUp = new Vector3Int[tileGenerationCount];
        tilePositionsRemove = new Vector3Int[tileGenerationCount];
        if (generateCeiling) tilePositionsRemoveUp = new Vector3Int[tileGenerationCount];

        tile_null = new TileBase[tileGenerationCount];
        tilesGround = new TileBase[tileGenerationCount];
        if (generateCeiling) tilesCeiling = new TileBase[tileGenerationCount];

        for (int i = 0; i < tileGenerationCount; i++)
            {
            tilePositionsAdd[i] = new Vector3Int(0, groundTileY, 0);
            if (generateCeiling) tilePositionsAddUp[i] = new Vector3Int(0, ceilingTileY, 0);
            tilePositionsRemove[i] = new Vector3Int(0, groundTileY, 0);
            if (generateCeiling) tilePositionsRemoveUp[i] = new Vector3Int(0, ceilingTileY, 0);
            tile_null[i] = null;
            tilesGround[i] = tileGround;
            if (generateCeiling) tilesCeiling[i] = tileCeiling;
            }
        tilemap.transform.SetParent(grid.transform);
    }

    void Update()
        {
        SetMovingDirection();
        }

    void FixedUpdate()
        {
        DestroyTiles();
        GenerateTiles();
        }

    void SetMovingDirection()
        {
        if (Camera.main.velocity.x > 0) movingRight = true;
        else if (Camera.main.velocity.x < 0) movingRight = false;
        }

    void SetTilePositionsWithStartingPoint(int x)
        {
        if(movingRight)
            {
            for(int i = 0; i < tileGenerationCount; i++)
                {
                tilePositionsAdd[i].x = x + i;
                if (generateCeiling) tilePositionsAddUp[i].x = x + i;
                }
            }
        else
            {
            for (int i = 0; i < tileGenerationCount; i++)
                {
                tilePositionsAdd[i].x = x - i;
                if (generateCeiling) tilePositionsAddUp[i].x = x - i;
                }
            }
        }

        void SetTileSide()
        {
        int roundedCameraPosition = (int)Mathf.Round(Main_Camera.transform.position.x);

        if (movingRight)
            {
            SetTilePositionsWithStartingPoint(roundedCameraPosition/3 + tileGenerateXOffset);
            }
        else
            {
            SetTilePositionsWithStartingPoint(roundedCameraPosition/3 - tileGenerateXOffset);
            }
        }

    void GenerateTiles()
        {
        SetTileSide();
        tilemap.SetTiles(tilePositionsAdd, tilesGround);
        if (generateCeiling) tilemap.SetTiles(tilePositionsAddUp, tilesCeiling);
        }

    void DestroyTiles()
        {
        int roundedCameraPosition = (int)Mathf.Round(Main_Camera.transform.position.x);
        if (movingRight)
            {
            int remove_x = roundedCameraPosition / 3 - tileDestroyXOffset;
            for (int i = 0; i < tileGenerationCount; i++)
                {
                tilePositionsRemove[i].x = remove_x - i;
                if (generateCeiling) tilePositionsRemoveUp[i].x = remove_x - i;
                }
            }
        else
            {
            int remove_x = roundedCameraPosition / 3 + tileDestroyXOffset;
            for (int i = 0; i < tileGenerationCount; i++)
                {
                tilePositionsRemove[i].x = remove_x + i;
                if (generateCeiling) tilePositionsRemoveUp[i].x = remove_x + i;
                }
            }
        tilemap.SetTiles(tilePositionsRemove, tile_null);
        if (generateCeiling) tilemap.SetTiles(tilePositionsRemoveUp, tile_null);
        }
}
