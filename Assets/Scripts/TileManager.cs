using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance { get; private set; }

    public GameObject[] rows;

    private List<Tile> tiles = new List<Tile>();

    private int rowSize;
    private int colSize;

    #region Init
    private void Awake()
    {
        Instance = this;

        Debug.Assert(rows.Length > 0, "There is no tile row");
    }

    void Start()
    {
        Setting();
        GenerateTilePath();
    }

    private void Setting()
    {
        rowSize = rows.Length;
        colSize = rows[0].transform.childCount;
        tiles.Capacity = rowSize * colSize;
    }

    /// <summary>
    /// ���������� Ÿ�� ��θ� ����
    /// </summary>
    private void GenerateTilePath()
    {
        Tile[,] map = new Tile[rowSize, colSize];

        // �ʱ�ȭ
        for (int r = 0; r < rowSize; r++)
        {
            for (int c = 0; c < colSize; c++)
            {
                map[r, c] = rows[r].transform.GetChild(c).GetComponent<Tile>();
            }
        }

        // Set Tile Next Path
        Vector2Int pos = new Vector2Int(0, 0);
        int currIndex = 0;
        // ������ Ÿ���� �����ؾ���
        bool[,] isMoved = new bool[rowSize, colSize];

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

        while (currIndex < tiles.Capacity - 1)
        {
            // ���� Ÿ���� ������
            isMoved[pos.y, pos.x] = true;

            // ���� Ÿ�� ����
            tiles.Add(map[pos.y, pos.x]);
            var prev = map[pos.y, pos.x];

            // Ÿ�� �̵�
            Vector2Int newPos = pos + dir[dirNum];
            int count = 0;
            // invalid�� Ÿ���̳� ������ Ÿ���� ����
            while (newPos.y < 0 || newPos.y >= rowSize || newPos.x < 0 || newPos.x >= colSize || isMoved[newPos.y, newPos.x])
            {
                dirNum = (dirNum + 1) % dir.Length;
                newPos = pos + dir[dirNum];
                count++;
                Debug.Assert(count <= dir.Length, $"Tile Direction Setting Error in tileCount : {currIndex}");
                if (count > dir.Length) break;
            }
            pos = newPos;
            currIndex++;

            // ���� Ÿ���� ���� Ÿ���� �̵��� Ÿ��
            //prev.NextTile = map[pos.y, pos.x];
        }

        // ������ Ÿ���� ������
        tiles.Add(map[pos.y, pos.x]);
    }


    #endregion

    /// <summary>
    /// �ε����� �������� �̵��� �� �ִ� Ÿ���� ������ �����Ѵ�
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int GetMovableCount(int index, int count)
    {
        if (count > 0)
        {
            return (index + count > tiles.Count - 1) ? (tiles.Count - 1) - index : count;
        }
        else
        {
            return index + count < 0 ? -index : -count;
        }
    }

    /// <summary>
    /// index�� �ش��ϴ� Tile�� �����´�
    /// index�� ������ ��� ù Ÿ����, index�� ������ Ÿ���� �Ѿ�� ��� ������ Ÿ���� �����Ѵ�
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Tile GetTile(int index)
    {
        return tiles[Mathf.Clamp(index, 0, tiles.Count - 1)];
    }

}
