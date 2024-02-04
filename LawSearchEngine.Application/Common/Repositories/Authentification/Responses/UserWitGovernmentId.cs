using LawSearchEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawSearchEngine.Application.Common.Repositories.Authentification.Responses
{
    public class UserWitGovernmentId
    {
        public User User { get; set; } = null!;
        public Guid? GovernementId { get; set; } = null;
    }
}
