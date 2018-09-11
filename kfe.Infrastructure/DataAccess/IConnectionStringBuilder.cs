using System;
using System.Collections.Generic;
using System.Text;

namespace kfe.Infrastructure.DataAccess
{
    public interface IConnectionStringBuilder
    {
        string Build();
        string Source { get; }
    }
}
