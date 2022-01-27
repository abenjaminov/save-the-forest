using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.ScriptableObjects.Channels.Input.Models;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Input Channel", menuName = "Channels/Input Channel", order = 2)]
    public class InputChannel : ScriptableObject
    {
        public Dictionary<InputActionTypes, UnityAction<InputActionOptions>> MappedActions = new Dictionary<InputActionTypes, UnityAction<InputActionOptions>>();

        public void OnAction(InputActionTypes inputActionType, InputActionOptions options)
        {
            var action = MappedActions[inputActionType];
            action?.Invoke(options);
        }

        public InputActionSubscription SubscribeAction(InputActionTypes inputActionType, UnityAction<InputActionOptions> action)
        {
            if (MappedActions.TryGetValue(inputActionType, out var keyEvent))
            {
                MappedActions[inputActionType] += action;
            }
            else
            {
                keyEvent += action;
                MappedActions.Add(inputActionType, keyEvent);
            }

            return new InputActionSubscription(this, inputActionType, action);
        }
    }
    
    public class InputActionSubscription
    {
        protected InputActionTypes _code;
        protected UnityAction<InputActionOptions> _action;
        protected InputChannel _channel;
        
        public InputActionSubscription(InputChannel channel, InputActionTypes code, UnityAction<InputActionOptions> action)
        {
            _code = code;
            _action = action;
            _channel = channel;
        }

        public void Unsubscribe()
        {
            _channel.MappedActions[_code] -= _action;
        }
    }
}