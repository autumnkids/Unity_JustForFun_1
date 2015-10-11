using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {
    private PlayerController m_player;
    private NavMeshAgent m_agent;

    void Start() {
        m_player = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<PlayerController>();
        m_agent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        if (m_player && m_agent) {
            m_agent.SetDestination(m_player.transform.position);
        }
    }
}
