using System.ComponentModel.DataAnnotations;
using Kustomer.Domain.Interfaces;

namespace Kustomer.Domain.Entities;

public class Customer : IAggregateRoot
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public required string LastName { get; set; }
    [EmailAddress]
    public required string Email { get; set; }
    [Phone]
    public required string Phone { get; set; }
}