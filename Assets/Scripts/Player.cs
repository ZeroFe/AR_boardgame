using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 보드 게임 말 정보를 담는 클래스
/// 
/// </summary>
public class Player : MonoBehaviour
{
    public int pos = -1;

    // 전투
    public int hp = 100;
    // 비활성화되는 턴
    public int restore = 0;

    private Tile currentTile;

    [Header("Animation")] 
    [SerializeField] private float jumpHeight = 2.0f;
    [SerializeField] private float waitTime = 0.2f;
    [SerializeField] private float rotateTime = 0.2f;
    [SerializeField] private float moveTime = 0.3f;

    public void Battle(Player other)
    {
        int tmpHp = hp;
        hp -= other.hp;
        other.hp -= tmpHp;

        if (hp < 0)
        {
            
        }
    }

    public void MoveCount(int count)
    {
        StartCoroutine(IEMoveCount(count));
    }

    /// <summary>
    /// 특정 타일로 이동하는 함수
    /// </summary>
    /// <param name="pos"></param>
    public void MoveToTile(int target)
    {
        StartCoroutine(IEMoveToTile(target));
    }

    // 타일 이동 전체 애니메이션
    IEnumerator IEMoveCount(int count)
    {
        int sign = count > 0 ? 1 : -1;
        count = TileManager.Instance.GetMovableCount(pos, count);

        // 이동 애니메이션
        for (int i = 0; i < count; i++)
        {
            yield return IEMoveTile(pos, pos + 1);
            yield return new WaitForSeconds(waitTime);
        }

        // 이동이 끝나면 타일 효과를 적용한다
        TileManager.Instance.GetTile(pos).ApplyTileEffect(this);
    }

    IEnumerator IEMoveToTile(int target)
    {
        yield return IEMoveTile(pos, target);
    }

    // 타일 단위 이동 애니메이션
    IEnumerator IEMoveTile(int start, int end)
    {
        // 회전 처리 대기
        //Quaternion.RotateTowards()

        // 시작점
        var startPos = TileManager.Instance.GetTile(start).transform.position;
        var endPos = TileManager.Instance.GetTile(end).transform.position;

        // 이동 애니메이션 
        for (float t = 0.0f; t < moveTime; t += Time.deltaTime)
        {
            float percent = t / moveTime;
            // 이동 - 단순 평행 이동
            var xzVec3 = Vector3.Lerp(startPos, endPos, t / moveTime);
            // 점프 - 이차함수를 이용한 포물선식 이동
            float maxY = Mathf.Max(startPos.y, endPos.y) + jumpHeight;
            float y = percent < 0.5f ?
                Quad(startPos.y, maxY, percent * 2) :
                Quad(endPos.y, maxY, (1 - percent) * 2);
            transform.position = new Vector3(xzVec3.x, y, xzVec3.z);
            yield return null;
        }
        transform.position = endPos;
        pos = end;
    }

    // 이차함수로 보간
    private float Quad(float a, float b, float t)
    {
        return Mathf.Lerp(a, b, t * t);
    }
}
