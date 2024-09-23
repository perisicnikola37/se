<kbd>[<img title="Inglés" alt="Inglés" src="https://upload.wikimedia.org/wikipedia/en/thumb/a/ae/Flag_of_the_United_Kingdom.svg/1200px-Flag_of_the_United_Kingdom.svg.png" width="22">](README.md)</kbd>
<kbd>[<img title="Italiano" alt="Italiano" src="https://upload.wikimedia.org/wikipedia/en/thumb/0/03/Flag_of_Italy.svg/1500px-Flag_of_Italy.svg.png" width="22">](translations/README.ita.md)</kbd>

<div style="display: flex; justify-content: space-between; align-items: center;">
  <h1>Seguimiento de Gastos</h1>
  <img src="https://i.postimg.cc/VsKQJpRb/logo.png" width="38" />
</div>

### Resumen

Esta aplicación basada en .NET y React está diseñada para ayudar a los usuarios a gestionar y realizar un seguimiento efectivo de sus gastos e ingresos. El objetivo es proporcionar una solución integral para organizar transacciones financieras, generar informes y ofrecer funciones premium adicionales para mejorar la experiencia del usuario.

## Documentación de API de Postman
<a href="https://documenter.getpostman.com/view/21619259/2s9YsRd9TF#757dd6bd-9a08-40fd-b5f9-7b19dfaf9b81" target="_blank">Haz clic aquí</a>

## Documentación de API de Swagger
[applicationURL]/swagger

## Usuario Administrador por Defecto

### Credenciales:
Dirección de correo electrónico: admin@gmail.com
<br />
Contraseña: password

## Funcionalidades

1. **Panel de Control**
   - Mostrar el monto actual
   - Mostrar los últimos 5 cambios en gastos
   - Mostrar los últimos 5 cambios en ingresos
   - Botones para navegar a las páginas de lista de grupos de gastos e ingresos
   - Botón para agregar gastos/ingresos a través de una ventana emergente

2. **Operaciones CRUD**
   - Crear, Leer, Actualizar y Eliminar gastos
   - Crear, Leer, Actualizar y Eliminar ingresos
   - Crear, Leer, Actualizar y Eliminar grupos de ingresos
   - Crear, Leer, Actualizar y Eliminar grupos de gastos

## Modelos de Base de Datos

- **Grupo de Gastos**
  - Nombre
  - Descripción
  - ID de Usuario
  - [gastos]

- **Gasto**
  - Descripción
  - Monto
  - ID de Grupo de Gastos
  - ID de Usuario

- **Grupo de Ingresos**
  - Nombre
  - Descripción
  - ID de Usuario
  - [ingresos]

- **Ingreso**
  - Descripción
  - Monto
  - ID de Grupo de Ingresos
  - ID de Usuario

- **Recordatorio** (Función Premium)
  - Día del recordatorio
  - Tipo
  - Activo (booleano)

- **Blog** (Función Premium)
  - Descripción
  - Texto
  - ID de Usuario

## Interfaz de Usuario - UI

1. **Panel de Control**
   - Resumen de la semana pasada - Gráfico
   - Últimos 5 cambios en gastos + cambio gráfico en relación al monto más alto de gastos
   - Últimos 5 cambios en ingresos + cambio gráfico en relación al monto más alto de ingresos
   - Menú para navegar a las páginas de lista de grupos de gastos e ingresos
   - Planes personalizados para tu trayectoria financiera
   - Sección de testimonios
   - Sección de preguntas frecuentes (FAQ)
   - Sección de boletín
   - Pie de página con enlaces relacionados

