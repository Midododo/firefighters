using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitEvent : MonoBehaviour
{
    private bool m_Player1 = false;
    private bool m_Player2 = false;


    void Update()
    {
        // プレイヤーがわくに入っているかの確認
        if (!m_Player1 && !m_Player2)
        {
            // シーン遷移
        }
    }


    void OnTriggerEnter(Collider Collider)
    {
        if (Collider.gameObject.tag == "Player1")
        {
            m_Player1 = true;
        }

        if (Collider.gameObject.tag == "Player2")
        {
            m_Player2 = true;
        }
    }

    void OnTriggerStay(Collider Collider)
    {
    }

    void OnTriggerExit(Collider Collider)
    {
        if (Collider.gameObject.tag == "Player1")
        {
            m_Player1 = false;
        }

        if (Collider.gameObject.tag == "Player2")
        {
            m_Player2 = false;
        }
    }
}
