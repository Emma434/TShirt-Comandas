# Sistema de Gestión de Comandas (TShirt.Comandas)

Sistema backend diseñado para la gestión y seguimiento de pedidos de estampado de poleras y polerones, desarrollado bajo principios de **Clean Architecture** y **CQRS**.

## 🏗️ Arquitectura y Diseño
El proyecto sigue una estructura de capas para garantizar alta cohesión y bajo acoplamiento:

* **Domain:** Define las entidades del negocio (Aggregate Roots como `Comanda` con sus `DetallesEstampado`) protegidas mediante encapsulamiento estricto.
* **Application:** Implementa la lógica de casos de uso usando **MediatR**. Incluye un `PipelineBehavior` centralizado para validaciones automáticas.
* **Infrastructure:** (En construcción) Responsable de la persistencia con EF Core.
* **API:** Exposición de endpoints RESTful.

## 🚀 Características Técnicas
* **CQRS (Command Query Responsibility Segregation):** Separación clara entre comandos de escritura y consultas de lectura.
* **Domain-Driven Design (DDD):** Entidades protegidas que garantizan la integridad del estado del negocio.
* **Validación en Pipeline:** Validación de reglas de negocio ejecutada antes de llegar a la lógica principal (Handler), asegurando que solo datos válidos entren al sistema.
* **Manejo Centralizado de Excepciones:** Middleware dedicado para capturar errores y devolver respuestas HTTP estandarizadas.

## 🛠️ Stack Tecnológico
* .NET 9
* MediatR
* FluentValidation
* Entity Framework Core