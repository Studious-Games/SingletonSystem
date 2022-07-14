using System;
using UnityEngine;

namespace Studious.SingletonSystem
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class SingletonAttribute : Attribute
    {
        public SingletonAttribute()
        {
            HideFlags = HideFlags.None;
        }

        public bool Persistent { get; set; }
        public readonly bool Automatic = true;
        public HideFlags HideFlags { get; set; }
        public string Name { get; set; }
        public string Scene { get; set; }
        public string SceneUnload { get; set; }
    }
}