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
    public int rest = 0;

    public Transform inTilePos;

    [Header("Animation")] 
    [SerializeField] private float jumpHeight = 2.0f;
    [SerializeField] private float waitTime = 0.2f;
    [SerializeField] private float moveTime = 0.3f;

    private void Start()
    {
        inTilePos = transform.Find("inTilePos").transform;
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        print($"Take Damage : {amount}");
        // 죽음 처리
        if (hp <= 0)
        {
            hp = 0;
            rest = 1;
            // 체력 변화 알리기
        }
    }

    #region 이동 관련 함수
    public void MoveCount(int count)
    {
        StartCoroutine(IEMoveCount(count));
    }

    /// <summary>
    /// 특정 타일로 이동
    /// </summary>
    /// <param name="target"></param>
    public void MoveToTile(int target)
    {
        StartCoroutine(IEJump(target));
    }

    // 타일 이동 전체 애니메이션
    IEnumerator IEMoveCount(int count)
    {
        print($"IE Move Count : {count}");
        int sign = count > 0 ? 1 : -1;
        count = TileManager.Instance.GetMovableCount(pos, count);

        // 이동 애니메이션
        for (int i = 0; i < count; i++)
        {
            yield return IEMoveTile(pos, pos + sign);
            yield return new WaitForSeconds(waitTime);
        }

        // 이동이 끝나면 타일 효과를 적용한다
        TileManager.Instance.GetTile(pos).ApplyTileEffect(this);
    }

    // 특정 타일로 바로 이동하는 애니메이션
    IEnumerator IEJump(int target)
    {
        yield return IEMoveTile(pos, target);

        // 이동이 끝나면 타일 효과를 적용한다
        TileManager.Instance.GetTile(pos).ApplyTileEffect(this);
    }

    // 타일 단위 이동 애니메이션
    IEnumerator IEMoveTile(int start, int end)
    {
        // 시작점
        var startPos = TileManager.Instance.GetTile(start).transform.position;
        var endPos = TileManager.Instance.GetTile(end).transform.position;

        // 회전 처리
        var towardVec3 = (endPos - startPos).normalized;
        transform.rotation = Quaternion.LookRotation(towardVec3);

        // 이동 애니메이션 
        for (float t = 0.0f; t < moveTime; t += Time.deltaTime)
        {
            float percent = t / moveTime;
            // 이동 - 단순 평행 이동
            var xzVec3 = Vector3.Lerp(startPos, endPos, t / moveTime);
            // 점프 - 이차함수를 이용한 포물선식 이동
            float maxY = Mathf.Max(startPos.y, endPos.y) + jumpHeight * GameSystem.Instance.GameScale;
            float y = percent < 0.5f ?
                Quad(startPos.y, maxY, percent * 2) :
                Quad(endPos.y, maxY, (1 - percent) * 2);
            transform.position = new Vector3(xzVec3.x, y, xzVec3.z);
            yield return null;
        }
        transform.position = endPos;
        pos = end;

        // 타일이 드러나지 않은 경우 드러나는 시간 동안 기다린다
        var currTile = TileManager.Instance.GetTile(pos);
        if (!currTile.isMoved)
        {
            currTile.isMoved = true;
            // 타일 드러나는 애니메이션
            yield return new WaitForSeconds(Tile.TILE_REVEAL_ANIM_TIME);
        }
    }

    // 이차함수로 보간
    private float Quad(float a, float b, float t)
    {
        return Mathf.Lerp(a, b, t * t);
    }


    #endregion
}
