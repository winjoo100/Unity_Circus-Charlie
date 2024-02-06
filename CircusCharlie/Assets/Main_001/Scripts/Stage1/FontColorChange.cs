using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontColorChange : MonoBehaviour
{
    public GameObject[] scoreTexts;

    private int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // 1초마다 ChnageColor 함수를 호출합니다.
        InvokeRepeating("ChangeColor", 0f, 0.5f);
    }

    private void ChangeColor()
    {
        // 현재 색상을 변경하고 다음 색상으로 전환합니다.

        if (currentIndex % 3 == 0)
        {
            scoreTexts[0].gameObject.SetActive(true);
            scoreTexts[1].gameObject.SetActive(false);
            scoreTexts[2].gameObject.SetActive(false);
        }
        else if (currentIndex % 3 == 1)
        {
            scoreTexts[0].gameObject.SetActive(false);
            scoreTexts[1].gameObject.SetActive(true);
            scoreTexts[2].gameObject.SetActive(false);
        }
        else
        {
            scoreTexts[0].gameObject.SetActive(false);
            scoreTexts[1].gameObject.SetActive(false);
            scoreTexts[2].gameObject.SetActive(true);
        }

        currentIndex++;
    }
}
