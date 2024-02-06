using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    const float RESPAWNOFFSET = 4f; // 리스폰 오프셋 

    private float width;    // 배경의 가로 길이
    private void Awake()
    {
        // BoxColiider2D 컴포넌트의 size 필드의 x 값을 가로 길이로 사용
        BoxCollider2D backGroundCollider = GetComponent<BoxCollider2D>();
        width = backGroundCollider.size.x;
    }

    private void Update()
    {
        // 현재 위치가 원점에서 왼쪽으로 width 이상 이동했을 때 위치를 재배치
        if (transform.position.x <= -width * 2)
        {
            Reposition();
        }
    }

    private void Reposition()
    {
        // 현재 위치에서 오른쪽으로 가로 길이 * 오프셋 만큼 이동
        Vector2 offset = new Vector2(width * RESPAWNOFFSET, 0);

        // transform.position = (Vector2)transform.position + offset; 래핑함
        transform.position = transform.position.AddVector(offset);
    }
}
