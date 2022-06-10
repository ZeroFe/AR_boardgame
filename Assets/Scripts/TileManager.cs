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
    /// 나선형으로 타일 경로를 만듦
    /// </summary>
    private void GenerateTilePath()
    {
        Tile[,] map = new Tile[rowSize, colSize];

        // 초기화
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
        // 지나간 타일은 제외해야함
        bool[,] isMoved = new bool[rowSize, colSize];

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

        while (currIndex < tiles.Capacity - 1)
        {
            // 현재 타일은 지나감
            isMoved[pos.y, pos.x] = true;

            // 이전 타일 저장
            tiles.Add(map[pos.y, pos.x]);
            var prev = map[pos.y, pos.x];

            // 타일 이동
            Vector2Int newPos = pos + dir[dirNum];
            int count = 0;
            // invalid한 타일이나 지나간 타일은 제외
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

            // 이전 타일의 다음 타일은 이동한 타일
            //prev.NextTile = map[pos.y, pos.x];
        }

        // 마지막 타일은 도착지
        tiles.Add(map[pos.y, pos.x]);
    }


    #endregion

    /// <summary>
    /// 인덱스를 기준으로 이동할 수 있는 타일의 개수를 리턴한다
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
    /// index에 해당하는 Tile을 가져온다
    /// index가 음수인 경우 첫 타일을, index가 마지막 타일을 넘어가는 경우 마지막 타일을 리턴한다
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Tile GetTile(int index)
    {
        return tiles[Mathf.Clamp(index, 0, tiles.Count - 1)];
    }

}
