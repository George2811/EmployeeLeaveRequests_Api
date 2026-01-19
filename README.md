# Backend (Employee Leave Requests Api)

## Contexto
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
Enfoque: Clean Architecture + DDD táctico

Se decidió utilizar este enfoque ya que el dominio tiene reglas claras (No Overlapping y Auto rejection). Asi mismo, acorde a lo recomnedble por Clean Architecture se emplea una arquitectura dividida en capas. Ello es muy útil para velar por la direccionalidad de dependencias, cada capa tiene un prósito (Controllers, Services, Persistence and Domain).

Así mismo, toda la información de los endpoints se encuentra correctamente documentada debido el uso de la herramienta Swagger.


## Oportunidades de mejora
La api posee buenas bases como software escalable, no obstante; existen algunas oportunidades de mejora que serian muy beneficiosas en entornos de producción:
- Logs: Para mantener un trazabilidad de los los eventos ocurridos en la apliación, una alternativa es el registro de logs. Para ello, puede ser útil destinar una Base de Datos la cual alamcena información como el evento, la fecha y hora, el tipo de evento y el usuario que originó el evento.
- Autenticación más robusta: Para efectos prácticos se utilizó una autenticación mediante JWT, pero se pueden añadir más capas de seguridad para velar por la integridad de la información de los usuarios. Por ejemplo, la encriptación de sus contraseñas.
- Paginación a nivel de backend: Añadir paginación al endpoint de GET LeaveRequests resultaría muy beneficios a mendida que exsitan más registros, ya que impacta directamente en los tiempos de respuesta de la API con el front end.

