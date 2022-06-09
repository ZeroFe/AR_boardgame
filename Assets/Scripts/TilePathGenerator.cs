using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePathGenerator : MonoBehaviour
{
    public GameObject[] rows;
    private Tile[,] tiles;

    private int m;
    private int n;

    void Start()
    {
        SetTiles();
        GenerateTilePath();
    }

    private void SetTiles()
    {
        m = rows.Length;
        n = rows[0].transform.childCount;
        tiles = new Tile[m, n];

        for (int r = 0; r < m; r++)
        {
            for (int c = 0; c < n; c++)
            {
                tiles[r, c] = rows[r].transform.GetChild(c).GetComponent<Tile>();
            }
        }
    }
    
    /// <summary>
    /// 나선형으로 타일 경로를 만듦
    /// </summary>
    private void GenerateTilePath()
    {
        // Set Tile Next Path
        Vector2Int pos = new Vector2Int(0, 0);
        int tileCount = m * n;
        // 지나간 타일은 제외해야함
        bool[,] isMoved = new bool[m, n];

        // Right, Down, Left, Up
        Vector2Int[] dir = new Vector2Int[]
        {
            new Vector2Int(1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(-1, 0),
            new Vector2Int(0, -1),
        };
        // 초기 이동 방향은 Right
        int dirNum = 0;

        while (tileCount > 1)
        {
            // 현재 타일은 지나감
            isMoved[pos.y, pos.x] = true;

            // 이전 타일 저장
            var prev = tiles[pos.y, pos.x];
            tileCount--;

            // 타일 이동
            Vector2Int newPos = pos + dir[dirNum];
            int count = 0;
            // invalid한 타일이나 지나간 타일은 제외
            while (newPos.y < 0 || newPos.y >= m || newPos.x < 0 || newPos.x >= n || isMoved[newPos.y, newPos.x])
            {
                dirNum = (dirNum + 1) % dir.Length;
                newPos = pos + dir[dirNum];
                count++;
                Debug.Assert(count <= 4, $"Tile Direction Setting Error in tileCount : {tileCount}");
                if (count > 4) break;
            }
            pos = newPos;

            // 이전 타일의 다음 타일은 이동한 타일
            prev.NextTile = tiles[pos.y, pos.x];
        }

        // 마지막 타일은 도착지
    }
}
