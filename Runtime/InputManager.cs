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
    public InputActionReference m_thruster;
    public BoolChangeEvent m_isThruster;

    //Yaw
    public InputActionReference m_yaw;
    public BoolChangeEvent m_isYaw;

    //Pitch
    public InputActionReference m_pitch;
    public BoolChangeEvent m_isPitch;

    //Roll
    public InputActionReference m_roll;
    public BoolChangeEvent m_isRoll;


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
        m_thruster.action.Enable();
        m_thruster.action.performed += Thruster;
        m_thruster.action.canceled += Thruster;

        //Yaw
        m_yaw.action.Enable();
        m_yaw.action.performed += Yaw;
        m_yaw.action.canceled += Yaw;

        //Pitch
        m_pitch.action.Enable();
        m_pitch.action.performed += Pitch;
        m_pitch.action.canceled += Pitch;

        //Roll
        m_roll.action.Enable();
        m_roll.action.performed += Roll;
        m_roll.action.canceled += Roll;
    }


    private void Thruster(InputAction.CallbackContext context) => m_isThruster.SetNewValue(context.ReadValueAsButton());
    private void Yaw(InputAction.CallbackContext context) => m_isYaw.SetNewValue(context.ReadValueAsButton());
    private void Pitch(InputAction.CallbackContext context) => m_isPitch.SetNewValue(context.ReadValueAsButton());
    private void Roll(InputAction.CallbackContext context) => m_isRoll.SetNewValue(context.ReadValueAsButton());
}
