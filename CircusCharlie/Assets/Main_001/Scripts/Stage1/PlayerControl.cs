using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerControl : MonoBehaviour
{
    public GameData gameData;

    public Animator playerAnim;
    public Animator lionAnim;
    public SpriteRenderer playerSpriteRenderer;
    public SpriteRenderer lionSpriteRenderer;

    private Rigidbody2D playerRigid;

    public float moveSpeed = 0.01f;
    public float jumpForce = 2f;

    private int jumpCount = 0;

    private bool move = false;
    private bool jump = false;

    // 효과음
    // 점프 효과음
    public AudioSource playerAudioSource;
    public AudioClip jumpAudio;

    // 보너스 주머니 효과음
    public AudioSource bonusAudioSource;
    public AudioClip bonusAudio;

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
        // 아무키도 입력받지 않고 있으면 플레이어 idle 모션을 띄웁니다.
        if ((Input.anyKey == false) && (jump == false))
        {
            move = false;

            // 다꺼줍니다.
            playerAnim.SetBool("isMove", false);
            playerAnim.SetBool("isJump", false);
        }

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
    public void Move_Right()
    {
        if(GameManager_Scene1.instance.isDead == false)
        {
            // 좌우 반전
            playerSpriteRenderer.flipX = false;
            lionSpriteRenderer.flipX = false;

            // 애니메이션을 바꿔줌
            playerAnim.SetBool("isMove", true);
            lionAnim.SetBool("isMove", true);

            // 플레이어를 이동시킴
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

            // 무브중임을 체크
            move = true;
        }
    }
    
    public void Move_Left()
    {
        if (GameManager_Scene1.instance.isDead == false)
        {
            // 좌우 반전
            playerSpriteRenderer.flipX = true;
            lionSpriteRenderer.flipX = true;

            // 애니메이션을 바꿔줌
            playerAnim.SetBool("isMove", true);
            lionAnim.SetBool("isMove", true);

            // 플레이어를 이동시킴
            transform.Translate(Vector3.left * (moveSpeed * 0.5f) * Time.deltaTime);

            // 무브중임을 체크
            move = true;
        }
            
    }

    public void Jump()
    {
        if (GameManager_Scene1.instance.isDead == false)
        {
            if (jumpCount < 1)
            {
                // 효과음을 출력함
                playerAudioSource.PlayOneShot(jumpAudio);

                // 애니메이션을 바꿔줌
                playerAnim.SetBool("isJump", true);
                lionAnim.SetBool("isJump", true);
                playerAnim.SetBool("isMove", false);
                lionAnim.SetBool("isMove", false);

                // 리지드바디에 위쪽으로 힘주기
                // playerRigidbody.velocity = new Vector2(0, jumpForce);
                playerRigid.AddForce(new Vector2(0, jumpForce));

                jump = true;
                jumpCount++;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥에 착지했다.
        if (collision.gameObject.tag == "Ground")
        {
            // 애니메이션을 바꿔줌
            playerAnim.SetBool("isJump", false);
            lionAnim.SetBool("isJump", false);
            lionAnim.SetBool("isMove", true);

            jump = false;

            jumpCount = 0;
        }

        // 골라인에 착지했다.
        if ((collision.gameObject.tag == "End" && transform.position.y > -3.2f) &&
            (transform.position.x - 1f <= collision.transform.position.x) && (collision.transform.position.x <= transform.position.x + 1f))
        {
            // 애니메이션을 바꿔줌
            playerAnim.SetBool("isJump", false);
            lionAnim.SetBool("isJump", false);
            lionAnim.SetBool("isMove", true);

            // 게임 클리어
            Debug.Log("게임 클리어");
            SceneManager.LoadScene("Stage1_Clear");
        }
        else { return; }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 링에 닿았다!
        if (collision.gameObject.tag == "Ring")
        {
            DieAudioSource.PlayOneShot(DieAudio);

            playerAnim.SetTrigger("Die");
            lionAnim.SetTrigger("Die");

            playerRigid.velocity = Vector3.zero;
            playerRigid.gravityScale = 0f;

            // 라이프를 감소시키고 저장합니다.
            gameData.life--;

            GameManager_Scene1.Instance.isDead = true;
        }

        // 보너스 점수를 획득했다!
        if (collision.gameObject.tag == "Bonus")
        {
            // 효과음 출력!
            bonusAudioSource.PlayOneShot(bonusAudio);

            GFunc.Log("보너스점수 획득");
            collision.gameObject.SetActive(false);

            gameData.score_Stage1 += 5000;
        }

        // 링 또는 스팟을 통과했을 때 점수 성승
        if (collision.gameObject.tag == "ScoreUp")
        {
            gameData.score_Stage1 += 1000;
        }
    }
}
