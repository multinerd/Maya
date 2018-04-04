using System;

namespace Maya.System
{
    public class EnumHelper
    {
        static Random RNG = new Random();

        /// <summary>
        /// Randomly select a value from an Enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T RandomEnum<T>()
        {
            var type = typeof(T);
            var values = Enum.GetValues(type);
            lock (RNG)
            {
                var value = values.GetValue(RNG.Next(values.Length));
                return (T)Convert.ChangeType(value, type);
            }
        }

    }
}
