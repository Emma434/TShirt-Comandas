# Sistema de Comandas SaaS (TShirt.Comandas)

Este proyecto es el núcleo (Core) de un sistema de comandas en tiempo real, diseñado originalmente para resolver el flujo de producción de poleras personalizadas, pero escalable a cualquier modelo de negocio SaaS (restaurantes, talleres, imprentas).

## 🏗️ Arquitectura del Sistema
El proyecto está construido bajo los estándares más estrictos de la industria moderna:

* **Clean Architecture:** Separación absoluta de responsabilidades en 4 capas (Domain, Application, Infrastructure, API).
* **CQRS (Command Query Responsibility Segregation):** Las intenciones de mutación de estado (Commands) están aisladas mediante **MediatR**, garantizando que la lógica de negocio no se acople a los controladores HTTP.
* **Domain-Driven Design (DDD):** Entidades de dominio ricas (ej. `Comanda`) con encapsulamiento estricto. El estado de las entidades solo muta a través de métodos de fábrica controlados, protegiendo la integridad del negocio.
* **Principio de Inversión de Dependencias (SOLID):** La capa de Aplicación dicta los contratos (Interfaces) y la Infraestructura los implementa, logrando un desacoplamiento total del motor de base de datos.

## 🛠️ Stack Tecnológico
* **Framework:** .NET 9
* **API:** ASP.NET Core Web API + Swagger/OpenAPI
* **Mensajería Interna:** MediatR
* **Persistencia:** Entity Framework Core 9 (Code-First Migrations)
* **Base de Datos:** PostgreSQL (Nativo)

## 🚀 Estado Actual del Desarrollo
- [x] Estructura Clean Architecture inicializada.
- [x] Modelo de Dominio (Máquina de estados de Comanda).
- [x] Infraestructura configurada con PostgreSQL.
- [x] Migraciones de base de datos materializadas (`InitialCreate`).
- [x] Inyección de dependencias modularizada por capas.
- [x] Flujo CQRS base (`CreateComandaCommand` y Handler) conectado al repositorio.
- [ ] Validación de comandos (FluentValidation).
- [ ] Notificaciones en tiempo real (SignalR).