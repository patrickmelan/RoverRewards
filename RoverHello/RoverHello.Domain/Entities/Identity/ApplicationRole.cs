﻿using Microsoft.AspNetCore.Identity;

namespace RoverHello.Domain.Entities.Identity;

public class ApplicationRole : IdentityRole<string>
{
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }

}