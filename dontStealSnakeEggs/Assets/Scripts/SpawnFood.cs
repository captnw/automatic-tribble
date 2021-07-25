using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnFood : MonoBehaviour
{
    // Food prefab
    [SerializeField] private GameObject m_foodPrefab;
    private SpriteRenderer m_foodSR;
    [SerializeField] private Transform m_foodFolder;

    // Borders
    [SerializeField] private Transform m_boundary;
    private Bounds m_polygonBoundary;
    private float m_minX, m_maxX, m_minY, m_maxY;

    // Tilemap ref
    [SerializeField] private GameObject m_wallTiles;
    private GridLayout m_wallGrid;
    private Tilemap m_wallTilemap;

    // Start is called before the first frame update
    private void Start()
    {
        m_wallGrid = m_wallTiles.GetComponent<GridLayout>();
        m_wallTilemap = m_wallTiles.GetComponent<Tilemap>();

        m_foodSR = m_foodPrefab.GetComponent<SpriteRenderer>();
        m_polygonBoundary = m_boundary.GetComponent<PolygonCollider2D>().bounds;

        m_minX = m_polygonBoundary.min.x + m_foodSR.sprite.rect.width / 2;
        m_maxX = m_polygonBoundary.max.x - m_foodSR.sprite.rect.width / 2;
        m_minY = m_polygonBoundary.min.y + m_foodSR.sprite.rect.height / 2;
        m_maxY = m_polygonBoundary.max.y - m_foodSR.sprite.rect.height / 2;

        InvokeRepeating("Spawn", 3, 4);
    }

    /// <summary>
    /// Spawns a piece of food within the borders (see line 10 of this script)
    /// </summary>
    private void Spawn()
    {
        while (true)
        {
            Vector2 ranPoint = RandomPointInBounds(m_polygonBoundary);

            TileBase t = m_wallTilemap.GetTile(m_wallGrid.WorldToCell(ranPoint));
            if (t)
            {
                // cannot spawn food in the walls, continue
                print("blocked");
                continue;
            }
            break;
        }

        GameObject newFood = Instantiate(m_foodPrefab, RandomPointInBounds(m_polygonBoundary), Quaternion.identity);
        newFood.transform.SetParent(m_foodFolder);
    }

    private Vector2 RandomPointInBounds(Bounds bounds)
    {
        return new Vector2(
            Random.Range(m_minX, m_maxX),
            Random.Range(m_minY, m_maxY)
        );
    }

}
