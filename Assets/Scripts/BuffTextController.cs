using UnityEngine;
using Random = UnityEngine.Random;
using System;
using System.Collections;

public class BuffTextController : MonoBehaviour {
    public enum BuffTextEffectType { VERTICAL, FOUNTAIN }

    public BuffTextEffectType m_buffTextEffectType;
    public GameObject m_buffTextPref;
    public float m_showDuration;
    public float m_showSpeed;
    [Range(0, 0.1f)]
    public float m_fountainWidth;
    public float m_longestDistance;

    public void genBuffText(string amount) {
        Action<string> genFunc = genVerticalEffect;
        if (m_buffTextEffectType == BuffTextEffectType.VERTICAL) {
            genFunc = genVerticalEffect;
        } else if (m_buffTextEffectType == BuffTextEffectType.FOUNTAIN) {
            genFunc = genFountainEffect;
        }

        genFunc(amount);
    }

    private void genVerticalEffect(string amount) {
        GameObject buffTextObj = Instantiate(m_buffTextPref, transform.position, Quaternion.identity) as GameObject;
        buffTextObj.transform.SetParent(gameObject.transform);
        buffTextObj.transform.localRotation = Quaternion.identity;
        buffTextObj.transform.localScale = Vector3.one;
        BuffText buffText = buffTextObj.GetComponent<BuffText>();
        buffText.m_showDuration = m_showDuration;
        buffText.m_moveSpeed = m_showSpeed;
        buffText.m_moveAngle = 0f;
        buffText.m_longestDistance = m_longestDistance;
        buffText.m_turnDir = 0;
        buffText.amount = amount;
    }

    private void genFountainEffect(string amount) {
        GameObject buffTextObj = Instantiate(m_buffTextPref, transform.position, Quaternion.identity) as GameObject;
        buffTextObj.transform.SetParent(gameObject.transform);
        buffTextObj.transform.localRotation = Quaternion.identity;
        buffTextObj.transform.localScale = Vector3.one;
        BuffText buffText = buffTextObj.GetComponent<BuffText>();
        buffText.m_showDuration = m_showDuration;
        buffText.m_moveSpeed = m_showSpeed;
        buffText.m_moveAngle = Random.Range(0, m_fountainWidth);
        buffText.m_longestDistance = m_longestDistance;
        buffText.m_turnDir = Random.Range(0, 2);
        buffText.amount = amount;
    }

    void Start() {
        m_showDuration = Mathf.Max(1, m_showDuration);
        m_showSpeed = Mathf.Max(1, m_showSpeed);
        m_fountainWidth = Mathf.Max(0, m_fountainWidth);
        m_longestDistance = Mathf.Max(1, m_longestDistance);
    }
}