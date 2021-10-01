using System;

namespace MyRead.Infra.Exceptions
{
    public class DbNotConfiguredException : Exception
    {
        public DbNotConfiguredException()
            : base("Data base not configured") { }
    }
}