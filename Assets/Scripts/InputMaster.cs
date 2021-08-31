// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""player"",
            ""id"": ""435129f7-8d39-4cea-ab7f-d768f5e1e97a"",
            ""actions"": [
                {
                    ""name"": ""record"",
                    ""type"": ""Value"",
                    ""id"": ""61f3fc87-49f0-4eb7-8f01-fb831a92c089"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""fireRightPressed"",
                    ""type"": ""Value"",
                    ""id"": ""afd472ac-bf53-4ae6-9024-bbfb6ac538ed"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""fireLeftPressed"",
                    ""type"": ""Button"",
                    ""id"": ""a52d0df6-4b80-4022-9821-7a2968506396"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5e62c8fd-d4a5-4a84-91ec-6b5e59f78d41"",
                    ""path"": ""<XRController>{RightHand}/triggerPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""record"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""891e0200-5343-42e6-8a12-6794b2221a19"",
                    ""path"": ""<XRController>{RightHand}/gripPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""fireRightPressed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d3df6bf4-31ec-4381-b8d4-bdef61baafe6"",
                    ""path"": ""<XRController>{LeftHand}/gripPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""fireLeftPressed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""right hand "",
            ""bindingGroup"": ""right hand "",
            ""devices"": [
                {
                    ""devicePath"": ""<XRController>{RightHand}"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // player
        m_player = asset.FindActionMap("player", throwIfNotFound: true);
        m_player_record = m_player.FindAction("record", throwIfNotFound: true);
        m_player_fireRightPressed = m_player.FindAction("fireRightPressed", throwIfNotFound: true);
        m_player_fireLeftPressed = m_player.FindAction("fireLeftPressed", throwIfNotFound: true);
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

    // player
    private readonly InputActionMap m_player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_player_record;
    private readonly InputAction m_player_fireRightPressed;
    private readonly InputAction m_player_fireLeftPressed;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @record => m_Wrapper.m_player_record;
        public InputAction @fireRightPressed => m_Wrapper.m_player_fireRightPressed;
        public InputAction @fireLeftPressed => m_Wrapper.m_player_fireLeftPressed;
        public InputActionMap Get() { return m_Wrapper.m_player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @record.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRecord;
                @record.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRecord;
                @record.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRecord;
                @fireRightPressed.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireRightPressed;
                @fireRightPressed.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireRightPressed;
                @fireRightPressed.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireRightPressed;
                @fireLeftPressed.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireLeftPressed;
                @fireLeftPressed.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireLeftPressed;
                @fireLeftPressed.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireLeftPressed;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @record.started += instance.OnRecord;
                @record.performed += instance.OnRecord;
                @record.canceled += instance.OnRecord;
                @fireRightPressed.started += instance.OnFireRightPressed;
                @fireRightPressed.performed += instance.OnFireRightPressed;
                @fireRightPressed.canceled += instance.OnFireRightPressed;
                @fireLeftPressed.started += instance.OnFireLeftPressed;
                @fireLeftPressed.performed += instance.OnFireLeftPressed;
                @fireLeftPressed.canceled += instance.OnFireLeftPressed;
            }
        }
    }
    public PlayerActions @player => new PlayerActions(this);
    private int m_righthandSchemeIndex = -1;
    public InputControlScheme righthandScheme
    {
        get
        {
            if (m_righthandSchemeIndex == -1) m_righthandSchemeIndex = asset.FindControlSchemeIndex("right hand ");
            return asset.controlSchemes[m_righthandSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnRecord(InputAction.CallbackContext context);
        void OnFireRightPressed(InputAction.CallbackContext context);
        void OnFireLeftPressed(InputAction.CallbackContext context);
    }
}
