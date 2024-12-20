﻿using Microsoft.AspNetCore.Identity;

namespace Fiorello.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public string? ConnectionId { get; set; }
    }
}
