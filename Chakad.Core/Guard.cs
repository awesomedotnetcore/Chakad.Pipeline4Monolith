using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Chakad.Core.Attributes;

namespace Chakad.Core
{
    public static class Guard
    {

        public static void TypeHasDefaultConstructor(Type type, [InvokerParameterName] string argumentName)
        {
            ConstructorInfo[] constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            Func<ConstructorInfo, bool> func = (Func<ConstructorInfo, bool>)(ctor => (uint)ctor.GetParameters().Length > 0U);
            Func<ConstructorInfo, bool> predicate = null;
            if (((IEnumerable<ConstructorInfo>)constructors).All<ConstructorInfo>(predicate))
                throw new ArgumentException(string.Format("Type '{0}' must have a default constructor.", (object)type.FullName), argumentName);
        }

        [ContractAnnotation("value: null => halt")]
        public static void AgainstNull([InvokerParameterName] string argumentName, [NotNull] object value)
        {
            if (value == null)
                throw new ArgumentNullException(argumentName);
        }

        [ContractAnnotation("value: null => halt")]
        public static void AgainstNullAndEmpty([InvokerParameterName] string argumentName,
            [NotNull] string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(argumentName);
        }

        [ContractAnnotation("value: null => halt")]
        public static void AgainstNullAndEmpty([InvokerParameterName] string argumentName, [NoEnumeration, NotNull] ICollection value)
        {
            if (value == null)
                throw new ArgumentNullException(argumentName);
            if (value.Count == 0)
                throw new ArgumentOutOfRangeException(argumentName);
        }

        public static void AgainstNegativeAndZero([InvokerParameterName] string argumentName, int value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(argumentName);
        }

        public static void AgainstNegative([InvokerParameterName] string argumentName, int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(argumentName);
        }

        public static void AgainstNegativeAndZero([InvokerParameterName] string argumentName, TimeSpan value)
        {
            if (value <= TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(argumentName);
        }

        public static void AgainstNegativeAndZero([InvokerParameterName] string argumentName, TimeSpan? value)
        {
            if (!value.HasValue)
                return;
            TimeSpan? nullable = value;
            TimeSpan zero = TimeSpan.Zero;
            if ((nullable.HasValue ? (nullable.GetValueOrDefault() <= zero ? 1 : 0) : 0) != 0)
                throw new ArgumentOutOfRangeException(argumentName);
        }

        public static void AgainstNegative([InvokerParameterName] string argumentName, TimeSpan value)
        {
            if (value < TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(argumentName);
        }
    }
}