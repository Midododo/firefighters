using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEEvent : MonoBehaviour
{
    private bool m_HasTriggerd = false;

    void OnTriggerEnter(Collider Collider)
    {
        if (m_HasTriggerd)
        { return; }

        if (Collider.gameObject.name == "Player(Clone)")
        {
            // event
        }
        if (Collider.gameObject.name == "TempPlayer(Clone)")
        {

            GameObject.Find("MapManager").GetComponent<MapManager>().YogaFire(Collider.transform.position);
            m_HasTriggerd = true;
        }
    }

    void OnTriggerStay(Collider Collider)
    {

    }

    void OnTriggerExit(Collider Collider)
    {

    }

}
