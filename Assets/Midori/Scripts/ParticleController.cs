﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{

    ParticleSystem m_System;
    ParticleSystem.Particle[] m_Particles;
    public float m_Drift = 0.01f;

    [SerializeField]
    float FireAlphaDecreaseSpeed = 0.001f;


    private ParticleSystem particle;
    //private ParticleCollisionEvent[] collisionEvents;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
        //collisionEvents = new ParticleCollisionEvent[10];
    }

    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log(other.name + "a");
        //Debug.Log(other.tag + "a");

        // オブジェクトとの当たり判定
        if (other.layer == 9)
        {
            Debug.Log("箱だよ");
            Vector3 Direction = other.transform.position - this.transform.position;
            Direction.y += 3.0f;
            Direction *= 2.0f;
            other.gameObject.GetComponent<Rigidbody>().AddForce(Direction, ForceMode.Impulse);
        }

        if (other.tag == "Fire")
        {
            // 現時点のstartColorの設定を取得
            Color32 colorTemp = other.GetComponent<ParticleSystem>().main.startColor.color;

            // αの値を減少
            float Alpha = colorTemp.a - FireAlphaDecreaseSpeed;
            if (Alpha < 0.0f)
            {
                Alpha = 0.0f;
                other.gameObject.SetActive(false);
            }

            // 反映させる
            ParticleSystem.MinMaxGradient color = new ParticleSystem.MinMaxGradient();
            color.mode = ParticleSystemGradientMode.Color;
            color.color = new Color32(colorTemp.r, colorTemp.g, colorTemp.b, (byte)Alpha);
            ParticleSystem.MainModule MainSystem = other.GetComponent<ParticleSystem>().main;
            MainSystem.startColor = color;
        }


        //　イベントの取得
        //particle.GetCollisionEvents(other, collisionEvents);
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        particle.GetCollisionEvents(other, collisionEvents);

        //　衝突した位置を取得し、ダメージスクリプトを呼び出す
        foreach (var colEvent in collisionEvents)
        {
            Vector3 pos = colEvent.intersection;
            //colEvent.colliderComponent.gameObject.SetActive(false);

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
            //    //m_Particles[i].startColor = new Color32(0, 0, 0, 0); 
            //}

            //// Apply the particle changes to the particle system
            //m_System.SetParticles(m_Particles, numParticlesAlive);
        }
    }

    void OnParticleTrigger()
    {
        //Debug.Log("unko");
    }
    //    ParticleSystem ps = GetComponent<ParticleSystem>();

    //    // particles
    //    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    //    List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

    //    // get
    //    int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    //    int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

    //    // iterate
    //    for (int i = 0; i < numEnter; i++)
    //    {
    //        ParticleSystem.Particle p = enter[i];
    //        p.startColor = new Color32(255, 0, 0, 255);
    //        enter[i] = p;
    //    }
    //    for (int i = 0; i < numExit; i++)
    //    {
    //        ParticleSystem.Particle p = exit[i];
    //        p.startColor = new Color32(0, 255, 0, 255);
    //        exit[i] = p;
    //    }

    //    // set
    //    ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    //    ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    //}
}