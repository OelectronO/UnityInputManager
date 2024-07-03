using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static Codice.Client.Commands.WkTree.WorkspaceTreeNode;

public class MoveCubeFromInpuMono : MonoBehaviour
{
    //Thruster
    public InputActionReference m_input;
    public BoolChangeEvent m_isInput;


    [System.Serializable]
    public class BoolChangeEvent
    {
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
    }

    public void OnEnable()
    {
        //Forward
        m_input.action.Enable();
        m_input.action.performed += Thruster;
        m_input.action.canceled += Thruster;
    }


    private void Thruster(InputAction.CallbackContext context) => m_isInput.SetNewValue(context.ReadValueAsButton());
}
