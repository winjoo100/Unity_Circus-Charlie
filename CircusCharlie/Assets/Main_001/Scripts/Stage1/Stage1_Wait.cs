using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class Stage1_Wait : MonoBehaviour
{
    private float playSceneTimer;

    public GameData gameData;
    public Image life_1;
    public Image life_2;
    public Image life_3;

    // Start is called before the first frame update
    void Start()
    {
        int life_ = gameData.life;
        playSceneTimer = 0f;

        // 목숨이 줄었을 때 표시
        if (life_ == 2)
        {
            life_1.gameObject.SetActive(false);
        }
        else if (life_ == 1)
        {
            life_1.gameObject.SetActive(false);
            life_2.gameObject.SetActive(false);
        }
        else if (life_ == 0)
        {
            life_1.gameObject.SetActive(false);
            life_2.gameObject.SetActive(false);
            life_3.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        playSceneTimer += Time.deltaTime;

        if (playSceneTimer > 3.0f)
        {
            SceneManager.LoadScene("Stage1");
        }
    }
}
