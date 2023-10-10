using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager_Scene1 : MonoBehaviour
{
    //게임데이터에 접근합니다.
    public GameData gameData;

    // 싱글턴 인스턴스를 저장할 정적 변수
    public static GameManager_Scene1 instance;

    // 텍스트와 목숨 이미지를 가져옵니다.
    public Text timeText;
    public Text scoreText;
    public Image life_1;
    public Image life_2;
    public Image life_3;

    
    // 게임 관련 변수들
    // private float waitTime_ = 0f;   // 대기시간
    private int gameScore;          // 점수
    private int life;               // 목숨
    public float gameTime = 30;     // 시간
    public bool gameEnd = false;    // 게임종료
    public bool isDead = false;     // 사망체크

    private float waitTime = 0f; // 대기 시간

    // GameManager 싱글턴 인스턴스에 접근할 수 있는 프로퍼티
    public static GameManager_Scene1 Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        // 인스턴스가 이미 존재하는 경우 중복 생성을 방지하기 위해
        // 게임 오브젝트를 파괴합니다.
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 싱글턴 인스턴스를 현재 인스턴스로 설정합니다.
        instance = this;

        // 게임매니저 오브젝트를 파괴하지 않고 게임 전체에 유지합니다.
        //DontDestroyOnLoad(gameObject);

        // 저장된 라이프를 가져옵니다.
        life = gameData.life;

        // 라이프가 0일 때,
        if (gameData.life <= 0)
        {
            // 목숨이 0이 되면 타이틀 씬으로 나가기
            SceneManager.LoadScene("TitleScene");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        //scoreText = GetComponent<Text>();

        if (scoreText == null)
        {
            Debug.LogError("ScoreText를 찾을 수 없습니다!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 점수를 실시간 갱신합니다.
        gameScore = gameData.score_Stage1;

        // 점수를 출력합니다.
        UpdateScoreText();

        // 시간을 출력합니다.
        UpdateTimeText();

        // 목숨이 줄었을 때 표시
        if (life == 2)
        {
            life_1.gameObject.SetActive(false);
        }
        else if (life == 1)
        {
            life_1.gameObject.SetActive(false);
            life_2.gameObject.SetActive(false);
        }
        
        else if (life == 0)
        {
            life_1.gameObject.SetActive(false);
            life_2.gameObject.SetActive(false);
            life_3.gameObject.SetActive(false);
        }

        // 게임종료
        if (gameTime < 0)
        {
            gameEnd = true;
        }

        // 목숨 1 줄어듦
        if(isDead == true)
        {
            waitTime += Time.deltaTime;
        }

        // 재시작
        if (waitTime > 1.5f)
        {
            if (life > 0)
            {
                gameData.score_Stage1 = 0;
                SceneManager.LoadScene("Stage1_Waiting");
            }
        }
    }

    // 스코어 텍스트 업데이트
    void UpdateScoreText()
    {
        if(isDead != true)
        {
            if (scoreText != null)
            {
                scoreText.text = string.Format("{0}", gameScore);
            }
        }
    }

    // 타임 텍스트 업데이트
    void UpdateTimeText()
    {
        if (isDead != true)
        {
            gameTime -= Time.deltaTime;

            if (timeText != null && gameTime >= 0)
            {
                timeText.text = string.Format("{0}", (int)gameTime);
            }

            else { return; }
        }
    }
}
