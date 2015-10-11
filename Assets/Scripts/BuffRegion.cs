using UnityEngine;
using System.Collections;

public class BuffRegion : RegionTrigger {
    public int m_regionBuffAmount;
    public float m_triggerGap;

    private float m_curTime;
    private bool m_timeToTrigger;

    protected override void Start() {
        base.Start();
        m_curTime = 0;
        m_timeToTrigger = false;
    }

    void Update() {
        m_curTime += Time.deltaTime;

        if (m_curTime >= m_triggerGap) {
            m_curTime = 0;
            m_timeToTrigger = true;
        }
    }

    protected override void OnTriggerStay(Collider other) {
        if (other.tag == "Player" && m_timeToTrigger) {
            m_timeToTrigger = false;
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (m_type == RegionTriggerType.DAMAGE) {
                player.damage(m_regionBuffAmount);
            } else if (m_type == RegionTriggerType.RESTORE) {
                player.restoreHealth(m_regionBuffAmount);
            }
        }
    }
}
