// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControlSystem : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public @PlayerControlSystem()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""PlayerControls"",
            ""id"": ""5a6b1cee-dc61-4691-8305-4aa99fd565b9"",
            ""actions"": [
                {
                    ""name"": ""RotateLeftArm"",
                    ""type"": ""Value"",
                    ""id"": ""a157c614-169e-477c-9eeb-14a6d7e59fd1"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateRightArm"",
                    ""type"": ""Value"",
                    ""id"": ""310fbf80-2e16-496f-9f7c-1cf79863d0a0"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftShoot"",
                    ""type"": ""Button"",
                    ""id"": ""9912880d-4c10-46f1-afa7-b659305d4e79"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightShoot"",
                    ""type"": ""Button"",
                    ""id"": ""eafe79ba-f567-4ad7-a801-430924bb77b9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AdvanceDialog"",
                    ""type"": ""Button"",
                    ""id"": ""a1effcfa-8373-4e01-98cf-174408eb5ecd"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""07b6fbf7-aba0-458c-9996-54f6dd735196"",
                    ""path"": ""<DualShockGamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateLeftArm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c7684b3-f6b2-4cdb-8785-1ded4da0b848"",
                    ""path"": ""<DualShockGamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateRightArm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""51e1154c-ce98-4c8e-9ca3-b43de1ed48d4"",
                    ""path"": ""<DualShockGamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftShoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a9c41a4c-4db0-406b-9a28-87774c151bcb"",
                    ""path"": ""<DualShockGamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightShoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fde52434-8ba0-47be-b14c-1d15437ebdce"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AdvanceDialog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerControls
        m_PlayerControls = asset.FindActionMap("PlayerControls", throwIfNotFound: true);
        m_PlayerControls_RotateLeftArm = m_PlayerControls.FindAction("RotateLeftArm", throwIfNotFound: true);
        m_PlayerControls_RotateRightArm = m_PlayerControls.FindAction("RotateRightArm", throwIfNotFound: true);
        m_PlayerControls_LeftShoot = m_PlayerControls.FindAction("LeftShoot", throwIfNotFound: true);
        m_PlayerControls_RightShoot = m_PlayerControls.FindAction("RightShoot", throwIfNotFound: true);
        m_PlayerControls_AdvanceDialog = m_PlayerControls.FindAction("AdvanceDialog", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // PlayerControls
    private readonly InputActionMap m_PlayerControls;
    private IPlayerControlsActions m_PlayerControlsActionsCallbackInterface;
    private readonly InputAction m_PlayerControls_RotateLeftArm;
    private readonly InputAction m_PlayerControls_RotateRightArm;
    private readonly InputAction m_PlayerControls_LeftShoot;
    private readonly InputAction m_PlayerControls_RightShoot;
    private readonly InputAction m_PlayerControls_AdvanceDialog;
    public struct PlayerControlsActions
    {
        private @PlayerControlSystem m_Wrapper;
        public PlayerControlsActions(@PlayerControlSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @RotateLeftArm => m_Wrapper.m_PlayerControls_RotateLeftArm;
        public InputAction @RotateRightArm => m_Wrapper.m_PlayerControls_RotateRightArm;
        public InputAction @LeftShoot => m_Wrapper.m_PlayerControls_LeftShoot;
        public InputAction @RightShoot => m_Wrapper.m_PlayerControls_RightShoot;
        public InputAction @AdvanceDialog => m_Wrapper.m_PlayerControls_AdvanceDialog;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControlsActions instance)
        {
            if (m_Wrapper.m_PlayerControlsActionsCallbackInterface != null)
            {
                @RotateLeftArm.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRotateLeftArm;
                @RotateLeftArm.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRotateLeftArm;
                @RotateLeftArm.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRotateLeftArm;
                @RotateRightArm.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRotateRightArm;
                @RotateRightArm.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRotateRightArm;
                @RotateRightArm.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRotateRightArm;
                @LeftShoot.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLeftShoot;
                @LeftShoot.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLeftShoot;
                @LeftShoot.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLeftShoot;
                @RightShoot.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRightShoot;
                @RightShoot.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRightShoot;
                @RightShoot.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRightShoot;
                @AdvanceDialog.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAdvanceDialog;
                @AdvanceDialog.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAdvanceDialog;
                @AdvanceDialog.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAdvanceDialog;
            }
            m_Wrapper.m_PlayerControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @RotateLeftArm.started += instance.OnRotateLeftArm;
                @RotateLeftArm.performed += instance.OnRotateLeftArm;
                @RotateLeftArm.canceled += instance.OnRotateLeftArm;
                @RotateRightArm.started += instance.OnRotateRightArm;
                @RotateRightArm.performed += instance.OnRotateRightArm;
                @RotateRightArm.canceled += instance.OnRotateRightArm;
                @LeftShoot.started += instance.OnLeftShoot;
                @LeftShoot.performed += instance.OnLeftShoot;
                @LeftShoot.canceled += instance.OnLeftShoot;
                @RightShoot.started += instance.OnRightShoot;
                @RightShoot.performed += instance.OnRightShoot;
                @RightShoot.canceled += instance.OnRightShoot;
                @AdvanceDialog.started += instance.OnAdvanceDialog;
                @AdvanceDialog.performed += instance.OnAdvanceDialog;
                @AdvanceDialog.canceled += instance.OnAdvanceDialog;
            }
        }
    }
    public PlayerControlsActions @PlayerControls => new PlayerControlsActions(this);
    public interface IPlayerControlsActions
    {
        void OnRotateLeftArm(InputAction.CallbackContext context);
        void OnRotateRightArm(InputAction.CallbackContext context);
        void OnLeftShoot(InputAction.CallbackContext context);
        void OnRightShoot(InputAction.CallbackContext context);
        void OnAdvanceDialog(InputAction.CallbackContext context);
    }
}
