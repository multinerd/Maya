using JetBrains.Annotations;
using System;
using System.Reflection;

namespace Maya.Core.Reflection.Extensions
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public static class TypeExtensions
    {
        public static Assembly GetAssembly(this Type type)
        {
            return type.GetTypeInfo().Assembly;
        }
    }
}
