# 📌 Proyecto Unity - Guía de Instalación y Colaboración

Este repositorio contiene el proyecto de Unity en el que trabajaremos en equipo. Sigue esta guía para clonar el proyecto y configurarlo correctamente en tu máquina.

## 🔹 Prerrequisitos

Antes de clonar el proyecto, asegúrate de tener lo siguiente instalado en tu máquina:

- **Git**: Para clonar y gestionar el repositorio. [Descargar Git](https://git-scm.com/downloads)
- **Unity Hub**: Para administrar versiones de Unity. [Descargar Unity Hub](https://unity.com/download)
- **La versión de Unity correcta**: Verifica la versión del proyecto en `ProjectSettings/ProjectVersion.txt`

---

## 🔹 1. Clonar el Repositorio

1. Abre una terminal o consola.
2. Navega a la carpeta donde deseas clonar el proyecto:
   ```bash
   cd /ruta/donde/quieres/guardar
   ```
3. Clona el repositorio con:
   ```bash
   git clone <URL_DEL_REPOSITORIO>
   ```
4. Ingresa a la carpeta del proyecto:
   ```bash
   cd NombreDelProyecto
   ```

---

## 🔹 2. Abrir el Proyecto en Unity

1. Abre **Unity Hub**.
2. Haz clic en **"Open"** y selecciona la carpeta del proyecto clonado.
3. Si Unity te pide actualizar la versión, verifica que todos estén usando la misma.
4. Espera a que Unity importe los archivos.

---

## 🔹 3. Configurar Git Correctamente

1. Si es la primera vez que usas Git, configura tu usuario:
   ```bash
   git config --global user.name "Tu Nombre"
   git config --global user.email "tu@email.com"
   ```
2. Verifica que el `.gitignore` de Unity esté correctamente configurado para evitar archivos innecesarios:
   ```bash
   cat .gitignore
   ```
3. Asegúrate de estar en la última versión del proyecto:
   ```bash
   git pull origin main
   ```

---

## 🔹 4. Cómo Hacer Cambios y Subirlos

1. **Verifica en qué rama estás**:
   ```bash
   git branch
   ```
2. **Crea una nueva rama si es necesario**:
   ```bash
   git checkout -b feature/nueva-funcionalidad
   ```
3. **Haz cambios en Unity** y guarda los archivos.
4. **Agrega los cambios a Git**:
   ```bash
   git add .
   ```
5. **Haz un commit con un mensaje descriptivo**:
   ```bash
   git commit -m "Añadida nueva funcionalidad X"
   ```
6. **Sube los cambios al repositorio**:
   ```bash
   git push origin feature/nueva-funcionalidad
   ```
7. **Abre un Pull Request (PR) en GitHub/GitLab** para fusionar los cambios en la rama principal.

---

## 🔹 5. Resolución de Problemas Comunes

🔹 **Error de versiones de Unity**: Asegúrate de usar la misma versión indicada en `ProjectVersion.txt`. 🔹 **Conflictos en Git**: Usa `git status` y `git merge --abort` si algo salió mal. 🔹 **Faltan archivos o texturas**: Prueba ejecutar **Reimport All** en Unity (`Click derecho en Assets > Reimport All`).

---

