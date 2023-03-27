using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsLetter.Core.Entities
{
    [CollectionName("Users")]
    public class User : MongoIdentityUser<Guid>
    {
    }
}
