using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multinerd.Windows
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
