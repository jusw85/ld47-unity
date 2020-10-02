using System;
using System.Collections.Generic;
using UnityEngine;

namespace Jusw85.Common
{
    /// <summary>
    /// https://wiki.unity3d.com/index.php/Toolbox
    /// </summary>
    public class Toolbox : Singleton<Toolbox>
    {
        [SerializeField] private Component[] trackedComponents;
        private readonly Dictionary<Type, Component> components = new Dictionary<Type, Component>();

        // Prevent constructor use.
        protected Toolbox()
        {
        }

        protected override void OnAwake()
        {
            if (trackedComponents == null)
            {
                return;
            }

            foreach (Component c in trackedComponents)
            {
                components.Add(c.GetType(), c);
            }
        }

        public T TryGet<T>() where T : Component
        {
            Component c;
            components.TryGetValue(typeof(T), out c);
            return (T) c;
        }

        // Define all required global components here. These are hard-codded components
        // that will always be added. Unlike the optional components added at runtime.

        // // Used to track any global components added at runtime.
        // private readonly Dictionary<string, Component> m_Components = new Dictionary<string, Component>();
        //
        // // The methods below allow us to add global components at runtime.
        // // TODO: Convert from string IDs to component types.
        //
        // public Component AddGlobalComponent(string componentID, Type component)
        // {
        //     if (m_Components.ContainsKey(componentID))
        //     {
        //         Debug.LogWarning("[Toolbox] Global component ID \""
        //                          + componentID + "\" already exist! Returning that.");
        //         return GetGlobalComponent(componentID);
        //     }
        //
        //     var newComponent = gameObject.AddComponent(component);
        //     m_Components.Add(componentID, newComponent);
        //     return newComponent;
        // }
        //
        //
        // public void RemoveGlobalComponent(string componentID)
        // {
        //     Component component;
        //
        //     if (m_Components.TryGetValue(componentID, out component))
        //     {
        //         Destroy(component);
        //         m_Components.Remove(componentID);
        //     }
        //     else
        //     {
        //         Debug.LogWarning("[Toolbox] Trying to remove nonexistent component ID \""
        //                          + componentID + "\"! Typo?");
        //     }
        // }
        //
        //
        // public Component GetGlobalComponent(string componentID)
        // {
        //     Component component;
        //
        //     if (m_Components.TryGetValue(componentID, out component))
        //     {
        //         return component;
        //     }
        //
        //     Debug.LogWarning("[Toolbox] Global component ID \""
        //                      + componentID + "\" doesn't exist! Typo?");
        //     return null;
        // }
    }
}