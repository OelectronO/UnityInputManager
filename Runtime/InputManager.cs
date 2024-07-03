using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MoveCubeFromInpuMono : MonoBehaviour
{
    //Forward
    public InputActionReference m_moveForward;
    public BoolChangeEvent m_isMovingForward;


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
        m_moveForward.action.Enable();
        m_moveForward.action.performed += MoveForward;
        m_moveForward.action.canceled += MoveForward;
    }


    private void MoveForward(InputAction.CallbackContext context)
    {
        m_isMovingForward.SetNewValue(context.ReadValueAsButton());

    }
}
