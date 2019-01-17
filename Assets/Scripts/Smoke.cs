using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour {

    private GameObject parentPlayer;
    private ParticleSystem particle;

    [SerializeField]
    float PlayerOffsetY = 0.5f;

    // Use this for initialization
    void Start () {

        parentPlayer = transform.root.gameObject;
        particle = this.GetComponent<ParticleSystem>();

        // 親の（プレイヤーの）位置にパーティクルを表示
        Vector3 Position = parentPlayer.transform.position;
        Position.y += PlayerOffsetY;
        particle.transform.position = Position;
    }

    // Update is called once per frame
    void Update () {

        float GaugePoint;

        // ゲージの取得
        if (parentPlayer.tag == "Player1")
        {
            GaugePoint = GameObject.Find("Nakami_Left").GetComponent<Gage>().GetGaugePoint();
        }
        else
        {
            GaugePoint = GameObject.Find("Nakami_Right").GetComponent<Gage>().GetGaugePoint();
        }

        if (GaugePoint > 0)
        {
            if (particle.isStopped || particle.isPaused)
            {
                //particle.Simulate(1.0f, true, true);

                particle.Play();
            }
        }
        else
        {
            //particle.Simulate(0.0f, true, true);

            particle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }

        if(particle.isStopped == false)
        {
            // 親の（プレイヤーの）位置にパーティクルを表示
            Vector3 Position = parentPlayer.transform.position;
            Position.y += PlayerOffsetY;
            particle.transform.position = Position;
            //particle.transform.position = new Vector3(Position.x, Position.y, Position.z);
        }
    }
}