2. **Página de Lista de Ingresos/Gastos**
   - Tabla
     - Id
     - Nombre (descripción)
     - Monto
     - Grupo
     - Botones de Editar y Eliminar
   - Botón de casilla de verificación para seleccionar todos los ingresos/gastos disponibles
   - Botón de icono de Eliminar para eliminar todos los ingresos/gastos disponibles
   - Flechas de clasificación para ordenar todos los ingresos/gastos disponibles
   - Botón para agregar ingresos/gastos a través de una ventana emergente
   - Campo de entrada de búsqueda para buscar ingresos/gastos
   - Menú desplegable de filtro para filtrar ingresos/gastos
   - Opciones de filtro disponibles: Monto mínimo, monto máximo y filtrar por grupo de ingresos/gastos (nombre)
   - Botón para exportar estadísticas por correo electrónico
   - Opciones de paginación para paginar ingresos/gastos
   - Función "Filas por página" para mostrar cierta cantidad de ingresos/gastos

3. **Ventana Emergente de Edición de Ingresos/Gastos**
   - Campo de nombre editable
   - Campo de descripción editable
   - Campo de monto editable
   - Campo de grupo de ingresos editable
   - Botón Guardar
   - Botón Cancelar

4. **Ventana Emergente de Eliminación de Ingresos/Gastos**
   - Botón Guardar
   - Botón Cancelar

5. **Página de Lista de Grupos de Ingresos/Gastos**
   - Tabla
     - Id
     - Nombre
     - Descripción
     - Botones de Editar y Eliminar
   - Botón de casilla de verificación para seleccionar todos los grupos de ingresos/gastos disponibles
   - Botón de icono de Eliminar para eliminar todos los grupos de ingresos/gastos disponibles
   - Flechas de clasificación para ordenar todos los grupos de ingresos/gastos disponibles
   - Botón para agregar grupos de ingresos/gastos a través de una ventana emergente
   - Opciones de paginación para paginar grupos de ingresos/gastos
   - Función "Filas por página" para mostrar cierta cantidad de grupos de ingresos/gastos
   - Modal de Eliminación con opción de confirmación y cancelación
   - Nombre del grupo de ingresos/gastos clickeable que lleva a la página de detalles

6. **Ventana Emergente de Edición de Grupos de Ingresos/Gastos**
   - Campo de nombre editable
   - Campo de descripción editable
   - Botón Guardar
   - Botón Cancelar

7. **Ventana Emergente de Eliminación de Grupos de Ingresos/Gastos**
   - Botón Guardar
   - Botón Cancelar

8. **Página de Detalles de Grupos de Ingresos/Gastos**
   - Nombre
   - Descripción
   - Últimos 5 cambios de cuenta para ese grupo
   - Miga de pan para una navegación fácil

9. **Recordatorios (Función Premium en Configuración de Perfil)**
   - Botón para crear un recordatorio a través de una ventana emergente
   - Detalles del recordatorio actualmente establecido en el panel de control
   - Miga de pan para una navegación fácil

10. **Ventana Emergente de Eliminación de Recordatorios**
    - Botón Guardar
    - Botón Cancelar

11. **Blogs (Función Premium)**
    - Botón para crear un blog a través de una ventana emergente
    - Tarjetas con todos los blogs disponibles
    - Miga de pan para una navegación fácil

12. **Ventana Emergente de Eliminación de Blogs**
    - Botón Guardar
    - Botón Cancelar

13. **Página de Detalles de Blogs**
    - Título
    - Descripción
    - Autor
    - Fecha de creación
    - Cuerpo
    - Miga de pan para una navegación fácil

## Integraciones Adicionales

### Correos Electrónicos de Estado de la Pipeline
**Pipeline Backend:**
Habilitar pruebas en el proyecto .NET.
<br/>
<br/>
**Pipeline Frontend:**
Construir el proyecto.

**Notificación por Correo Electrónico:**

Enviar correos electrónicos de estado de la pipeline a una lista predefinida de destinatarios, enfocándose solo en las pipelines fallidas.

### Integración con Mailchimp
Registrar automáticamente a los usuarios que se suscriban al boletín en la audiencia de Mailchimp conectada.

<hr/>

Todos los derechos reservados.
<br/>
Copyright &copy; 2024
