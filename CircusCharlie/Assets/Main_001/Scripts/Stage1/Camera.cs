using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject player;

    public float cameraSpeed = 2.0f;

    private Vector3 pos;         // 위치 제한 계산/ 판단을 위한 임시 변수
    public float maxXPosition;  // x축 최대값
    public float minXPosition;  // x축 최소값

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;   // 현재 position 값

        //if (pos.x >= maxXPosition)
        //{
        //    pos.x = maxXPosition;   // x축 최대값 이상이면 x축 최대값으로 조정
        //}

        //if (pos.x <= minXPosition)
        //{
        //    pos.x = minXPosition;   // x축 최대값 이상이면 x축 최대값으로 조정
        //}


        // pos.x 값을 최소값 ~ 최대값 사이로 잘라낸다.
        pos.x = Mathf.Clamp(pos.x, minXPosition, maxXPosition);
        transform.position = pos;

        // x좌표값을 직접 다듬어 버린다.
        // transform.position.x = Mathf.Clamp(transform.position.x, minXPosition, maxXPosition);

        Vector3 dir = player.transform.position - this.transform.position;
        Vector3 moveVector = new Vector3((dir.x) * cameraSpeed * Time.deltaTime, 0.0f, 0.0f);
        this.transform.Translate(moveVector);
    }
}
