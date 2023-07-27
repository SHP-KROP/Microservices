﻿using Microsoft.AspNetCore.Identity;

namespace AuthServer
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        
        public string Surname { get; set; }
    }
}
