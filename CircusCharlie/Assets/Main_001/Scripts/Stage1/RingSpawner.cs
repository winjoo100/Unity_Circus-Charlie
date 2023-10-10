using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSpawner : MonoBehaviour
{
    public GameObject fireRingPrefab;   // 생성할 링 프리팹
    public GameObject bonusScorePrefab; // 생성할 보너스주머니 프리팹
    public GameObject scoreUpPrefab;    // 생성할 점수상승 콜라이더 프리팹

    public float timeSpawnMin = 0.2f;   // 생성할 시간간격 최소
    public float timeSpawnMax = 1.0f;   // 생성할 시간간격 최대
    private float timeSpawn;            // 다음 배치까지의 시간 간격
    private float lastSpawnTime;        // 마지막 배치 시점

    private float yPos = -1.5f;         // 생성될 y의 값
    private float xPos = 25f;           // 생성될 x의 값

    private float bonusScore_Ypos = -0.6f;       // 보너스 주머니 y의 값
    private float bonusScore_Xpos = 25.25f;       // 보너스 주머니 x의 값

    // Start is called before the first frame update
    void Start()
    {
        // 마지막 배치 시점 초기화
        lastSpawnTime = 0f;

        // 다음번 배치까지의 시간 간격을 0으로 초기화
        timeSpawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // 게임 종료 시간 7초 전에는 생성하지 않기
        if(GameManager_Scene1.instance.gameTime > 7)
        {
            // 마지막 배치 시점에서 시간간격이 지났다면
            if (Time.time >= lastSpawnTime + timeSpawn)
            {
                // 기록된 마지막 배치 시점을 현재 시점으로 갱신
                lastSpawnTime = Time.time;

                // 다음 배치까지의 시간 간격을 timeSpawnMin ~ timeSpawnMax 에서 랜덤 설정
                timeSpawn = Random.Range(timeSpawnMin, timeSpawnMax);

                // 링 생성
                GameObject newRing = Instantiate(fireRingPrefab, new Vector3(xPos, yPos, 0f), Quaternion.identity);
                GameObject newScore = Instantiate(scoreUpPrefab, new Vector3(xPos, 0f, 0f), Quaternion.identity);

                // 확률로 보너스주머니 생성
                if (Random.Range(0, 10) <= 3)
                {
                    // 보너스 주머니 생성
                    GameObject newBonusScore = Instantiate(bonusScorePrefab, new Vector3(bonusScore_Xpos, bonusScore_Ypos, 0f), Quaternion.identity);
                }
            }
        }

        else { return; }

        
    }
}
