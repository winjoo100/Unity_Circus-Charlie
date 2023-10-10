using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeySpawner : MonoBehaviour
{
    public GameObject monkey_Prefab;        // 생성할 원숭이 프리팹
    public GameObject blueMonkey_Prefab;    // 생성할 파란원숭이 프리팹
    public GameObject scoreUp_Prefab;       // 생성할 점수상승 콜라이더 프리팹
    public GameObject scoreUp2_Prefab;      // 생성할 점수상승 콜라이더 프리팹 2

    public GameObject endLinePrefab;        // 골라인 프리팹 생성

    public float timeSpawnMin = 0.2f;       // 생성할 시간간격 최소
    public float timeSpawnMax = 1.0f;       // 생성할 시간간격 최대
    private float timeSpawn;                // 다음 배치까지의 시간 간격
    private float lastSpawnTime;            // 마지막 배치 시점

    private float yPos = 1f;                // 생성될 y의 값
    private float xPos = 25f;               // 생성될 x의 값

    private bool isEndSpawn = false;        // 골라인이 생성 됬는지 체크

    void Start()
    {
        // 마지막 배치 시점 초기화
        lastSpawnTime = 0f;

        // 다음번 배치까지의 시간 간격을 0으로 초기화
        timeSpawn = 0f;
    }

    void Update()
    {
        // 게임 종료 시간 10초 전에는 생성하지 않기
        if (GameManager_Scene2.instance.gameTime > 10)
        {
            // 마지막 배치 시점에서 시간간격이 지났다면
            if (Time.time >= lastSpawnTime + timeSpawn)
            {
                // 기록된 마지막 배치 시점을 현재 시점으로 갱신
                lastSpawnTime = Time.time;

                // 다음 배치까지의 시간 간격을 timeSpawnMin ~ timeSpawnMax 에서 랜덤 설정
                timeSpawn = Random.Range(timeSpawnMin, timeSpawnMax);

                // 원숭이 생성 & 점수 확인용 콜라이더 생성
                GameObject newMonkey = Instantiate(monkey_Prefab, new Vector3(xPos, yPos, 0f), Quaternion.identity);
                GameObject newScoreUp = Instantiate(scoreUp_Prefab, new Vector3(xPos, 0f, 0f), Quaternion.identity);

                // 확률로 파란원숭이 생성
                if (Random.Range(0, 10) <= 4)
                {
                    // 파란 원숭이 생성 & 점수 확인용 콜라이더 2 생성
                    GameObject newBonusScore = Instantiate(blueMonkey_Prefab, new Vector3(xPos - 5, yPos, 0f), Quaternion.identity);
                    GameObject newScoreUp2 = Instantiate(scoreUp2_Prefab, new Vector3(xPos - 5, 0f, 0f), Quaternion.identity);
                }
            }
        }

        // 게임종료 5초 전에는 골라인을 생성합니다.
        else if (GameManager_Scene2.instance.gameTime < 5 && isEndSpawn == false)
        {
            // 골라인 생성
            GameObject endLine = Instantiate(endLinePrefab, new Vector3(xPos, 0f, 0f), Quaternion.identity);
            isEndSpawn = true;
        }

        else { return; }


    }
}


