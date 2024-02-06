using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueMonkeyMove : MonoBehaviour
{
    private Rigidbody2D blueMonkeyRigid;

    // Start is called before the first frame update
    void Start()
    {
        blueMonkeyRigid = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 일반 원숭이랑 충돌하면,
        if (GameManager_Scene2.instance.isDead == false)
        {
            if (collision.gameObject.tag == "Monkey")
            {
                // 이동을 바꾼다.
                blueMonkeyRigid.velocity = new Vector2(0f, 20f);
            }
        }
    }
}
