using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ���� �� ������ ��� Ŭ����
/// 
/// </summary>
public class Player : MonoBehaviour
{
    public int pos = -1;

    // ����
    public int hp = 100;
    // ��Ȱ��ȭ�Ǵ� ��
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
        // ���� ó��
        if (hp <= 0)
        {
            hp = 0;
            rest = 1;
            // ü�� ��ȭ �˸���
        }
    }

    #region �̵� ���� �Լ�
    public void MoveCount(int count)
    {
        StartCoroutine(IEMoveCount(count));
    }

    /// <summary>
    /// Ư�� Ÿ�Ϸ� �̵�
    /// </summary>
    /// <param name="target"></param>
    public void MoveToTile(int target)
    {
        StartCoroutine(IEJump(target));
    }

    // Ÿ�� �̵� ��ü �ִϸ��̼�
    IEnumerator IEMoveCount(int count)
    {
        print($"IE Move Count : {count}");
        int sign = count > 0 ? 1 : -1;
        count = TileManager.Instance.GetMovableCount(pos, count);

        // �̵� �ִϸ��̼�
        for (int i = 0; i < count; i++)
        {
            yield return IEMoveTile(pos, pos + sign);
            yield return new WaitForSeconds(waitTime);
        }

        // �̵��� ������ Ÿ�� ȿ���� �����Ѵ�
        TileManager.Instance.GetTile(pos).ApplyTileEffect(this);
    }

    // Ư�� Ÿ�Ϸ� �ٷ� �̵��ϴ� �ִϸ��̼�
    IEnumerator IEJump(int target)
    {
        yield return IEMoveTile(pos, target);

        // �̵��� ������ Ÿ�� ȿ���� �����Ѵ�
        TileManager.Instance.GetTile(pos).ApplyTileEffect(this);
    }

    // Ÿ�� ���� �̵� �ִϸ��̼�
    IEnumerator IEMoveTile(int start, int end)
    {
        // ������
        var startPos = TileManager.Instance.GetTile(start).transform.position;
        var endPos = TileManager.Instance.GetTile(end).transform.position;

        // ȸ�� ó��
        var towardVec3 = (endPos - startPos).normalized;
        transform.rotation = Quaternion.LookRotation(towardVec3);

        // �̵� �ִϸ��̼� 
        for (float t = 0.0f; t < moveTime; t += Time.deltaTime)
        {
            float percent = t / moveTime;
            // �̵� - �ܼ� ���� �̵�
            var xzVec3 = Vector3.Lerp(startPos, endPos, t / moveTime);
            // ���� - �����Լ��� �̿��� �������� �̵�
            float maxY = Mathf.Max(startPos.y, endPos.y) + jumpHeight * GameSystem.Instance.GameScale;
            float y = percent < 0.5f ?
                Quad(startPos.y, maxY, percent * 2) :
                Quad(endPos.y, maxY, (1 - percent) * 2);
            transform.position = new Vector3(xzVec3.x, y, xzVec3.z);
            yield return null;
        }
        transform.position = endPos;
        pos = end;

        // Ÿ���� �巯���� ���� ��� �巯���� �ð� ���� ��ٸ���
        var currTile = TileManager.Instance.GetTile(pos);
        if (!currTile.isMoved)
        {
            currTile.isMoved = true;
            // Ÿ�� �巯���� �ִϸ��̼�
            yield return new WaitForSeconds(Tile.TILE_REVEAL_ANIM_TIME);
        }
    }

    // �����Լ��� ����
    private float Quad(float a, float b, float t)
    {
        return Mathf.Lerp(a, b, t * t);
    }


    #endregion
}
