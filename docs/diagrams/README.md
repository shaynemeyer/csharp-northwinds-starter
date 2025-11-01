# Architecture Diagrams - C# Northwind Workshop

This directory contains comprehensive architecture diagrams for the C# Northwind Workshop project. These diagrams illustrate the system architecture, data flow, design patterns, and project structure.

## Overview

The C# Northwind Workshop is a three-layer ASP.NET Core application that demonstrates Clean Architecture principles, SOLID design, and professional software engineering practices. These diagrams help visualize how the components work together.

## Diagram Index

### 1. [Layered Architecture](./01-layered-architecture.md)
**Purpose**: Shows the three-layer architecture and separation of concerns

**Key Concepts**:
- Presentation Layer (Web)
- Domain Layer (Core)
- Data Access Layer (Infrastructure)
- Dependency flow and the Dependency Inversion Principle
- How layers communicate through interfaces

**When to Reference**:
- Understanding the overall system architecture
- Learning about Clean Architecture
- Planning where to add new features
- Explaining SOLID principles (especially Dependency Inversion)

---

### 2. [Entity Relationship Diagram (ERD)](./02-entity-relationship-diagram.md)
**Purpose**: Visualizes the database schema and relationships between entities

**Key Concepts**:
- All domain entities (Customer, Product, Order, etc.)
- One-to-Many and Many-to-Many relationships
- Primary keys and foreign keys
- Entity properties and navigation properties
- Business rules and constraints
- Domain model inheritance from BaseEntity

**When to Reference**:
- Understanding the data model
- Designing new features that involve multiple entities
- Writing LINQ queries with joins
- Creating database migrations
- Understanding EF Core navigation properties

---

### 3. [Data Flow Diagram](./03-data-flow-diagram.md)
**Purpose**: Traces the complete request/response lifecycle from browser to database

**Key Concepts**:
- HTTP request handling
- ASP.NET Core middleware pipeline
- Razor Pages and PageModels
- Repository pattern in action
- EF Core query execution
- Data transformation (Database → Entity → ViewModel → HTML)
- Dependency Injection flow
- Async/await execution model

**When to Reference**:
- Debugging request handling issues
- Understanding how data flows through the application
- Learning async/await patterns
- Optimizing performance
- Understanding dependency injection

---

### 4. [Repository Pattern](./04-repository-pattern.md)
**Purpose**: Deep dive into the Repository Pattern implementation

**Key Concepts**:
- Generic IRepository<T> interface
- Specific repository interfaces (ICustomerRepository, etc.)
- Repository implementations
- Benefits over direct DbContext usage
- Testing with mocked repositories
- Dependency Injection registration
- Unit of Work pattern (advanced)

**When to Reference**:
- Implementing new repositories
- Writing unit tests
- Understanding abstraction layers
- Learning SOLID principles
- Comparing with and without repository pattern

---

### 5. [Project Structure](./05-project-structure.md)
**Purpose**: Detailed breakdown of all projects, folders, and key files

**Key Concepts**:
- Complete directory structure for all three projects
- Purpose of each file and folder
- Project dependencies
- Build and deployment process
- File organization best practices

**When to Reference**:
- Setting up the solution from scratch
- Understanding where to place new files
- Navigating the codebase
- Understanding project references
- Planning deployment

---

## Learning Path

For students working through the workshop, we recommend studying these diagrams in this order:

1. **Start with Project Structure** - Get familiar with where everything lives
2. **Then Layered Architecture** - Understand the big picture
3. **Study Entity Relationships** - Learn the domain model
4. **Trace Data Flow** - See how a request works end-to-end
5. **Deep Dive into Repository Pattern** - Understand the abstraction layer

## Quick Reference Guide

### I want to understand...

| Goal | Diagram to Reference |
|------|---------------------|
| Overall architecture | Layered Architecture |
| Database design | Entity Relationship Diagram |
| How requests are handled | Data Flow Diagram |
| How to implement repositories | Repository Pattern |
| Where to add new code | Project Structure |
| Dependency injection | Data Flow Diagram + Layered Architecture |
| Testing strategy | Repository Pattern |
| SOLID principles | Layered Architecture + Repository Pattern |

## Technology Stack

These diagrams illustrate the following technologies:

- **Framework**: ASP.NET Core 9.0
- **Language**: C# 13
- **ORM**: Entity Framework Core 9.0
- **Database**: SQLite
- **UI**: Razor Pages with Tailwind CSS 4.0
- **Patterns**: Repository Pattern, Dependency Injection, Clean Architecture

## Architecture Principles Demonstrated

1. **Separation of Concerns**: Each layer has a single, well-defined responsibility
2. **Dependency Inversion**: High-level modules depend on abstractions, not concrete implementations
3. **Interface Segregation**: Specific repository interfaces for different needs
4. **Single Responsibility**: Each class has one reason to change
5. **Open/Closed**: Open for extension, closed for modification

## Additional Resources

- **Main Workshop Tutorial**: [../CSharp-Northwind-Workshop.md](../CSharp-Northwind-Workshop.md)
- **VSCode Setup**: [../VSCode-QuickStart-Guide.md](../VSCode-QuickStart-Guide.md)
- **Tailwind CSS Setup**: [../Tailwind-CSS-Setup-Guide.md](../Tailwind-CSS-Setup-Guide.md)
- **Technology Comparison**: [../NextJS-vs-CSharp-Comparison.md](../NextJS-vs-CSharp-Comparison.md)

## How These Diagrams Were Created

These diagrams use ASCII art for maximum portability and version control friendliness. They can be viewed in any text editor or terminal and are easy to update as the project evolves.

## Contributing

If you find errors or want to suggest improvements to these diagrams:

1. Review the diagram files (they're just markdown!)
2. Propose changes that improve clarity
3. Keep ASCII art formatting consistent
4. Update this README if adding new diagrams

## Next Steps

After reviewing these diagrams:

1. Follow along with the main workshop tutorial
2. Reference these diagrams as you implement each feature
3. Use them to plan your own features
4. Share them with your team to explain the architecture

---

**Happy Learning!**

For questions or issues, refer to the main [README.md](../../README.md) or the [Workshop Checklist](../Workshop-Checklist.md).
