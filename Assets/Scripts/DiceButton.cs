using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �ֻ��� ��ư Ŭ����
// 
public class DiceButton : MonoBehaviour
{
    public void Dice()
    {
        // ���� ���� �޴´ٰ� �����Ѵ�
        int random = UnityEngine.Random.Range(0, 6) + 1;

        GameSystem.Instance.MovePlayer(random);
    }
}
