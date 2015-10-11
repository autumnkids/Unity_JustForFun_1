using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class RegionTrigger : MonoBehaviour {
    public enum RegionTriggerType { SHOW_TEXT, DAMAGE, RESTORE }

    public RegionTriggerType m_type;
    public bool m_hasTriggerText;
    public string m_regionName;

    private GameObject m_mainTitleTextObj;

    protected virtual void Start() {
        m_mainTitleTextObj = GameObject.FindGameObjectWithTag("MainTitleText");
    }

    protected virtual void OnTriggerEnter(Collider other) {
        if (m_mainTitleTextObj && m_hasTriggerText) {
            TriggerText triggerText = m_mainTitleTextObj.GetComponent<TriggerText>();
            if (triggerText) {
                triggerText.showText(m_regionName);
            }
        }
    }

    protected virtual void OnTriggerStay(Collider other) { }
}
