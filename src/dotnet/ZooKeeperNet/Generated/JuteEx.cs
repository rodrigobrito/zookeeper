﻿namespace Org.Apache.Jute
{
    using System;
    using System.Runtime.CompilerServices;

    public static class JuteEx
    {
        public static int CompareTo(this byte[] b1, byte[] b2)
        {
            if ((b1 != null) || (b2 != null))
            {
                if ((b1 == null) || (b2 == null))
                {
                    return 0;
                }
                for (int i = 0; i < b1.Length; i++)
                {
                    for (int j = 0; j < b2.Length; j++)
                    {
                        if (i > j)
                        {
                            return 1;
                        }
                        if (i < j)
                        {
                            return -1;
                        }
                    }
                }
            }
            return 0;
        }
    }
}

