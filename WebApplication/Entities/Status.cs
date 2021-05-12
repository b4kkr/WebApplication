using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Entities
{
    public enum Status
    {
        AddedForService,
        WaitingForParts,
        WorkingOnCarNow,
        Finished
    }
}