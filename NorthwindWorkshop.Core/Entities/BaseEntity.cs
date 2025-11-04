namespace NorthwindWorkshop.Core.Entities;

/// <summary>
/// Base class for all entities in the domain
/// Demonstrates OOP principle: Inheritance
/// </summary>
public abstract class BaseEntity
{
    // Primary key - common to all entities
    public int Id { get; set; }
}
