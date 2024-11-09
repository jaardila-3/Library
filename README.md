# Biblioteca - Sistema de Gestión

Este proyecto es una aplicación de gestión de biblioteca desarrollada en **ASP.NET Framework 4.7.2**, que permite a los usuarios gestionar áreas, libros, usuarios, realizar préstamos y gestionar sanciones. El sistema está diseñado siguiendo una estructura de **N-Capas** y utiliza varios patrones de diseño para organizar el código y facilitar su mantenimiento y escalabilidad.

## Características

- **Gestión de Usuarios:** Creación, actualización y eliminación de usuarios registrados en la biblioteca.
- **Gestión de Áreas:** Creación y administración de diferentes áreas temáticas para clasificar los libros.
- **Gestión de Libros:** Adición, modificación y eliminación de libros en la biblioteca, asignándolos a áreas específicas.
- **Préstamos de Libros:** Permite a los usuarios realizar préstamos de libros, asignando fechas de entrega y estableciendo penalidades si no se cumple con la fecha de devolución.
- **Sanciones:** Generación automática de sanciones para usuarios que no cumplan con las fechas de devolución de libros prestados.

## Estructura del Proyecto

El proyecto está estructurado en N-Capas, con las siguientes capas y patrones de diseño:

1. **Business**: Implementa la lógica de negocio utilizando los patrones **Repository** y **Unit of Work** para facilitar la separación de lógica y acceso a datos.
   
2. **Common**: Contiene los objetos de transferencia de datos (**DTOs**), utilizados para intercambiar información entre las capas de la aplicación.

3. **Data**: Responsable del acceso a datos y del manejo de las tablas de base de datos. Incluye las migraciones necesarias para crear las tablas de Identity y las específicas del sistema (áreas, usuarios, libros, préstamos y sanciones).

4. **Presentation**: Es la capa de presentación que utiliza el patrón **Strategy** para manejar las distintas estrategias de préstamo (vigentes y no vigentes) e incluye la configuración de **Identity** para la autenticación de usuarios.

## Requisitos

- **Visual Studio 2019 o superior**.
- **.NET Framework 4.7.2**.
- **SQL Server** para la base de datos.
- Cambiar la cadena de conexión a la base de datos en el archivo de configuración (`Web.config`) de la capa de presentación (`Presentation`), en el archivo `app.config` de `Business` y en `DataAccess`.

### Instalación

1. **Clona este repositorio** en tu máquina local:
   ```bash
   git clone https://github.com/jaardila-3/Library.git
   ```

2. **Configura la base de datos**:
   - Actualiza la cadena de conexión en el archivo `Web.config` de la capa de presentación (`Presentation`), en el archivo `app.config` de `Business` y en `DataAccess` para que apunte al servidor SQL adecuado.
   - Ejecuta el script de SQL nombrado `ScriptBDLibrary.sql` para crear las tablas necesarias fuera de Identity, ubicadas en la raiz de este proyecto.

3. **Ejecuta la aplicación** en Visual Studio.

### Uso

1. **Registro y Autenticación**: Al ejecutar la aplicación, podrás registrarte como usuario administrador de la biblioteca y posterior también podrá iniciar sesión usando el sistema de autenticación de Identity con el login.
2. **Gestión de Biblioteca**: Una vez dentro, tendrás acceso a las funcionalidades de gestión de usuarios, áreas, libros y préstamos.
3. **Sanciones**: Si un libro no es devuelto en la fecha indicada, el sistema creará automáticamente una sanción para el usuario.

### Notas Importantes

- El sistema fue desarrollado en **diciembre de 2021**, por lo que es importante actualizar las versiones de .NET y los paquetes en caso de actualizaciones.
- Las tablas de **Identity** se crean automáticamente al ejecutar la aplicación por primera vez.
- Asegúrate de revisar las dependencias del proyecto y actualizar los paquetes NuGet según sea necesario.
