using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : UnitController {
    public enum CharacterState { MOVING, IDLING }

    private static int MOUSE_RIGHT_BUTTON = 1;
    private static Vector3 CAMERA_POS = new Vector3(0, 10, -10);

    #region // Commented: Outdated movement parameters
    // public float m_moveSpeed;
    // public float m_turnSpeed;
    #endregion
    public Image m_healthBar;
    public BuffTextController m_buffTextController;
    public LayerMask m_groundLayer;

    private CharacterState m_curState;
    private NavMeshAgent m_navAgent;
    private Vector3 m_moveTargetPos;
    private float m_height;
    private bool m_cameraIsFollow;

    private float healthBarFillAmount {
        set {
            if (m_healthBar) {
                m_healthBar.fillAmount = m_curHealth * 1f / m_totalHealth;
            }
        }
    }

    public bool cameraIsFollow {
        set { 
            m_cameraIsFollow = value;
        }
    }

    public override void restoreHealth(int amount) {
        base.restoreHealth(amount);
        healthBarFillAmount = m_curHealth * 1f / m_totalHealth;
        if (m_buffTextController && m_curHealth < m_totalHealth) {
            m_buffTextController.genBuffText("+" + amount.ToString());
        }
    }

    public override void damage(int d) {
        base.damage(d);
        healthBarFillAmount = m_curHealth * 1f / m_totalHealth;
        if (m_buffTextController && m_curHealth > 0) {
            m_buffTextController.genBuffText("-" + d.ToString());
        }
    }

    private void setCameraFollow() {
        if (m_cameraIsFollow) {
            Camera.main.transform.position = transform.position + transform.forward * CAMERA_POS.z + new Vector3(0, CAMERA_POS.y, 0) - new Vector3(0, m_height, 0);
            Camera.main.transform.forward = transform.position - Camera.main.transform.position;
        } else {
            Camera.main.transform.position = transform.position + CAMERA_POS - new Vector3(0, m_height, 0);
            Camera.main.transform.forward = transform.position - Camera.main.transform.position;
        }
    }

    protected override void Start() {
        base.Start();
        m_curState = CharacterState.IDLING;
        m_navAgent = GetComponent<NavMeshAgent>();
        m_moveTargetPos = Vector3.zero;
        m_height = transform.position.y;
    }

    void Update() {
        if (Input.GetMouseButton(MOUSE_RIGHT_BUTTON)) {
            Vector3 mousePos = Input.mousePosition;
            Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);
            #region // Commented: Manual calculating target position
            /*
            Vector3 rayOriginProject = new Vector3(mouseRay.origin.x, 0, mouseRay.origin.z);
            float height = Vector3.Distance(mouseRay.origin, rayOriginProject);
            Vector3 heightRay = rayOriginProject - mouseRay.origin;
            float cosTheta = Vector3.Dot(mouseRay.direction, heightRay) / height;
            float distance = height / cosTheta;
            m_moveTargetPos = mouseRay.origin + mouseRay.direction * distance + new Vector3(0, m_height, 0);
            */
            #endregion
            RaycastHit hit;
            if (Physics.Raycast(mouseRay, out hit, 100f, m_groundLayer)) {
                m_moveTargetPos = hit.point;
            }

            m_curState = CharacterState.MOVING;
        }

        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        print(new Vector2(x, y));

        // State judge
        if (m_curState == CharacterState.MOVING) {
            #region // Commented: Manipulate the position and facing direction directly
            // transform.position = Vector3.MoveTowards(transform.position, m_moveTargetPos, m_moveSpeed * Time.deltaTime);
            // transform.forward = Vector3.Slerp(transform.forward, m_moveTargetPos - transform.position, m_turnSpeed * Time.deltaTime);
            #endregion
            if (m_navAgent) { m_navAgent.SetDestination(m_moveTargetPos); }

            if (Vector3.Distance(transform.position, m_moveTargetPos) < 0.1f) { // Reached target pos
                m_curState = CharacterState.IDLING;
            }
        }

        setCameraFollow();
    }
}
