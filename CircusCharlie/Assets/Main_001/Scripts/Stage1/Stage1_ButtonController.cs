using UnityEngine;

public class Stage1_ButtonController : MonoBehaviour
{
    public bool isLeft_Downing;     // 왼쪽 이동을 위한 조건
    public bool isRight_Downing;    // 오른쪽 이동을 위한 조건
    public bool isJump_Downing;     // 점프 이동을 위한 조건
    public PlayerControl playerControl;

    void Update()
    {
        // 왼쪽 버튼을 누르고 있다면
        if(isLeft_Downing)
        {
            playerControl.Move_Left();
        }

        // 오른쪽 버튼을 누르고 있다면
        if(isRight_Downing)
        {
            playerControl.Move_Right();
        }

        // 점프 버튼을 누르고 있다면
        if(isJump_Downing)
        {
            playerControl.Jump();
        }
    }

    // 왼쪽 클릭
    public void LeftDown()
    {
        isLeft_Downing = true;
    }

    public void LeftUp()
    {
        isLeft_Downing = false;
    }

    // 오른쪽 클릭
    public void RightDown()
    {
        isRight_Downing = true;
    }

    public void RightUp()
    {
        isRight_Downing = false;
    }

    // 점프 클릭
    public void JumpDown()
    {
        isJump_Downing = true;
    }

    public void JumpUp()
    {
        isJump_Downing = false;
    }
}
