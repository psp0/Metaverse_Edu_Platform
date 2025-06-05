using UnityEngine;

namespace Controller
{
    public class ThirdPersonCamera : PlayerCamera
    {
        private void LateUpdate()
        {
            if (m_Player == null) return;

            Quaternion rotation = Quaternion.Euler(m_Angles.x, m_Angles.y, 0f);
            Vector3 position = m_Player.position - rotation * Vector3.forward * m_Distance;

            m_Transform.position = position;
            m_Transform.rotation = rotation;
        }
    }
}
