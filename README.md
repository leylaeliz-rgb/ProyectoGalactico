# Catálogo galáctico

API REST hecha con Minimal API en .NET 10 para la materia Tecnologías Web 1. Permite consultar y administrar personajes, sus cartas coleccionables y los eventos en los que participan, y simular batallas entre Rebeldes e Imperio.

Los datos se guardan en memoria: al reiniciar la aplicación vuelven a su estado inicial.

## Cómo clonarlo y ejecutarlo

Necesitas tener instalados Git y el SDK de .NET 10.

```bash
git clone <url-del-repositorio>
cd ProyectoGalactico
dotnet run
```

La consola muestra la dirección donde corre la app (por ejemplo `http://localhost:5115`). Ábrela en el navegador y agrega `/swagger` al final.

## Cómo usarlo

Swagger tiene dos documentos, que se eligen en el selector de arriba:

- **Juego (v1):** consultar personajes, cartas y eventos, ver el ranking y simular batallas.
- **Administración (v2):** crear, modificar y borrar personajes, cartas y eventos.
