using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke_Extinguish : MonoBehaviour
{

    private GameObject parent;
    private ParticleSystem particle;
    public int GoSmoke = 0;

    // Use this for initialization
    void Start()
    {
        parent = transform.root.gameObject;
        particle = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // 火が消え、親は生存中、まだ煙を出していないなら
        if (particle.isStopped && GoSmoke == 1)
        {
            //GoSmoke = 2;
            Debug.Log("aa");
            // 親の（プレイヤーの）位置にパーティクルを表示   
            Vector3 Position = parent.transform.position;
            particle.transform.position = Position;

            particle.Play();
        }

        // 煙を出し終えたら
        if (particle.isStopped && GoSmoke == 2)
        {
            GoSmoke = 0;

            particle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            parent.SetActive(false);
        }
    }

    public void SetGoSmoke(int num)
    {
        GoSmoke = num;
    }

    public int GetGoSmoke()
    {
        return (GoSmoke);
    }
}