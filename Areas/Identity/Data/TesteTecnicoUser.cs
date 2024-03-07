using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TesteTecnico.Areas.Identity.Data;

// Add profile data for application users by adding properties to the  class
public class TesteTecnicoUser : IdentityUser
{
    [PersonalData]
    public string? Name { get; set; }
}

