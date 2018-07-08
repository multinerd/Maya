using System;

namespace Multinerd.Extensions
{
    /* SAMPLE USAGE
     * class A { string Name { get; } }
     * class B : A { string LongName { get; } }
     * class C : A { string FullName { get; } }
     * class X { public string ToString(IFormatProvider provider); }
     * class Y { public string GetIdentifier(); }
     * 
     * public string GetName(object value)
     * {
         * string name = null;
         * TypeSwitch.On(value)
             * .Case((C x) => name = x.FullName)
             * .Case((B x) => name = x.LongName)
             * .Case((A x) => name = x.Name)
             * .Case((X x) => name = x.ToString(CultureInfo.CurrentCulture))
             * .Case((Y x) => name = x.GetIdentifier())
             * .Default((x) => name = x.ToString());
         * return name;
     * }
     */
    public static class TypeSwitch
    {
        public static Switch<TSource> On<TSource>(TSource value)
        {
            return new Switch<TSource>(value);
        }

        public sealed class Switch<TSource>
        {
            private readonly TSource value;
            private bool handled = false;

            internal Switch(TSource value)
            {
                this.value = value;
            }

            public Switch<TSource> Case<TTarget>(Action<TTarget> action) where TTarget : TSource
            {
                if (!this.handled && this.value is TTarget)
                {
                    action((TTarget)this.value);
                    this.handled = true;
                }
                return this;
            }

            public void Default(Action<TSource> action)
            {
                if (!this.handled)
                    action(this.value);
            }
        }
    }
}
