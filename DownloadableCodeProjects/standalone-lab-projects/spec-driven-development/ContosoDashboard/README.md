# ContosoDashboard Application

A comprehensive internal web application for Contoso Corporation employees to manage tasks, projects, and team collaboration.

## Overview

ContosoDashboard is built using ASP.NET Core 8.0 with Blazor Server and provides a centralized platform for:
- Task management and tracking
- Project oversight and collaboration
- Team coordination
- Notifications and announcements
- User profile management

## Features

### ✅ Implemented Features

- **Dashboard Home Page**: Personalized dashboard with summary cards showing active tasks, due dates, projects, and notifications
- **Task Management**: View, filter, sort, and update tasks with priority levels and status tracking
- **Project Management**: Browse projects with completion percentages, team members, and status indicators
- **Project Details**: Comprehensive project view with task list, team members, and project statistics
- **Team Directory**: Browse team members by department with status, roles, and contact information
- **Notifications Center**: View and manage all notifications with read/unread status and priority badges
- **User Profile**: Update personal information, availability status, and notification preferences
- **Data Models**: Complete entity framework models for Users, Tasks, Projects, Notifications, and Announcements
- **Business Services**: Service layer for all core functionality (Tasks, Projects, Users, Notifications, Dashboard)
- **Database Context**: EF Core DbContext with relationships, indexes, and seed data

### 🔧 Technical Stack

- **Framework**: ASP.NET Core 8.0
- **UI**: Blazor Server
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: Microsoft Identity (Microsoft Entra ID integration ready)
- **Styling**: Bootstrap 5.3 with Bootstrap Icons
- **Architecture**: Clean separation of concerns with Models, Services, Data, and Pages layers

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- SQL Server or SQL Server LocalDB
- Visual Studio 2022 or Visual Studio Code

### Installation

1. **Clone or navigate to the project directory**:
   ```powershell
   cd ContosoDashboard
   ```

2. **Update the database connection string** in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ContosoDashboard;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

3. **Restore NuGet packages**:
   ```powershell
   dotnet restore
   ```

4. **Run the application**:
   ```powershell
   dotnet run
   ```

5. **Open your browser** and navigate to:
   ```
   http://localhost:5000
   ```
   
   Note: The application runs on HTTP in Development mode for easier local testing.

### Database Setup

The application automatically creates and seeds the database on first run with:
- 4 sample users (Admin, Project Manager, Team Lead, Employee)
- 1 sample project
- 3 sample tasks
- Project member assignments
- A welcome announcement

## Project Structure

```
ContosoDashboard/
├── Data/
│   └── ApplicationDbContext.cs      # EF Core database context
├── Models/
│   ├── User.cs                      # User entity with roles
│   ├── TaskItem.cs                  # Task entity
│   ├── Project.cs                   # Project entity
│   ├── TaskComment.cs               # Task comments
│   ├── Notification.cs              # User notifications
│   ├── ProjectMember.cs             # Project team members
│   └── Announcement.cs              # System announcements
├── Services/
│   ├── IUserService.cs / UserService.cs
│   ├── ITaskService.cs / TaskService.cs
│   ├── IProjectService.cs / ProjectService.cs
│   ├── INotificationService.cs / NotificationService.cs
│   └── IDashboardService.cs / DashboardService.cs
├── Pages/
│   ├── Index.razor                  # Dashboard home page
│   ├── Tasks.razor                  # Task list and management
│   ├── Projects.razor               # Project list view
│   ├── ProjectDetails.razor         # Individual project details
│   ├── Team.razor                   # Team member directory
│   ├── Notifications.razor          # Notification center
│   ├── Profile.razor                # User profile page
│   └── _Host.cshtml                 # Blazor host page
├── Shared/
│   ├── MainLayout.razor             # Main layout template
│   └── NavMenu.razor                # Navigation sidebar
├── wwwroot/
│   └── css/site.css                 # Custom styles
├── Program.cs                       # Application entry point
├── appsettings.json                 # Configuration
└── ContosoDashboard.csproj          # Project file
```

## Configuration

### Authentication Setup (Optional)

To enable Microsoft Entra ID authentication:

1. Update `appsettings.json` with your Azure AD details:
   ```json
   "AzureAd": {
     "Instance": "https://login.microsoftonline.com/",
     "Domain": "yourdomain.onmicrosoft.com",
     "TenantId": "your-tenant-id",
     "ClientId": "your-client-id",
     "CallbackPath": "/signin-oidc"
   }
   ```

2. Uncomment authentication configuration in `Program.cs`:
   ```csharp
   builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
       .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));
   
   // Later in the pipeline:
   app.UseAuthentication();
   ```

### User Roles

The application supports four role levels:
- **Employee**: Standard user access
- **TeamLead**: Can view team member activities
- **ProjectManager**: Can create projects and assign tasks
- **Administrator**: Full system access

## Sample Data

The application includes pre-seeded data for testing:

**Users**:
- admin@contoso.com (Administrator)
- camille.nicole@contoso.com (Project Manager) - Camille Nicole
- floris.kregel@contoso.com (Team Lead) - Floris Kregel
- ni.kang@contoso.com (Software Engineer) - Ni Kang - *Default demo user (UserId: 4)*

**Project**:
- "ContosoDashboard Development" with 3 tasks

## Key Functionalities

### Dashboard (Home Page)
- Summary cards with real-time metrics
- Active announcements display
- Quick action links
- Recent notifications feed

### Task Management
- Filter by status, priority, and project
- Quick status updates via dropdown
- Priority-based color coding
- Overdue task highlighting

### Project Management
- Project cards with progress bars
- Completion percentage calculation
- Team member visibility
- Status badges

### User Profile
- Profile information editing
- Availability status management
- Notification preferences
- Display initials when no photo is set

## Future Enhancements

Based on the specification, the following features can be added:
- Real-time notifications with SignalR
- Task comment display and editing UI
- Project timeline/Gantt chart view
- Advanced search functionality
- Reports and analytics dashboard
- Email notification integration
- File attachments for tasks
- Drag-and-drop task prioritization

## Compliance & Security

The application architecture supports:
- Role-based access control (RBAC)
- WCAG 2.1 Level AA accessibility standards
- TLS encryption for data transmission
- Secure authentication via Microsoft Entra ID
- Audit logging capabilities

## Performance Considerations

- Database indexes on frequently queried fields
- Eager loading with Include() to prevent N+1 queries
- Async/await pattern throughout for scalability
- Optimized for 1,000+ concurrent users (as per spec)

## Contributing

This application was built according to the ContosoDashboard Application Specification v1.0.

## License

Internal use only - Contoso Corporation

## Support

For issues or questions, contact the development team.
