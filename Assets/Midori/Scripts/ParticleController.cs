using System.Collections;
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
        //Debug.Log(other.layer);

        // オブジェクトとの当たり判定
        if (other.layer == 9)
        {
            Debug.Log("箱だよ");
            Vector3 Direction = other.transform.position - this.transform.position;
            Direction.y += 3.0f;
            Direction *= 2.0f;
            other.gameObject.GetComponent<Rigidbody>().AddForce(Direction, ForceMode.Impulse);
        }

        if (other.tag == "Fire" || other.layer == 31)
        {
            Debug.Log("aho");

            // 現時点のstartColorの設定を取得
            Color colorTemp = other.GetComponent<ParticleSystem>().main.startColor.color;

            // αの値を減少
            colorTemp.a -= FireAlphaDecreaseSpeed;

            if (colorTemp.a < 0.0f)
            {
                colorTemp.a = 0.0f;

                other.gameObject.SetActive(false);

                //other.transform.root.gameObject.SetActive(false);
            }
            else if (colorTemp.a < 0.2f && colorTemp.a > 0)
            {
                Smoke_Extinguish SmokeScript;
                SmokeScript = GameObject.Find("Smoke_Extinguish").GetComponent<Smoke_Extinguish>();

                if(SmokeScript.GetGoSmoke() == 0)
                {
                    SmokeScript.SetGoSmoke(1);
                }
            }

            // 反映させる
            ParticleSystem.MinMaxGradient color = new ParticleSystem.MinMaxGradient();
            color.mode = ParticleSystemGradientMode.Color;
            color.color = new Color(colorTemp.r, colorTemp.g, colorTemp.b, colorTemp.a);
            ParticleSystem.MainModule MainSystem = other.GetComponent<ParticleSystem>().main;
            MainSystem.startColor = color;
        }

        //　イベントの取得
        //particle.GetCollisionEvents(other, collisionEvents);
        //List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        //particle.GetCollisionEvents(other, collisionEvents);

        ////　衝突した位置を取得し、ダメージスクリプトを呼び出す
        //foreach (var colEvent in collisionEvents)
        //{
        //    Debug.Log("out");
        //    Vector3 pos = colEvent.intersection;
        //    colEvent.colliderComponent.gameObject.SetActive(false);

            //if (m_System == null)
            //    m_System = GetComponent<ParticleSystem>();

            //if (m_Particles == null || m_Particles.Length < m_System.main.maxParticles)
            //    m_Particles = new ParticleSystem.Particle[m_System.main.maxParticles];

            //// GetParticles is allocation free because we reuse the m_Particles buffer between updates
            //int numParticlesAlive = m_System.GetParticles(m_Particles);

            //// Change only the particles that are alive
            //for (int i = 0; i < numParticlesAlive; i++)
            //{
            ////m_Particles[i].velocity += Vector3.up * m_Drift;
            //if (m_Particles[i].velocity.x < 0.0f && m_Particles[i].velocity.y < 0.0f && m_Particles[i].velocity.z < 0.0f)
            //    m_Particles[i].startColor = new Color32(0, 0, 0, 0);

            //}

            //// Apply the particle changes to the particle system
            //m_System.SetParticles(m_Particles, numParticlesAlive);
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Fire" || other.gameObject.layer == 31)
        {

            // 現時点のstartColorの設定を取得
            Color colorTemp = other.gameObject.GetComponent<ParticleSystem>().main.startColor.color;

            // αの値を減少
            colorTemp.a -= FireAlphaDecreaseSpeed;
            if (colorTemp.a < 0.0f)
            {
                colorTemp.a = 0.0f;
                other.gameObject.SetActive(false);
                other.transform.root.gameObject.SetActive(false);
            }
            colorTemp.b -= (byte)FireAlphaDecreaseSpeed;
            colorTemp.r -= (byte)FireAlphaDecreaseSpeed;
            colorTemp.g -= (byte)FireAlphaDecreaseSpeed;

            // 反映させる
            ParticleSystem.MinMaxGradient color = new ParticleSystem.MinMaxGradient();
            color.mode = ParticleSystemGradientMode.Color;
            color.color = new Color(colorTemp.r, colorTemp.g, colorTemp.b, colorTemp.a);
            ParticleSystem.MainModule MainSystem = other.gameObject.GetComponent<ParticleSystem>().main;
            MainSystem.startColor = color;
        }
    }

    void OnParticleTrigger()
    {
        //Debug.Log("unko");
        ParticleSystem ps = GetComponent<ParticleSystem>();

        // particles
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
        List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

        // get
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        // iterate
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            p.startColor = new Color32(0, 0, 0, 0);
            enter[i] = p;
        }
        for (int i = 0; i < numExit; i++)
        {
            ParticleSystem.Particle p = exit[i];
            p.startColor = new Color32(0, 0, 0, 0);
            exit[i] = p;
        }

        // set
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    }
}