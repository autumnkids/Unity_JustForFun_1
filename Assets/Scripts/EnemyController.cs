using UnityEngine;
using System.Collections;

public class EnemyController : UnitController {
    public int m_attack;
    public float m_attackFrequency;
    public float m_attackRange;

    private PlayerController m_player;
    private NavMeshAgent m_agent;
    private float m_curTime;
    private bool m_readyToAttack;

    protected override void Start() {
        base.Start();

        m_readyToAttack = false;
        m_curTime = m_attackFrequency;
        m_player = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<PlayerController>();
        m_agent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        m_curTime += Time.deltaTime;
        if (m_curTime >= m_attackFrequency) {
            m_curTime = m_attackFrequency;
            m_readyToAttack = true;
        }

        if (m_player && m_agent) {
            m_agent.SetDestination(m_player.transform.position);
            if (Vector3.Distance(transform.position, m_player.transform.position) <= m_attackRange && m_readyToAttack) {
                m_readyToAttack = false;
                m_curTime = 0;
                m_player.damage(m_attack);
            }
        }

        float sizeRate = m_curHealth * 1f / m_totalHealth;
        transform.localScale = Vector3.one * sizeRate;

        if (m_curHealth <= 0) { Destroy(gameObject); }
    }
}
