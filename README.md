# TicketsPro

Sistema profesional de gestión de tickets de soporte técnico (Help Desk / Service Desk) desarrollado en .NET con arquitectura multicapa.

## Descripción

TicketsPro es una aplicación empresarial para la gestión integral de tickets de soporte técnico y mantenimiento de equipos informáticos. Permite gestionar solicitudes de servicio, asignar técnicos, hacer seguimiento de equipos, administrar inventario y controlar acuerdos de nivel de servicio (SLA).

## Características Principales

- **Gestión de Tickets**: Creación, seguimiento y resolución de tickets de soporte
- **Sistema SLA**: Control de acuerdos de nivel de servicio con políticas configurables
- **Gestión de Prioridades**: Sistema de prioridades con niveles de impacto y peso
- **Asignación de Técnicos**: Asignación y seguimiento de tickets a técnicos
- **Gestión de Equipos**: Registro y control de equipos informáticos
- **Inventario**: Administración de items y categorías de inventario
- **Comentarios y Adjuntos**: Sistema de comunicación en tickets con archivos adjuntos
- **Auditoría**: Control de integridad con hash para detectar alteraciones
- **Ubicaciones**: Gestión de ubicaciones físicas y ubicaciones de equipos
- **Categorización**: Sistema de categorías para tickets y equipos
- **Estados Configurables**: Workflow de estados personalizables
- **Analíticas**: Dashboard con métricas y estadísticas de tickets

## Arquitectura

El proyecto utiliza una arquitectura en capas (N-Tier Architecture) con los siguientes proyectos:

```
TicketsPro/
├── Entity/          - Capa de entidades de dominio
├── DAL/             - Capa de acceso a datos (Data Access Layer)
├── BLL/             - Capa de lógica de negocio (Business Logic Layer)
├── Services/        - Servicios transversales (login, seguridad, auditoría)
├── Controller/      - Capa de controladores
├── TicketPro/       - Interfaz de usuario Windows Forms
└── WEB/             - Interfaz web con Blazor Server (ASP.NET Core 9.0)
```

### Patrones de Diseño Implementados

- **Repository Pattern**: Abstracción del acceso a datos
- **Unit of Work**: Control de transacciones
- **Strategy Pattern**: Para cálculo de SLA (24x7, horas laborales)
- **Factory Pattern**: Para creación de estrategias de SLA
- **Dependency Injection**: Configuración de servicios

## Tecnologías Utilizadas

### Backend
- **.NET Framework 4.x** (TicketPro UI, DAL, BLL, Services)
- **ASP.NET Core 9.0** (WEB - Blazor Server)
- **Entity Framework 6.5** y **Entity Framework Core 9.0**
- **C# 9.0+**

### Base de Datos
- **SQL Server** (2016 o superior)
- **T-SQL**

### Frameworks y Librerías
- **Blazor Server** - Framework UI para aplicación web
- **AutoMapper** - Mapeo de objetos
- **MySQL.Data** - Conector MySQL (opcional)
- **Newtonsoft.Json** - Serialización JSON
- **Lucene.Net** - Búsqueda full-text (opcional)

### Herramientas
- **Visual Studio 2022**
- **SQL Server Management Studio**

## Requisitos Previos

- Windows 10/11 o Windows Server 2016+
- .NET Framework 4.8 o superior
- .NET 9.0 SDK
- SQL Server 2016 o superior (Express, Developer o Enterprise)
- Visual Studio 2022 (recomendado) o Visual Studio Code
- SQL Server Management Studio (opcional)

## Instalación

### 1. Clonar el Repositorio

```bash
git clone https://github.com/tu-usuario/TicketsPro.git
cd TicketsPro
```

### 2. Configurar la Base de Datos

Ejecutar los scripts SQL en el siguiente orden:

```bash
# Desde SQL Server Management Studio o sqlcmd
sqlcmd -S localhost\SQLEXPRESS -i Scripts\01_MainBusiness.sql
sqlcmd -S localhost\SQLEXPRESS -i Scripts\02_Services.sql
```

O manualmente:
1. Abrir SQL Server Management Studio
2. Conectarse a la instancia de SQL Server
3. Ejecutar `Scripts/01_MainBusiness.sql` (crea la base de datos y tablas principales)
4. Ejecutar `Scripts/02_Services.sql` (crea tablas de servicios, usuarios y permisos)

### 3. Configurar Cadenas de Conexión

#### Para el proyecto WEB (Blazor):

