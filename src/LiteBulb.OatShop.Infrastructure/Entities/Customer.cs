﻿using System.Collections.ObjectModel;
using LiteBulb.OatShop.SharedKernel.Entities;

namespace LiteBulb.OatShop.Infrastructure.Entities;
public class Customer : Entity, IEntity<int>
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MobilePhone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Line1 { get; set; } = string.Empty;
    public string Line2 { get; set; } = string.Empty;
    public string Line3 { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string County { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public virtual ICollection<Order> Orders { get; set; } = new Collection<Order>();
}
