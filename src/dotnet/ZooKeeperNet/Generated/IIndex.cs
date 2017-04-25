namespace Org.Apache.Jute
{
    using System;

    public interface IIndex
    {
        bool Done();
        void Incr();
    }
}

