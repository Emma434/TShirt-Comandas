# Engine de Comandas Genéricas para Arquitecturas SaaS (TShirt.Comandas)

Este repositorio aloja el Core de un motor de gestión y persistencia de comandas en tiempo real altamente escalable, diseñado bajo los principios de **Clean Architecture** y patrones avanzados de **Domain-Driven Design (DDD)** en el ecosistema **.NET 9**.

El sistema evolucionó estratégicamente desde un modelo originalmente acoplado al flujo de producción textil hacia una infraestructura abstracta y multi-rubro (SaaS Genérico) capaz de procesar pedidos para restaurantes, e-commerce, manufactura o retail sin requerir modificaciones en el esquema relacional rígido de la base de datos.

---

## 🏗️ Arquitectura del Sistema

El proyecto está construido bajo los estándares más estrictos de la industria moderna para garantizar el desacoplamiento, la mantenibilidad y la escalabilidad del producto:

* **Clean Architecture:** Separación absoluta de responsabilidades en 4 capas independientes (`Domain`, `Application`, `Infrastructure`, `API`).
* **CQRS (Command Query Responsibility Segregation):** Las intenciones de mutación de estado (Commands) y las consultas (Queries) están completamente aisladas mediante el patrón Mediator (**MediatR**), garantizando que la lógica de negocio jamás se acople a los controladores HTTP.
* **Domain-Driven Design (DDD):** Modelado de un Aggregate Root rico (`Comanda`) con una máquina de estados integrada y encapsulamiento estricto. El estado de las entidades solo muta a través de métodos de negocio controlados, protegiendo la integridad del ecosistema.
* **Principio de Inversión de Dependencias (SOLID):** La capa de Aplicación dicta los contratos e interfaces (`IComandaRepository`) y la Infraestructura los implementa, logrando un desacoplamiento total del motor de persistencia.

---

## 🎯 Retos e Hitos Técnicos Empleados

### 1. Persistencia Híbrida Relacional / NoSQL (PostgreSQL + JSONB)
Para resolver la variabilidad de propiedades dinámicas que exige cada cliente en un modelo SaaS (ej. *talla/estampado* en ropa, *término/ingredientes* en gastronomía) sin ensuciar el Dominio ni generar tablas escasas (*sparse tables*), implementé una solución híbrida:
* **Estructura SQL Estricta:** Datos financieros y operativos inmutables (`Cantidad`, `Precio`, `SKU`, `Origen`) mapeados mediante Fluent API.
* **Flexibilidad NoSQL:** Columna nativa de tipo `jsonb` en PostgreSQL para el almacenamiento indexado de esquemas de datos dinámicos (`NotasConfiguracion`). Esto permite realizar consultas estructuradas de alta velocidad directamente sobre atributos variables en formato JSON sin romper la integridad relacional.

### 2. Barrera Perimetral de Validación Asíncrona (MediatR Pipeline + FluentValidation)
Para evitar que payloads corruptos u operaciones inválidas degraden el rendimiento del sistema o golpeen la base de datos innecesariamente, se construyó un escudo perimetral:
* Mediante un comportamiento genérico de pipeline (`IPipelineBehavior<,>`), MediatR intercepta de forma asíncrona todos los comandos de mutación entrantes antes de que alcancen sus respectivos manejadores.
* Si el payload viola las reglas complejas de negocio (ej. SKU vacío o cantidades negativas), la petición es abortada en la frontera de la aplicación y un middleware global transforma la excepción en una respuesta estructurada semántica **HTTP 400 Bad Request**, garantizando inmunidad a fallos catastróficos HTTP 500.

### 3. Inmunidad a Caracteres Especiales en Lectura (Bypass Seguro de Datos NoSQL)
Para resolver los clásicos colapsos de parseo en el Frontend por el uso de caracteres especiales, eñes, tildes o saltos de línea dentro de campos variables, la arquitectura desecha el uso de strings planos escapados:
* Al ejecutar `GetComandaByIdQuery`, el backend lee de forma optimizada (`AsNoTracking()`) y utiliza el motor nativo de serialización `System.Text.Json` para parsear el string JSON a nivel de infraestructura directamente en un árbol binario jerárquico (`JsonDocument`).
* El DTO de salida transporta un objeto JSON legítimo, lo que evita que el Frontend sufra la sobre-validación de cadenas o la necesidad de ejecutar métodos de parseo propensos a errores en el cliente web.

---

## 🛠️ Lecciones Avanzadas de Ingeniería y Decisiones de Diseño

