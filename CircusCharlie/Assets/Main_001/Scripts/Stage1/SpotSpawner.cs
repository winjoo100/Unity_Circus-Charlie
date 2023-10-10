using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotSpawner : MonoBehaviour
{
    public GameObject fireSpotPrefab;   // 생성할 스팟 프리팹
    public GameObject scoreUpPrefab;    // 생성할 점수상승 콜라이더 프리팹
    public GameObject endLinePrefab;    // 골라인 프리팹

    public float timeSpawnMin = 4f;     // 생성할 시간간격 최소
    public float timeSpawnMax = 8f;     // 생성할 시간간격 최대
    private float timeSpawn;            // 다음 배치까지의 시간 간격
    private float lastSpawnTime;        // 마지막 배치 시점

    private float yPos = -4f;           // 생성될 y의 값
    private float xPos = 25f;           // 생성될 x의 값

    private bool isEndSpawn = false;    // 골라인이 생성 됬는지 체크

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
        // 게임종료 5초 전에는 생성하지 않음
        if (GameManager_Scene1.instance.gameTime > 7)
        {
            // 마지막 배치 시점에서 시간간격이 지났다면
            if (Time.time >= lastSpawnTime + timeSpawn)
            {
                // 기록된 마지막 배치 시점을 현재 시점으로 갱신
                lastSpawnTime = Time.time;

                // 다음 배치까지의 시간 간격을 timeSpawnMin ~ timeSpawnMax 에서 랜덤 설정
                timeSpawn = Random.Range(timeSpawnMin, timeSpawnMax);

                // 링 생성
                GameObject newRing = Instantiate(fireSpotPrefab, new Vector3(xPos, yPos, 0f), Quaternion.identity);
                GameObject newScore = Instantiate(scoreUpPrefab, new Vector3(xPos, 0f, 0f), Quaternion.identity);
            }

        }

        // 게임종료 5초 전에는 골라인을 생성합니다.
        else if (GameManager_Scene1.instance.gameTime < 5 && isEndSpawn == false)
        {
            // 골라인 생성
            GameObject endLine = Instantiate(endLinePrefab, new Vector3(xPos + 3f, -4.1f, 0f), Quaternion.identity);
            isEndSpawn = true;
        }

        else { return; }

        
    }
}
