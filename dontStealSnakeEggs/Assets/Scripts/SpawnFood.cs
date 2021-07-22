using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    // Food prefab
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private Transform foodFolder;

    // Borders
    [SerializeField] private Transform borderTop;
    [SerializeField] private Transform borderBottom;
    [SerializeField] private Transform borderLeft;
    [SerializeField] private Transform borderRight;

    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating("Spawn", 3, 4);
    }

    /// <summary>
    /// Spawns a piece of food within the borders (see line 10 of this script)
    /// </summary>
    private void Spawn()
    {
        int x = (int)Random.Range(borderLeft.position.x, borderRight.position.x);
        int y = (int)Random.Range(borderBottom.position.y, borderTop.position.y);

        GameObject newFood = Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity);
        newFood.transform.SetParent(foodFolder);
    }
}