### Diagnóstico de Tipos en Runtime (Type Identity Mismatch)
Durante la fase de estabilización, se diagnosticó y erradicó una anomalía en el contenedor IoC: el servicio `IComandaRepository` figuraba registrado pero MediatR fallaba en tiempo de ejecución. Implementé una sonda por reflexión en el arranque para auditar los metadatos de carga de la memoria intermedia del CLR (Common Language Runtime), lo que expuso que la interfaz se estaba compilando por duplicado debido a un archivo remanente en la infraestructura física (`TShirt.Comandas.Infrastructure.dll` vs `TShirt.Comandas.Application.dll`). El conflicto se resolvió unificando estrictamente el tipo bajo las fronteras de la capa de Aplicación.

### Enfoque de Identidad Híbrido: GUID vs. Enteros Secuenciales
En sistemas tradicionales monolíticos, es común utilizar llaves primarias autoincrementales (`1, 2, 3...`). Sin embargo, en un modelo SaaS distribuido y Cloud-Grade, este enfoque es inviable por tres razones de arquitectura:
1. **Seguridad (Ataques de Enumeración / IDOR):** Exponer IDs secuenciales en endpoints públicos permite a competidores adivinar el volumen de negocio o raspar registros ajenos cambiando el valor correlativo.
2. **Ciclo de Vida de Dominio (DDD):** El uso de GUIDs permite que la capa de aplicación asigne identidades únicas al aggregate root y a sus entidades hijas en memoria de forma atómica *antes de tocar la base de datos*, reduciendo los viajes de red (roundtrips).
3. **Escalamiento y Sharding:** LosGUIDs garantizan unicidad global si en el futuro se requiere mover clientes a bases de datos dedicadas o procesar comandas offline desde dispositivos móviles sin riesgo de colisión relacional.

* **La Solución Senior:** El motor adopta una estrategia híbrida. La llave primaria interna es un `Guid` inmutable para blindar la seguridad de la API y los índices de base de datos. Para el uso cotidiano de seguimiento humano (búsquedas SQL rápidas o visualización del usuario), se expone la columna indexada `NumeroSeguimiento`, delegada nativamente a PostgreSQL mediante una secuencia automatizada de tipo `IDENTITY`.

---

## 🚀 Desglose de Capas

* **TShirt.Comandas.Domain:** Entidades puras, agregados y lógica del negocio sin dependencias externas de frameworks u ORMs.
* **TShirt.Comandas.Application:** Implementación de CQRS (Commands/Queries), interceptores globales de validación, contratos asíncronos y reglas de FluentValidation.
* **TShirt.Comandas.Infrastructure:** Capa de acceso a datos que encapsula Entity Framework Core, DbContext híbrido, repositorios concretos y scripts de migración PostgreSQL.
* **TShirt.Comandas.API:** Punto de entrada HTTP expuesto mediante controladores anémicos, middleware centralizado de excepciones y documentación interactiva autogenerada a través de OpenAPI/Swagger en .NET 9.

---

## ⚙️ Stack Tecnológico
* **Framework:** .NET 9 (C# 13)
* **API Presentación:** ASP.NET Core Web API + Swagger/OpenAPI
* **Mensajería Interna:** MediatR (v12)
* **Validación:** FluentValidation (v11)
* **Persistencia:** Entity Framework Core 9 (Code-First)
* **Base de Datos:** PostgreSQL Nativo (Engine de datos jsonb)

---

## 📈 Estado Actual del Desarrollo
- [x] Estructura Clean Architecture inicializada y vinculada.
- [x] Modelo de Dominio abstracto (Máquina de estados de Comanda).
- [x] Persistencia configurada e implementada en PostgreSQL.
- [x] Migraciones de base de datos materializadas (`InitialCreate`).
- [x] Inyección de dependencias modularizada por capas (IoC Container).
- [x] Flujo CQRS de Escritura (`CreateComandaCommand` y Handler) conectado al repositorio real.
- [x] Escudo defensivo perimetral automatizado mediante `IPipelineBehavior` y FluentValidation.
- [x] Flujo CQRS de Lectura (`GetComandaByIdQuery`) con extracción y parseo asíncrono de JSONB.
- [x] Pruebas End-to-End de Lectura/Escritura exitosas e inmunes a caracteres especiales.
- [ ] Construcción de la Interfaz de Usuario (UI Formulario Dinámico Multi-rubro).
- [ ] Implementación de Eventos de Dominio (Domain Events) asíncronos para notificaciones de estado.
- [ ] Canalizaciones en tiempo real mediante SignalR.