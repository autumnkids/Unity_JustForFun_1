using UnityEngine;
using System.Collections;

public class EnemySpawnController : MonoBehaviour {
    public GameObject[] m_enemyPrefs;
    public int m_maxGenAmount;
    public float m_genGap;
    public float m_sqaureWidth;

    private int m_curAmount;
    private float m_curTime;

    void Start() {
        m_curTime = 0f;
        m_curAmount = 0;
    }

    void Update() {
        m_curTime += Time.deltaTime;

        if (m_curTime >= m_genGap && m_curAmount < m_maxGenAmount) {
            m_curTime = 0;
            float offset = Random.Range(-m_sqaureWidth / 2f, m_sqaureWidth / 2f + 1);
            float x = transform.position.x + offset;
            float y = transform.position.y + offset;

            int enemySize = m_enemyPrefs.Length;
            int genIndex = Random.Range(0, enemySize);
            GameObject genObj = Instantiate(m_enemyPrefs[genIndex], new Vector3(x, y, 0), Quaternion.identity) as GameObject;
            genObj.transform.SetParent(transform);
            m_curAmount++;
        }
    }
}
