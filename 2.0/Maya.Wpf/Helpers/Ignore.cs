using System;

namespace Maya.WPF.Helpers
{
    public static class Ignore
    {
        public static void IgnoreExceptions(Action act)
        {
            try
            {
                act.Invoke();
            }
            catch
            {
                //
            }
        }
    }
}
