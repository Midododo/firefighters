using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour {

    private ParticleSystem particle;
    private GameObject parent;

    ParticleSystem m_System;
    ParticleSystem.Particle[] m_Particles;
    public float m_Drift = 0.01f;

    [SerializeField]
    float PlayerOffsetY = 0.5f;

    // Use this for initialization
    void Start () {
        particle = this.GetComponent<ParticleSystem>();

        // ここで Particle System を停止する.
        particle.Simulate(0.0f, true, true);
        particle.Stop();

        // root→一番親 parent→一個上の親
        parent = transform.root.gameObject;

        // 親の（プレイヤーの）位置にパーティクルを表示
        Vector3 Position = parent.transform.position;
        Position.y += PlayerOffsetY;
        particle.transform.position = Position;

        Debug.Log(parent.name + "a");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.F))
        {

            if (particle.isStopped || particle.isPaused)
            {
                particle.Simulate(1.0f, true, true);

                particle.Play();

                Debug.Log("aaaaaaa");

            }
        }
        else
        {
            particle.Simulate(0.0f, true, true);

            particle.Stop();
        }

        if(particle.isStopped == false)
        {
            // 親の（プレイヤーの）位置にパーティクルを表示
            Vector3 Position = parent.transform.position;
            Position.y += PlayerOffsetY;
            particle.transform.position = Position;
            //particle.transform.position = new Vector3(Position.x, Position.y, Position.z);


            //if (m_System == null)
            //    m_System = GetComponent<ParticleSystem>();

            //if (m_Particles == null || m_Particles.Length < m_System.main.maxParticles)
            //    m_Particles = new ParticleSystem.Particle[m_System.main.maxParticles];

            //// GetParticles is allocation free because we reuse the m_Particles buffer between updates
            //int numParticlesAlive = m_System.GetParticles(m_Particles);

            //// Change only the particles that are alive
            //for (int i = 0; i < numParticlesAlive; i++)
            //{

            //    m_Particles[i].velocity += Vector3.up * m_Drift;
            //}

            //// Apply the particle changes to the particle system
            //m_System.SetParticles(m_Particles, numParticlesAlive);
        }
    }
}

/*
_particleSystem.Simulate(
    t            : Time.unscaledDeltaTime, //パーティクルシステムを早送りする時間
    withChildren : true,                   //子のパーティクルシステムもすべて早送りするかどうか
    restart      : false                   //再起動し最初から再生するかどうか
  ;
    */
