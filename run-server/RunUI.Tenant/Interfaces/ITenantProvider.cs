using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunUI
{
    public interface ITenantProvider
    {
        string GetTenantId();

        Task<Tenant> GetTenant();
    }
}
