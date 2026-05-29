# Engine de Comandas Genéricas para Arquitecturas SaaS (TShirt.Comandas)

Este repositorio aloja el Core de un motor de gestión y persistencia de comandas en tiempo real altamente escalable, diseñado bajo los principios de **Clean Architecture** y patrones avanzados de **Domain-Driven Design (DDD)** en el ecosistema **.NET 9**.

El sistema evolucionó estratégicamente desde un modelo originalmente acoplado al flujo de producción textil hacia una infraestructura abstracta y multi-rubro (SaaS Genérico) capaz de procesar pedidos para restaurantes, e-commerce, manufactura o retail sin requerir modificaciones en el esquema relacional rígido de la base de datos.

---

## 🏗️ Arquitectura del Sistema

El proyecto está construido bajo los estándares más estrictos de la industria moderna para garantizar el desacoplamiento, la mantenibilidad y la escalabilidad del producto:

* **Clean Architecture:** Separación absoluta de responsabilidades en 4 capas independientes (`Domain`, `Application`, `Infrastructure`, `API`).
* **CQRS (Command Query Responsibility Segregation):** Las intenciones de mutación de estado (Commands) están completamente aisladas mediante el patrón Mediator (**MediatR**), garantizando que la lógica de negocio jamás se acople a los controladores HTTP.
* **Domain-Driven Design (DDD):** Modelado de un Aggregate Root rico (`Comanda`) con una máquina de estados integrada y encapsulamiento estricto. El estado de las entidades solo muta a través de métodos de negocio controlados, protegiendo la integridad del ecosistema.
* **Principio de Inversión de Dependencias (SOLID):** La capa de Aplicación dicta los contratos e interfaces (`IComandaRepository`) y la Infraestructura los implementa, logrando un desacoplamiento total del motor de persistencia.

---

## 🎯 Retos e Hitos Técnicos Empleados

### 1. Persistencia Híbrida Relacional / NoSQL (PostgreSQL + JSONB)
Para resolver la variabilidad de propiedades dinámicas que exige cada cliente en un modelo SaaS (ej. *talla/estampado* en ropa, *término/ingredientes* en gastronomía) sin ensuciar el Dominio ni generar tablas escasas (*sparse tables*), implementé una solución híbrida:
* **Estructura SQL Estricta:** Datos financieros y operativos inmutables (`Cantidad`, `Precio`, `SKU`, `Origen`) mapeados mediante Fluent API.
* **Flexibilidad NoSQL:** Columna nativa de tipo `jsonb` en PostgreSQL para el almacenamiento indexado de esquemas de datos dinámicos (`NotasConfiguracion`). Esto permite realizar consultas estructuradas de alta velocidad directamente sobre atributos variables en formato JSON sin romper la integridad relacional.

### 2. Encapsulamiento Estricto del Dominio (Field Property Access)
Respetando las reglas de diseño táctico de DDD, el Aggregate Root `Comanda` gobierna de forma absoluta sus entidades hijas `ItemComanda`. La lista interna está protegida contra mutaciones externas e ilegítimas (`IReadOnlyCollection`). Mediante Fluent API en Entity Framework Core, configuré el acceso directo a nivel de campo privado (`PropertyAccessMode.Field`), permitiendo que el ORM hidrate el grafo completo desde la base de datos sin necesidad de exponer setters públicos que violen la integridad del negocio.

### 3. Inversión de Control Estricta (SOLID)
Los contratos e interfaces de persistencia (`IComandaRepository`) pertenecen única y exclusivamente a la capa de **Application**, obligando a la capa de **Infrastructure** a depender de la abstracción superior. El cableado se gestiona mediante el contenedor IoC nativo de .NET con ciclos de vida controlados (`Scoped`) por request HTTP.

---

## 🛠️ Lecciones Avanzadas de Ingeniería: Diagnóstico en Runtime

Durante la fase de estabilización del pipeline de inyección de dependencias y el escaneo de ensamblados por **MediatR**, se diagnosticó y erradicó una **Anomalía de Identidad de Tipos (Type Identity Mismatch)**. 

* **El Problema:** El contenedor IoC reportaba que el servicio `IComandaRepository` estaba registrado, pero MediatR fallaba en tiempo de ejecución al intentar instanciar el Handler (`CreateComandaCommandHandler`), aduciendo que el tipo no podía ser resuelto.
* **El Diagnóstico Molecular:** Implementé una sonda de diagnóstico por reflexión en el arranque (`Program.cs`) para auditar los metadatos de carga de la memoria intermedia del CLR (Common Language Runtime). La sonda expuso que la interfaz se estaba compilando por duplicado en rutas físicas cruzadas debido a un archivo remanente en la infraestructura:
    * `DI CONTAINER Target -> TShirt.Comandas.Infrastructure.dll`
    * `MEDIATR HANDLER Expectation -> TShirt.Comandas.Application.dll`
* **La Solución:** Eliminación del tipo duplicado en la capa de persistencia, unificación absoluta bajo las fronteras de Clean Architecture y depuración de artefactos y variables de entorno del sistema operativo por bloqueos de entrada/salida (I/O File Locks).

---

## 🚀 Desglose de Capas

* **TShirt.Comandas.Domain:** Entidades puras, agregados y lógica del negocio sin dependencias externas de frameworks u ORMs.
* **TShirt.Comandas.Application:** Implementación de CQRS (Commands/Queries) utilizando MediatR y validaciones de contratos mediante FluentValidation de forma asíncrona.
* **TShirt.Comandas.Infrastructure:** Capa de acceso a datos que encapsula Entity Framework Core, DbContext híbrido, repositorios concretos y scripts de migración PostgreSQL.
* **TShirt.Comandas.API:** Punto de entrada HTTP expuesto mediante controladores limpios y documentación interactiva autogenerada a través de OpenAPI/Swagger en .NET 9.

---

## ⚙️ Stack Tecnológico
* **Framework:** .NET 9 (C# 13)
* **API presentación:** ASP.NET Core Web API + Swagger/OpenAPI
* **Mensajería Interna:** MediatR (v12)
* **Persistencia:** Entity Framework Core 9 (Code-First)
* **Base de Datos:** PostgreSQL Nativo

---

## 📈 Estado Actual del Desarrollo
- [x] Estructura Clean Architecture inicializada y vinculada.
- [x] Modelo de Dominio abstracto (Máquina de estados de Comanda).
- [x] Persistencia configurada e implementada en PostgreSQL.
- [x] Migraciones de base de datos materializadas (`InitialCreate`).
- [x] Inyección de dependencias modularizada por capas (IoC Container).
- [x] Flujo CQRS de Escritura (`CreateComandaCommand` y Handler) conectado al repositorio real.
- [x] Pruebas End-to-End exitosas con almacenamiento nativo de datos JSONB.
- [ ] Validación avanzada de comandos con FluentValidation.
- [ ] Implementación de Queries CQRS para la lectura indexada de propiedades dinámicas.
- [ ] Notificaciones en tiempo real mediante SignalR.