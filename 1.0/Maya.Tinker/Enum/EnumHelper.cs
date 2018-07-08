using System;
using System.Text.RegularExpressions;

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



    public static class EnumExtensions
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





        /// <summary>
        /// Converts Enum to a Human Readable String
        /// </summary>
        /// <param name="input"></param>
        /// <returns> A Human Readable String. </returns>
        public static string Wordify(this Enum input)
        {
            Regex r = new Regex("(?<=[a-z])(?<x>[A-Z])|(?<=.)(?<x>[A-Z])(?=[a-z])");
            return r.Replace(input.ToString(), " ${x}");
        }

    }
}
