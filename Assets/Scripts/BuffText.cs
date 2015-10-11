using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuffText : MonoBehaviour {
    [HideInInspector]
    public float m_showDuration;
    [HideInInspector]
    public float m_moveSpeed;
    [HideInInspector]
    public float m_moveAngle;
    [HideInInspector]
    public float m_longestDistance;
    [HideInInspector]
    public int m_turnDir;

    private Text m_text;
    private Vector3 m_moveDir;
    private float m_curTime;
    private float m_offset;

    public string amount {
        set {
            if (m_text) {
                m_text.text = value;
            }
        }
    }

    public float alpha {
        set {
            if (m_text) {
                m_text.color = new Color(m_text.color.r, m_text.color.g, m_text.color.b, value);
            }
        }
    }

    void Awake() {
        m_text = GetComponent<Text>();
        m_moveDir = Vector3.up;
        m_curTime = 0f;
        m_moveSpeed = Mathf.Max(1, m_moveSpeed);
        m_moveAngle = Mathf.Max(0, m_moveAngle);
        m_showDuration = Mathf.Max(1, m_showDuration);
        m_longestDistance = Mathf.Max(1, m_longestDistance);
        m_turnDir = 0;
        m_offset = 0f;
    }

    void Update() {
        m_curTime += Time.deltaTime;

        if (m_curTime >= m_showDuration || Vector3.Distance(transform.parent.position, transform.position) >= m_longestDistance) {
            Destroy(gameObject);
        } else {
            transform.position += m_moveDir * m_moveSpeed * Time.deltaTime;
            alpha = 1 - m_curTime / m_showDuration;

            m_offset += 1f;
            if (m_turnDir == 0) {
                m_moveDir = new Vector2(
                    m_moveDir.x * m_offset * Mathf.Cos(m_moveAngle) - m_moveDir.y * m_offset * Mathf.Sin(m_moveAngle),
                    m_moveDir.x * m_offset * Mathf.Sin(m_moveAngle) + m_moveDir.y * m_offset * Mathf.Cos(m_moveAngle)
                );
                m_moveDir.Normalize();
            } else if (m_turnDir == 1) {
                m_moveDir = new Vector2(
                    m_moveDir.x * m_offset * Mathf.Cos(m_moveAngle) + m_moveDir.y * m_offset * Mathf.Sin(m_moveAngle),
                    m_moveDir.y * m_offset * Mathf.Cos(m_moveAngle) - m_moveDir.x * m_offset * Mathf.Sin(m_moveAngle)
                );
                m_moveDir.Normalize();
            }
        }
    }
}