Editar `WEB/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "ProyectoTicketsProDB": "Data Source=TU_SERVIDOR\\SQLEXPRESS;Initial Catalog=TicketsProDB_v2;Integrated Security=True;TrustServerCertificate=True"
  }
}
```

#### Para los proyectos .NET Framework:

Editar los archivos `app.config` en los proyectos BLL, DAL, Services:

```xml
<connectionStrings>
  <add name="TicketsProDB"
       connectionString="Data Source=TU_SERVIDOR\SQLEXPRESS;Initial Catalog=TicketsProDB_v2;Integrated Security=True"
       providerName="System.Data.SqlClient"/>
</connectionStrings>
```

### 4. Restaurar Paquetes NuGet

```bash
# Desde la raíz del proyecto
dotnet restore TicketsPro/TicketsPro-master/WEB/WEB.csproj

# Para los proyectos .NET Framework
cd TicketsPro/TicketsPro-master/TicketPro
nuget restore TicketPro.sln
```

### 5. Compilar la Solución

#### Opción A: Visual Studio
1. Abrir `TicketsPro/TicketsPro-master/TicketPro/TicketPro.sln`
2. Compilar la solución (Build → Build Solution) o `Ctrl+Shift+B`

#### Opción B: Línea de comandos
```bash
# Compilar proyecto WEB (Blazor)
cd TicketsPro/TicketsPro-master/WEB
dotnet build

# Compilar solución .NET Framework
cd ../TicketPro
msbuild TicketPro.sln /p:Configuration=Release
```

## Ejecución

### Aplicación Web (Blazor Server)

```bash
cd TicketsPro/TicketsPro-master/WEB
dotnet run
```

O desde Visual Studio:
1. Establecer el proyecto `WEB` como proyecto de inicio
2. Presionar `F5` o hacer clic en el botón de ejecución

La aplicación estará disponible en:
- HTTPS: `https://localhost:5001`
- HTTP: `http://localhost:5000`

### Aplicación de Escritorio (Windows Forms)

1. Abrir la solución en Visual Studio
2. Establecer `TicketPro UI` como proyecto de inicio
3. Presionar `F5` para ejecutar

## Estructura del Proyecto

### Entity (Capa de Dominio)
Contiene las entidades de negocio:
- `Ticket`: Ticket de soporte
- `Usuario`: Usuarios del sistema
- `PrioridadTicket`: Prioridades configurables
- `EstadoTicket`: Estados del workflow
- `CategoriaTicket`: Categorías de tickets
- `EquipoInformatico`: Equipos a dar soporte
- `PoliticaSLA`: Políticas de nivel de servicio
- `Ubicacion`: Ubicaciones físicas
- `InventarioItem`: Items de inventario
- `ComentarioTicket`: Comentarios en tickets
- `AdjuntoTicket`: Archivos adjuntos

### DAL (Data Access Layer)
Implementa el patrón Repository y Unit of Work:
- `IGenericRepository<T>`: Interfaz genérica de repositorio
- `IUnitOfWork`: Patrón Unit of Work
- Implementaciones para SQL Server
- `SqlHelper`: Utilidades para acceso a datos

### BLL (Business Logic Layer)
Contiene la lógica de negocio:
- Validaciones de negocio
- Cálculo de SLA con estrategias (24x7, horas laborales)
- Gestión de estados y transiciones
- Helpers de auditoría y email
- Manejo de excepciones de negocio

### Services
Servicios transversales:
- Autenticación y autorización
- Gestión de sesiones
- Sistema de familias y patentes (permisos)
- Internacionalización (i18n)
- Logging y auditoría

### WEB (Blazor Server)
Interfaz web moderna con:
- Razor Components
- Server-side Blazor
- Entity Framework Core 9.0
- ASP.NET Core 9.0

### TicketPro UI (Windows Forms)
Aplicación de escritorio legacy con interfaz Windows Forms.

## Funcionalidades Principales

### Sistema de Tickets
- Crear tickets con título, descripción, prioridad y categoría
- Asignar tickets a técnicos
- Cambiar estados según workflow configurado
- Agregar comentarios y adjuntos
- Asociar equipos informáticos
- Control de integridad con hash

### Sistema SLA
- Configurar políticas de SLA por prioridad
- Dos estrategias de cálculo:
  - **24x7**: Cuenta todas las horas del día
  - **Horas Laborales**: Solo cuenta horario laboral
- Cálculo automático de fechas de vencimiento
- Tracking de primera respuesta y resolución

### Gestión de Equipos e Inventario
- Registro de equipos informáticos
- Tipos y ubicaciones de equipos
- Inventario de items con categorías
- Asociación de equipos a tickets

