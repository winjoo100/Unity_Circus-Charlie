using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage2_Clear : MonoBehaviour
{
    public Text scorecalText;
    public Text finalScoreText;
    public GameData gameData;
    private float titleSceneTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // 현재 스코어 = 스코어 * 라이프
        scorecalText.text = string.Format("Score * Life : {0} * {1}", gameData.score_Stage2, gameData.life);
        gameData.score_Stage2 *= gameData.life;
        finalScoreText.text = string.Format("Final Score : {0}", gameData.score_Stage2);

    }

    // Update is called once per frame
    void Update()
    {
        titleSceneTimer += Time.deltaTime;

        if (titleSceneTimer > 3.0f)
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
