using System;
using System.Reflection;

namespace Studious.SingletonSystem
{
    public class SingletonInstance
    {
        public MethodInfo ScriptType;
        public SingletonAttribute SingletonAttribute;
        public Type Type;
        public object SingletonClass;

        public SingletonInstance(MemberInfo scriptType, SingletonAttribute singletonAttribute, Type type, object instance)
        {
            ScriptType = (MethodInfo)scriptType;
            SingletonAttribute = singletonAttribute;
            Type = type;
            SingletonClass = instance;
        }
    }
}
