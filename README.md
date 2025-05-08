# 🛒 TiendaOnline

**TiendaOnline** es una aplicación web desarrollada con **ASP.NET MVC** que permite gestionar una tienda en línea con funcionalidades de catálogo de productos, carrito de compras y autenticación de usuarios con roles diferenciados (usuario y administrador).

## 🚀 Funcionalidades principales

- Visualización de productos disponibles.
- Carrito de compras con posibilidad de agregar, editar y eliminar productos.
- Registro y autenticación de usuarios.
- Roles de **usuario estándar** y **administrador**.
- Panel de administración para gestión de productos (CRUD).

## 🏗️ Tecnologías utilizadas

- **ASP.NET MVC**
- **C#**
- **Entity Framework**
- **SQL Server**
- **Bootstrap**
- **JavaScript**
- **HTML5**
- **CSS**
- **Razor Views**
- **bcrypt**

  
## 📂 Estructura general del proyecto


✅ Esta estructura sigue el patrón **MVC** y además aplica separación de capas **Domain, Application, Infrastructure** dentro del mismo proyecto.

/Controllers → Controladores de la aplicación (acciones MVC)
/Data → Configuración de la base de datos y autenticación
/Factories → Fábricas y objetos generadores de instancias
/Migrations → Migraciones de la base de datos (Entity Framework)
/Models → Modelos de datos (entidades de la base de datos)
/Properties → Configuraciones generales de la aplicación
/Repositories → Lógica de acceso a datos (Repository Pattern)
/Services → Servicios de negocio (capa de lógica de negocio)
/Singletons → Servicios y dependencias registradas como singleton
/ViewModels → Modelos de vista (transferencia de datos a las vistas)
/Views → Vistas Razor de la aplicación


## ⚙️ Cómo ejecutar el proyecto

1. Clona el repositorio:

   ```bash
   git clone https://github.com/JulianSantamariaV/TiendaOnline.git

2. Abre la solucion con Visual Studio **Como es ASP.NET es preferible no usar Visual Studio CODE**

3. Desde la terminal de comandos de Nugget ejecuta

   ```bash
   Update-Database

Esto para generar la base de datos local.

4. Ejecutar SQLQuery1.sql

5. Correr el proyecto.
   Atajo CTRL+f5

## USUARIOS ##

('test', 'test@test.com', 'Test123.')
('admin', 'admin@admin.com', 'Admin123.')

✅ Requisitos de software
Herramienta	Versión mínima	Recomendado
.NET SDK	8.0	8.0.4 o superior
Visual Studio	2022 (17.8+)	con ASP.NET 2022 Community Edition
SQL Server	2017	2019 o superior
Entity Framework Core Tools	Incluidos en proyecto	Latest global tools


