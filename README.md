# SportXpress

**Sistema de gestión integral para gimnasios**

SportXpress es una aplicación de escritorio desarrollada en C# con Windows Forms, orientada a facilitar la administración y el control de las actividades de un gimnasio.

El sistema centraliza la gestión de alumnos, empleados, asistencias, planes de entrenamiento, membresías y pagos, proporcionando herramientas para consultar información, administrar usuarios y generar reportes que permitan realizar un seguimiento de la actividad del establecimiento.

## Funcionalidades principales

- **Gestión de alumnos:** registro, consulta, modificación y administración de alumnos.
- **Gestión de empleados:** administración de los empleados del establecimiento.
- **Usuarios y permisos:** control de acceso al sistema mediante usuarios y permisos según el rol asignado.
- **Planes de entrenamiento:** administración y asignación de planes a los alumnos.
- **Membresías:** gestión de membresías y sus asignaciones.
- **Asistencias:** registro y consulta de la asistencia de los alumnos.
- **Pagos:** administración y seguimiento de los pagos realizados.
- **Reportes:** consulta de estadísticas e información relevante, incluyendo ingresos por planes y membresías, cantidad de alumnos y asistencias recientes.
- **Copias de seguridad:** generación y restauración de respaldos de la base de datos.

## Tecnologías utilizadas

- **Lenguaje:** C#
- **Framework:** .NET 10
- **Interfaz gráfica:** Windows Forms
- **Base de datos:** Microsoft SQL Server
- **Acceso a datos:** Microsoft.Data.SqlClient
- **Componentes visuales:** FontAwesome.Sharp
- **Control de versiones:** Git y GitHub

## Arquitectura del proyecto

El proyecto utiliza una arquitectura en capas para separar las responsabilidades y facilitar el mantenimiento del código.

| Capa | Responsabilidad |
|---|---|
| `CapaPresentacion` | Interfaz gráfica e interacción con el usuario. |
| `CapaNegocio` | Lógica de negocio y validaciones. |
| `CapaDatos` | Acceso a la base de datos y ejecución de consultas. |
| `CapaEntidad` | Clases y objetos que representan las entidades del sistema. |

Esta separación permite mantener una estructura organizada, reducir el acoplamiento entre componentes y facilitar futuras modificaciones.

## Base de datos

SportXpress utiliza Microsoft SQL Server para almacenar y administrar la información del gimnasio.

La base de datos del proyecto se denomina `DBSISTEMA_GIMNASIO` y contempla la información necesaria para gestionar alumnos, empleados, planes, membresías, asignaciones, asistencias, pagos y permisos.

Durante el desarrollo se utilizó SQL Server LocalDB como entorno de base de datos.

## Objetivo del proyecto

El objetivo de SportXpress es ofrecer una solución que simplifique las tareas administrativas de un gimnasio, centralizando sus operaciones en una única aplicación y proporcionando herramientas para mejorar la organización, el seguimiento de los alumnos y el control de los ingresos.

---

## Para iniciar el programa es necesario ejecutar el script de SQL llamado Gimnasio.sql, una vez dentro del sistema les dejo las claves de los usuarios de prueba por defecto:

Usuario: admin
Contraseña: 1234
Rol: Administrador

Usuario: laura
Contraseña: 1234
Rol: Recepcionista

Usuario: martin
Contraseña: 1234
Rol: Entrenador
