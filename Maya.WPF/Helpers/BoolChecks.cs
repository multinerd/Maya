namespace Maya.WPF.Helpers
{
    public static class BoolChecks
    {
        public static bool IsNot<T>(this object obj)
        {
            return !(obj is T);
        }
    }
}