### Sistema de Usuarios y Permisos
- Gestión de usuarios
- Sistema de familias (roles) y patentes (permisos)
- Control de acceso basado en roles

### Auditoría y Trazabilidad
- Log de todas las operaciones
- Hash de integridad para detectar alteraciones
- Bitácora de eventos del sistema

## Configuración

### Configuración de SLA

Las políticas SLA se configuran en la tabla `PoliticaSLA`:
- **HorasAtencion**: Tiempo para primera respuesta
- **HorasResolucion**: Tiempo para resolver el ticket
- **SoloHorasLaborales**: Define si se cuentan solo horas laborales
- **PrioridadId**: Asociada a una prioridad específica

### Configuración de Estados

Los estados se definen en la tabla `EstadoTicket`. Estados típicos:
- Nuevo
- Abierto
- En Progreso
- Pendiente Cliente
- Resuelto
- Cerrado
- Cancelado

### Configuración de Prioridades

Las prioridades se definen con:
- **Nombre**: Ej. Crítica, Alta, Media, Baja
- **NivelPeso**: Valor numérico (1-100)
- **CodigoColor**: Color en formato hexadecimal
- **NivelImpacto**: Descripción del impacto

## Base de Datos

### Tablas Principales

- **Ticket**: Tickets de soporte
- **Usuario**: Usuarios del sistema
- **PrioridadTicket**: Prioridades
- **EstadoTicket**: Estados
- **CategoriaTicket**: Categorías
- **PoliticaSLA**: Políticas de SLA
- **EquipoInformatico**: Equipos
- **TipoEquipo**: Tipos de equipos
- **Ubicacion**: Ubicaciones físicas
- **UbicacionEquipo**: Ubicaciones específicas de equipos
- **InventarioItem**: Items de inventario
- **CategoriaItem**: Categorías de inventario
- **ComentarioTicket**: Comentarios
- **AdjuntoTicket**: Archivos adjuntos
- **SolucionTicket**: Soluciones aplicadas

### Diagramas

Los scripts SQL en `Scripts/` contienen:
- Definiciones de tablas con restricciones
- Índices para optimización
- Relaciones e integridad referencial
- Datos iniciales (seeds)

## Contribución

Para contribuir al proyecto:

1. Fork el repositorio
2. Crea una rama para tu feature (`git checkout -b feature/NuevaFuncionalidad`)
3. Commit tus cambios (`git commit -m 'Agregar nueva funcionalidad'`)
4. Push a la rama (`git push origin feature/NuevaFuncionalidad`)
5. Abre un Pull Request

### Estándares de Código

- Seguir las convenciones de C# y .NET
- Documentar métodos públicos con comentarios XML
- Escribir pruebas unitarias para nueva funcionalidad
- Mantener la separación de capas

## Licencia

Este proyecto es de código cerrado. Todos los derechos reservados.

## Autor

TicketsPro - Sistema de Gestión de Tickets

## Soporte

Para reportar problemas o solicitar funcionalidades, crear un issue en el repositorio.

## Roadmap

### Próximas Funcionalidades
- [ ] API REST para integración con terceros
- [ ] Aplicación móvil (iOS/Android)
- [ ] Dashboard avanzado con gráficos
- [ ] Notificaciones en tiempo real (SignalR)
- [ ] Sistema de encuestas de satisfacción
- [ ] Base de conocimiento (Knowledge Base)
- [ ] Integración con correo electrónico (IMAP/SMTP)
- [ ] Chatbot de soporte
- [ ] Reportes avanzados y exportación a Excel/PDF
- [ ] Multi-tenancy (multi-empresa)

## Notas Técnicas

### Migraciones

Para crear nuevas migraciones en el proyecto WEB:

```bash
cd WEB
dotnet ef migrations add NombreMigracion
dotnet ef database update
```

### Troubleshooting

#### Error de conexión a SQL Server
- Verificar que el servicio SQL Server esté ejecutándose
- Confirmar el nombre de la instancia (.\SQLEXPRESS o localhost\SQLEXPRESS)
- Verificar que SQL Server Browser esté habilitado
- Confirmar que la autenticación de Windows esté habilitada

#### Error de dependencias NuGet
```bash
# Limpiar y restaurar
dotnet clean
dotnet restore
nuget restore
```

#### Problemas con Entity Framework
```bash
# Reinstalar paquetes EF
dotnet tool install --global dotnet-ef
dotnet ef database update --project WEB
```

---

**Versión**: 2.0
**Última Actualización**: Marzo 2026
