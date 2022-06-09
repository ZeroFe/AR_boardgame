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
    /// ���������� Ÿ�� ��θ� ����
    /// </summary>
    private void GenerateTilePath()
    {
        // Set Tile Next Path
        Vector2Int pos = new Vector2Int(0, 0);
        int tileCount = m * n;
        // ������ Ÿ���� �����ؾ���
        bool[,] isMoved = new bool[m, n];

        // Right, Down, Left, Up
        Vector2Int[] dir = new Vector2Int[]
        {
            new Vector2Int(1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(-1, 0),
            new Vector2Int(0, -1),
        };
        // �ʱ� �̵� ������ Right
        int dirNum = 0;

        while (tileCount > 1)
        {
            // ���� Ÿ���� ������
            isMoved[pos.y, pos.x] = true;

            // ���� Ÿ�� ����
            var prev = tiles[pos.y, pos.x];
            tileCount--;

            // Ÿ�� �̵�
            Vector2Int newPos = pos + dir[dirNum];
            int count = 0;
            // invalid�� Ÿ���̳� ������ Ÿ���� ����
            while (newPos.y < 0 || newPos.y >= m || newPos.x < 0 || newPos.x >= n || isMoved[newPos.y, newPos.x])
            {
                dirNum = (dirNum + 1) % dir.Length;
                newPos = pos + dir[dirNum];
                count++;
                Debug.Assert(count <= 4, $"Tile Direction Setting Error in tileCount : {tileCount}");
                if (count > 4) break;
            }
            pos = newPos;

            // ���� Ÿ���� ���� Ÿ���� �̵��� Ÿ��
            prev.NextTile = tiles[pos.y, pos.x];
        }

        // ������ Ÿ���� ������
    }
}
