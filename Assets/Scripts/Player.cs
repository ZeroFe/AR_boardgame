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
    /// Ư�� Ÿ�Ϸ� �̵��ϴ� �Լ�
    /// </summary>
    /// <param name="pos"></param>
    public void MoveToTile(int target)
    {
        StartCoroutine(IEMoveToTile(target));
    }

    // Ÿ�� �̵� ��ü �ִϸ��̼�
    IEnumerator IEMoveCount(int count)
    {
        int sign = count > 0 ? 1 : -1;
        count = TileManager.Instance.GetMovableCount(pos, count);

        // �̵� �ִϸ��̼�
        for (int i = 0; i < count; i++)
        {
            yield return IEMoveTile(pos, pos + 1);
            yield return new WaitForSeconds(waitTime);
        }

        // �̵��� ������ Ÿ�� ȿ���� �����Ѵ�
        TileManager.Instance.GetTile(pos).ApplyTileEffect(this);
    }

    IEnumerator IEMoveToTile(int target)
    {
        yield return IEMoveTile(pos, target);
    }

    // Ÿ�� ���� �̵� �ִϸ��̼�
    IEnumerator IEMoveTile(int start, int end)
    {
        // ȸ�� ó�� ���
        //Quaternion.RotateTowards()

        // ������
        var startPos = TileManager.Instance.GetTile(start).transform.position;
        var endPos = TileManager.Instance.GetTile(end).transform.position;

        // �̵� �ִϸ��̼� 
        for (float t = 0.0f; t < moveTime; t += Time.deltaTime)
        {
            float percent = t / moveTime;
            // �̵� - �ܼ� ���� �̵�
            var xzVec3 = Vector3.Lerp(startPos, endPos, t / moveTime);
            // ���� - �����Լ��� �̿��� �������� �̵�
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

    // �����Լ��� ����
    private float Quad(float a, float b, float t)
    {
        return Mathf.Lerp(a, b, t * t);
    }
}
