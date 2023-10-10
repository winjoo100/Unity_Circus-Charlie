using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Load_Stage1 : MonoBehaviour
{
    // 게임 변수들
    public GameData gameData;

    // 베스트 스코어 text
    public Text bestScore1;
    public Text bestScore2;

    // 버튼을 할당할 버튼 컴포넌트
    public Button button1;
    public Button button2;
    public string waitingSceneName1 = "Stage1_Waiting";
    public string waitingSceneName2 = "Stage2_Waiting";
    public string stageName1 = "Stage1";
    public string stageName2 = "Stage2";

    // 대기시간
    public float waitTime = 1.5f;

    private void Start()
    {
        // 베스트 스코어 stage 1 갱신
        if (gameData.score_Stage1 > gameData.bestScore_Stage1)
        {
            gameData.bestScore_Stage1 = gameData.score_Stage1;
        }

        // 베스트 스코어 stage 2 갱신
        if (gameData.score_Stage2 > gameData.bestScore_Stage2)
        {
            gameData.bestScore_Stage2 = gameData.score_Stage2;
        }

        // 베스트 스코어 출력
        bestScore1.text = string.Format("{0}", gameData.bestScore_Stage1);
        bestScore2.text = string.Format("{0}", gameData.bestScore_Stage2);

        // 버튼 클릭 이벤트에 Load_Scene1 함수를 연결합니다.
        button1.onClick.AddListener(LoadScene1);
        button2.onClick.AddListener(LoadScene2);

        // 게임 데이터를 초기화합니다.
        gameData.score_Stage1 = 0;
        gameData.score_Stage2 = 0;
        gameData.life = 3;
    }

    // 스테이지 1 로드합니다.
    private void LoadScene1()
    {
        SceneManager.LoadScene(waitingSceneName1);
    }

    // 스테이지 2 로드합니다.
    private void LoadScene2()
    {
        SceneManager.LoadScene(waitingSceneName2);
    }
}
