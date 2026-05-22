# MiApp - Sistema de Gestión de Productos y Autenticación

Este proyecto es una API Web desarrollada en **.NET 8** siguiendo los principios de **Clean Architecture** (Arquitectura Limpia) y el patrón **CQRS (Command Query Responsibility Segregation)** implementado con **MediatR**.

## 🚀 Requisitos Previos

Antes de ejecutar la aplicación, asegúrate de tener instalado:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Git](https://git-scm.com/)

---

## 🛠️ Arquitectura y Patrones Utilizados

La solución está dividida de forma estricta para respetar el flujo correcto de una solicitud HTTP:

- **MiApp.Api**: Capa de presentación (Controladores que reciben las peticiones HTTP y delegan a MediatR).
- **MiApp.Application**: Lógica de negocio (Contiene los Casos de Uso divididos en **Commands** y **Queries** independientes, cumpliendo con la consigna de la materia).
- **MiApp.Infrastructure**: Acceso a datos y persistencia utilizando **Entity Framework Core** y **SQLite**.
- **MiApp.Domain**: Núcleo del sistema (Entidades, interfaces de repositorios y lógica pura de dominio).

---

## 💻 Instrucciones para Hacer Arrancar el Proyecto

Sigue estos pasos en tu terminal (Git Bash o Consola) para clonar, configurar e iniciar el servidor:

### 1. Clonar el repositorio

```bash
git clone <URL_DE_TU_REPOSITORIO_DE_GITHUB>
cd MiApp
```
