using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject WallPrefab;
    public GameObject[] DecorationPrefabs;

    void Start()
    {
        var tilePositions = new HashSet<(int, int)>();
        var maxX = int.MinValue;
        var minX = int.MaxValue;
        var maxZ = int.MinValue;
        var minZ = int.MaxValue;
        foreach (var tile in FindObjectsByType<TileScript>(FindObjectsSortMode.None))
        {
            var position = tile.transform.position;

            var x = (int)position.x;
            var z = (int)position.z;

            if (Random.Range(0f, 1f) < 0.2f)
            {
                var go = Instantiate(
                    DecorationPrefabs[Random.Range(0, DecorationPrefabs.Length)],
                    position + new Vector3(Random.Range(-0.4f, 0.4f), 0.5f, Random.Range(-0.4f, 0.4f)),
                    Quaternion.identity
                );

                var scale = Random.Range(0.5f, 2f);
                go.transform.localScale = new Vector3(1, 1, 1) * scale;
            }

            if (x > maxX)
            {
                maxX = x;
            }
            if (x < minX)
            {
                minX = x;
            }
            if (z > maxZ)
            {
                maxZ = z;
            }
            if (z < minZ)
            {
                minZ = z;
            }

            Debug.Log($"found tile ({x}, {z})");
            tilePositions.Add((x, z));
        }

        Debug.Log($"tilePositions = {tilePositions.Count}");

        for (var x = minX - 1; x <= maxX + 1; x++)
            for (var z = minZ - 1; z <= maxZ + 1; z++)
            {
                if (tilePositions.Contains((x, z)))
                {
                    continue;
                }

                Debug.Log($"spawn = ({x}, {z})");
                Instantiate(WallPrefab, new Vector3(x, 0, z), Quaternion.identity);
            }
    }

}
