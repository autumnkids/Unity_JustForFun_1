using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour {
    public float m_totalHealth;

    protected float m_curHealth;

    public virtual void restoreHealth(int amount) {
        m_curHealth += amount;
        m_curHealth = Mathf.Min(m_curHealth, m_totalHealth);
    }

    public virtual void damage(int d) {
        m_curHealth -= d;
        m_curHealth = Mathf.Max(0, m_curHealth);
    }

    protected virtual void Start() {
        m_totalHealth = Mathf.Max(1, m_totalHealth);
        m_curHealth = m_totalHealth;
    }
}
