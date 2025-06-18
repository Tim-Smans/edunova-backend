# EduNova Backend

EduNova is a **B2B SaaS e-learningplatform** for IT companies. This backend is built usin **ASP.NET Core 8 Web API** in combination with a **structured layer-architecture**. The application supports **multi-tenant setups**, **JWT-authenticatie**, **User-roles**, **course management**, and expandable modules like **gamification**, **certificates** and eventually **HR-integraties**.

---

## Project Structure

```plaintext
/src
├── EduNova.Api             // Web API project (entrypoint, controllers, middleware)
├── EduNova.Core            // Business logic, DTOs, interfaces
├── EduNova.Infrastructure  // Data access, services (EF Core, JWT, e-mail, etc.)
├── EduNova.Shared          // Reusable code: enums, exceptions, helpers, constants
```