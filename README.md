# Backend (Employee Leave Requests Api)
Api para que empleados creen solicitudes de vacaciones y managers las gestionen.

🧱 Stack Tecnológico

- .Net 9
- SQL Server (local o Docker)
- Docker (opcional)
- EF Core CLI

## Instrucciones para el Setup
⚙️ Requisitos
- SDK Net 9
- Docker (opcional)
- Clonar repositorio

### 1. Instalar dependencias:

Instlar las dependencias mediante "Manage NuGet Packages"

**npm install**

### 2. Crear la Base De Datos (usar script)

Ejecutar el script "Scripts/CreateDatabaseAndEmployeeRecords.sql" en SQL Server.

### 3. Crear el archivo de variables de entorno (appsettings.json)

Crear/Editar el archivo appsettings para registrar las variables de entorno.

**Conexión a BD**
**JWT Key**

## Decisiones de Diseño

### 1. Enfoque Domain-Driven Design (DDD) y Clean Architecture

Adpoté Domain-Driven Design (DDD) combinado con Clean Architecture como enfoque arquitectónico principal.

El objetivo principal de esta decisión es asegurar que la lógica de negocio sea el núcleo del sistema (No Overlapping y Auto rejection), manteniéndose independiente de frameworks, ORMs o detalles de infraestructura. Es por ello que la solución se estructuró en capas bien definidas:

#### - Capa de Dominio
Contiene las entidades principales del negocio, como Employee y LeaveRequest, así como las reglas de negocio. Esta capa no depende de ninguna otra y representa el corazón del sistema.

#### - Capa de Servicios
Encapsula los casos de uso del sistema e implementa las interfaces necesarias para interactuar con el dominio. Aquí se orquesta el flujo de negocio sin conocer detalles de persistencia o transferencia de data.

#### - Capa de Persistencia
Incluye el acceso a datos mediante el ORM de Entity Framework, encargado de interactuar y realizar las transacciones con la base de datos.

#### - Capa de Controllers (API)
Expone los endpoints HTTP, maneja la serialización de datos, validaciones de entrada, autenticación y autorización.

Este enfoque permite que el sistema sea altamente mantenible, facilita la incorporación de nuevas funcionalidades y reduce significativamente el impacto de cambios tecnológicos futuros.


### 2. Patrones de Software
En el proyecto apliqué principalmente el Repository Pattern para desacoplar el dominio de EF Core y el Resource Pattern para evitar exponer entidades del dominio directamente al frontend.

- Repository Pattern: Defino las interfaces en el Domain y sus implementaciones en la capa de Persistence, ello hace que EF Core puede cambiarse por otros ORMS sin afectar el Dominio.

- Resource Pattern: Permitió controlar exactamente qué datos salen del API. Es por ello que los endpoints no devuelven LeaveRequest directamente, sino recursos como LeaveRequestResource.

### 3. Autenticación mediante JWT
La autenticación del sistema se implementó utilizando JWT, una solución ampliamente adoptada para APIs REST.

- Los tokens incluyen información del usuario autenticado y se validan en cada request protegido mediante middleware.

### 4. Buenas prácticas
- **Manejo centralizado de errores**: Se manejó un mecanismo consistente para el manejo de errores, procurando comunicar de forma clara los errores del negocio y no exponer información sensible.
- **Documentación de la API**: Para documentar la API se utilizó Swagger (OpenAPI), permitiendo generar documentación interactiva de manera automática. Es un punto clave ya que facilita el consumo de la API por parte del frontend y otros clientes.
- **Uso de varibales de entorno**: Las configuraciones del sistema se manejan mediante archivos appsettings.json y variables de entorno, permitiendo adaptar el comportamiento del backend según el entorno (desarrollo, staging o producción). Ello facilitará la integración con pipelines CI/CD y plataformas cloud.


## Oportunidades de mejora
La api posee buenas bases como un software escalable, no obstante; existen algunas oportunidades de mejora que serian muy beneficiosas en entornos de producción:
- **Logs**: Para mantener un trazabilidad de los los eventos ocurridos en la aplicación, una alternativa es el registro de logs. Para ello, puede ser útil destinar una Base de Datos la cual alamcena información como el evento, la fecha/hora, el tipo de evento y el usuario que originó el evento.
- **Autenticación más robusta**: Para efectos prácticos se utilizó una autenticación mediante JWT, pero se pueden añadir más capas de seguridad para velar por la integridad de la información de los usuarios. Así mismo, se podría manejar un módulo de de encriptación para las contraseñas.
- **Paginación a nivel de backend**: Añadir paginación al endpoint de GET LeaveRequests resultaría muy beneficios a medida que exsitan más registros, ya que impacta directamente en los tiempos de respuesta de la API con el frontend.

