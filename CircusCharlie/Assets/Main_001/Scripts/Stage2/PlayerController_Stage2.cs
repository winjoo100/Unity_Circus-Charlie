using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController_Stage2 : MonoBehaviour
{
    // 게임 변수
    public GameData gameData;

    // 플레이어 애니메이터
    public Animator playerAnim;

    // 플레이어 리지드바디
    private Rigidbody2D playerRigid;

    public float moveSpeed = 0.01f; // 플레이어 이동속도
    public float jumpForce = 2f;    // 플레이어 점프 힘

    private int jumpCount = 0;      // 점프 횟수

    // 플레이어 좌우 반전
    public SpriteRenderer playerSpriteRenderer;

    // 효과음
    // 점프 효과음
    public AudioSource playerAudioSource;
    public AudioClip jumpAudio;

    // 죽는 효과음
    public AudioSource DieAudioSource;
    public AudioClip DieAudio;


    // Start is called before the first frame update
    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어 오른쪽 이동
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Move_Right();
        }

        // 플레이어 왼쪽 이동
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move_Left();
        }

        // 플레이어 점프
        if (Input.GetKeyDown(KeyCode.X))
        {
            Jump();
        }
    }

    // 플레이어 이동 관련 함수들
    // 플레이어 오른쪽 이동
    public void Move_Right()
    {
        if (GameManager_Scene2.instance.isDead == false)
        {
            // 플레이어 좌우반전 false
            playerSpriteRenderer.flipX = false;

            // 플레이어를 이동시킴
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }

    // 플레이어 왼쪽 이동
    public void Move_Left()
    {
        if (GameManager_Scene2.instance.isDead == false)
        {
            // 플레이어 좌우반전 true
            playerSpriteRenderer.flipX = true;

            // 플레이어를 이동시킴
            transform.Translate(Vector3.left * (moveSpeed * 0.5f) * Time.deltaTime);
        }

    }

    // 플레이어 점프 동작
    public void Jump()
    {
        if (GameManager_Scene2.instance.isDead == false)
        {
            if (jumpCount < 1)
            {
                // 점프 효과음
                playerAudioSource.PlayOneShot(jumpAudio);

                // 애니메이션을 바꿔줌
                playerAnim.SetBool("isJump", true); 

                // 리지드바디에 위쪽으로 힘주기
                // playerRigidbody.velocity = new Vector2(0, jumpForce);
                playerRigid.AddForce(new Vector2(0, jumpForce));
                jumpCount++;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 원숭이랑 충돌했다. => 사망
        if ((collision.gameObject.tag == "BlueMonkey") || (collision.gameObject.tag == "Monkey"))
        {
            // 죽는 효과음
            DieAudioSource.PlayOneShot(DieAudio);

            // 애니메이션 변경
            playerAnim.SetTrigger("Die");

            playerRigid.velocity = Vector3.zero;
            playerRigid.gravityScale = 0f;

            // 라이프를 감소시키고 저장합니다.
            gameData.life--;

            GameManager_Scene2.Instance.isDead = true;
        }

        // 바닥에 착지했다.
        if (collision.gameObject.tag == "Ground")
        {
            // 애니메이션을 바꿔줌
            playerAnim.SetBool("isJump", false);

            jumpCount = 0;
        }

        // 골라인에 착지했다.
        if ((collision.gameObject.tag == "End" && transform.position.y > -3.2f) &&
            (transform.position.x - 1f <= collision.transform.position.x) && (collision.transform.position.x <= transform.position.x + 1f))
        {
            // 애니메이션을 바꿔줌
            playerAnim.SetBool("isJump", false);

            // 게임 클리어
            Debug.Log("게임 클리어");
            SceneManager.LoadScene("Stage2_Clear");
        }
        else { return; }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 보너스 점수를 획득했다!
        if (collision.gameObject.tag == "Bonus")
        {
            GFunc.Log("보너스점수 획득");
            collision.gameObject.SetActive(false);

            gameData.score_Stage2 += 5000;
        }

        // 링 또는 스팟을 통과했을 때 점수 성승
        if (collision.gameObject.tag == "ScoreUp")
        {
            gameData.score_Stage2 += 1000;
        }
    }
}

