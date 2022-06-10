using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 가상 주사위 버튼 클래스
// 
public class DiceButton : MonoBehaviour
{
    public void Dice()
    {
        // 랜덤 값을 받는다고 가정한다
        int random = UnityEngine.Random.Range(0, 6) + 1;

        GameSystem.Instance.MovePlayer(random);
    }
}
