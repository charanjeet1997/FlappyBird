using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VariableEvents
{
    public class Variable<T> : ScriptableObject
    {
        public T Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
                CallAction(_value);
            }
        }

        private T _value;
        
        private List<Action<T>> actions;

        public void BindVariable(Action<T> action)
        {
            if (action == null)
            {
                return;
            }

            if (actions == null)
            {
                actions = new List<Action<T>>();
            }

            if (actions.Contains(action))
            {
                return;
            }
            
            actions.Add(action);
        }

        public void UnbindVariable(Action<T> action)
        {
            if (action == null)
            {
                return;
            }
            if (actions == null)
            {
                actions = new List<Action<T>>();
            }

            if (actions.Contains(action))
            {
                actions.Remove(action);
                return;
            }
        }

        public void CallAction(T value)
        {
            if (actions != null)
            {
                for (int indexOfVariabe = 0; indexOfVariabe < actions.Count; indexOfVariabe++)
                {
                    actions[indexOfVariabe]?.Invoke(value);
                }
            }
        }
    }
}