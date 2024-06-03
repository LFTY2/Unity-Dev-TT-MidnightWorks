using System;

namespace Utils
{
    public static class ActionExtentions
    {
        public static void SafeInvoke<T>(this Action<T> invocationTarget, T arg)
        {
            if (null != invocationTarget)
            {
                invocationTarget.Invoke(arg);
            }
        }

        public static void SafeInvoke<T1, T2>(this Action<T1, T2> invocationTarget, T1 arg1, T2 arg2)
        {
            if (null != invocationTarget)
            {
                invocationTarget.Invoke(arg1, arg2);
            }
        }
    }
}