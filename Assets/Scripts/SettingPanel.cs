using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingPanel : MonoBehaviour {
    public PlayerController m_player;
    public Toggle m_rotateCameraToggle;
    public Button m_applyBtn;
    public Button m_triggerBtn;
    public Slider m_effectTextShowDuration;
    public Slider m_effectTextMoveSpeed;
    public Slider m_buffEffectSpeed;
    public Slider m_fountainWidth;
    public Toggle m_toggleNormalEffect;
    public Toggle m_toggleFountainEffect;
    public BuffTextController m_buffTextController;
    public BuffRegion[] m_buffRegions;

    private Animator m_anim;
    private float m_lastValueDuration;
    private float m_lastValueMoveSpeed;
    private float m_lastValueBuffEffectSpeed;
    private float m_lastValueFountainWidth;
    private bool m_isTriggered;

    public void onClick_ApplyChanges() {
        checkRotateScreen();
        checkBuffEffect();
    }

    public void onClick_TriggerPanel() {
        m_isTriggered = !m_isTriggered;
        m_anim.SetBool(HashIDs.SETTINGPANEL_ISPOPOUT, m_isTriggered);
    }

    public void toggleNormal(bool value) {
        if (value) {
            m_toggleFountainEffect.isOn = false;
        }
    }

    public void toggleFountain(bool value) {
        if (value) {
            m_toggleNormalEffect.isOn = false;
        }
    }

    private void checkRotateScreen() {
        if (m_rotateCameraToggle && m_player) {
            if (m_rotateCameraToggle.isOn) {
                m_player.cameraIsFollow = true;
            } else {
                m_player.cameraIsFollow = false;
            }
        }
    }

    private void checkBuffEffect() {
        if (m_toggleNormalEffect && m_toggleFountainEffect && m_buffTextController) {
            if (m_toggleNormalEffect.isOn) {
                m_buffTextController.m_buffTextEffectType = BuffTextController.BuffTextEffectType.VERTICAL;
            } else if (m_toggleFountainEffect.isOn) {
                m_buffTextController.m_buffTextEffectType = BuffTextController.BuffTextEffectType.FOUNTAIN;
            }
        }
    }

    void Start() {
        m_anim = GetComponent<Animator>();
        m_isTriggered = false;
        m_triggerBtn.onClick.AddListener(onClick_TriggerPanel);
        m_applyBtn.onClick.AddListener(onClick_ApplyChanges);
        checkRotateScreen();

        if (m_effectTextShowDuration && m_buffTextController) {
            m_effectTextShowDuration.value = m_buffTextController.m_showDuration;
            m_lastValueDuration = m_buffTextController.m_showDuration;
        }

        if (m_effectTextMoveSpeed && m_buffTextController) {
            m_effectTextMoveSpeed.value = m_buffTextController.m_showSpeed;
            m_lastValueMoveSpeed = m_buffTextController.m_showSpeed;
        }

        if (m_buffEffectSpeed && m_buffRegions.Length > 0) {
            m_buffEffectSpeed.value = m_buffRegions[0].m_triggerGap;
            m_lastValueBuffEffectSpeed = m_buffRegions[0].m_triggerGap;
        }

        if (m_buffTextController && m_toggleNormalEffect && m_toggleFountainEffect) {
            if (m_buffTextController.m_buffTextEffectType == BuffTextController.BuffTextEffectType.VERTICAL) {
                m_toggleNormalEffect.isOn = true;
                m_toggleFountainEffect.isOn = false;
            } else if (m_buffTextController.m_buffTextEffectType == BuffTextController.BuffTextEffectType.FOUNTAIN) {
                m_toggleFountainEffect.isOn = true;
                m_toggleNormalEffect.isOn = false;
            }

            m_toggleNormalEffect.onValueChanged.AddListener(toggleNormal);
            m_toggleFountainEffect.onValueChanged.AddListener(toggleFountain);
        }

        if (m_buffTextController && m_fountainWidth) {
            m_fountainWidth.value = m_buffTextController.m_fountainWidth;
            m_lastValueFountainWidth = m_buffTextController.m_fountainWidth;
        }
    }

    void Update() {
        if (m_buffTextController && m_effectTextShowDuration && m_lastValueDuration != m_effectTextShowDuration.value) {
            m_buffTextController.m_showDuration = m_effectTextShowDuration.value;
            m_lastValueDuration = m_effectTextShowDuration.value;
        }

        if (m_buffTextController && m_effectTextMoveSpeed && m_lastValueMoveSpeed != m_effectTextMoveSpeed.value) {
            m_buffTextController.m_showSpeed = m_effectTextMoveSpeed.value;
            m_lastValueMoveSpeed = m_effectTextMoveSpeed.value;
        }

        if (m_buffRegions.Length > 0 && m_buffEffectSpeed && m_lastValueBuffEffectSpeed != m_buffEffectSpeed.value) {
            for (int i = 0; i < m_buffRegions.Length; i++) {
                m_buffRegions[i].m_triggerGap = m_buffEffectSpeed.value;
            }
            m_lastValueBuffEffectSpeed = m_buffEffectSpeed.value;
        }

        if (m_buffTextController && m_fountainWidth && m_lastValueFountainWidth != m_fountainWidth.value) {
            m_buffTextController.m_fountainWidth = m_fountainWidth.value;
            m_lastValueFountainWidth = m_fountainWidth.value;
        }
    }
}
