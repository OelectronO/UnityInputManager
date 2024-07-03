using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static Codice.Client.Commands.WkTree.WorkspaceTreeNode;
using static DebugRefInputValueMono;
using static MoveCubeFromInpuMono;

public class InputCustomManager : MonoBehaviour
{
    public List<DebugInputActionReference> m_customInput = new List<DebugInputActionReference>();

    [System.Serializable]
    public class DebugInputActionReference
    {
        public string m_description = "Reminder...";

        public InputActionReference m_input1;
        private DebugInputActionReference m_input2;

        public bool m_currentValue;

        public UnityEvent<bool> m_onValueChanged;
        public UnityEvent m_onTrue;
        public UnityEvent m_onFalse;

        public void SetNewValue(bool newValue)
        {
            bool changed = m_currentValue != newValue;
            m_currentValue = newValue;

            if (changed)
            {

                m_onValueChanged.Invoke(m_currentValue);
                if (m_currentValue)
                {
                    m_onTrue.Invoke();
                }
                else
                {
                    m_onFalse.Invoke();
                }
            }

        }

        public void OnEnable()
        {
            //Forward
            m_input1.action.Enable();
            m_input1.action.performed += Thruster;
            m_input1.action.canceled += Thruster;
        }

        private void Thruster(InputAction.CallbackContext context) => m_input2.SetNewValue(context.ReadValueAsButton());
    }
}