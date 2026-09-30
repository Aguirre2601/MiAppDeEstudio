# AGENTS.md

## Proyecto

App de escritorio Windows con WinUI 3 (.NET 8). Registro de sesiones de estudio con racha de días consecutivos.

## Comandos

```bash
dotnet build          # Compilar
dotnet run            # Ejecutar (requiere Windows 10/11)
```

No hay tests, lint, ni CI.

## Arquitectura

- **MVVM** con CommunityToolkit.Mvvm: `[ObservableProperty]` y `[RelayCommand]` generan código en tiempo de compilación.
- **Base de datos**: EF Core + MySQL (Pomelo). Cadena de conexión en `appsettings.json` (no incluir en git). La base de datos `estudio` se crea automáticamente con `EnsureCreated()`.
- **UI thread**: Para actualizar la UI desde el ViewModel se usa `App.MainWindow?.DispatcherQueue.TryEnqueue(...)`.
- **Navegación**: `MainWindow` contiene un `Frame` que navega a `Views/MainPage`.

## Estructura

```
Models/Session.cs          → Modelo de datos
ViewModels/MainViewModel.cs → Lógica de la app
Views/MainPage.xaml        → Interfaz (XAML)
Views/MainPage.xaml.cs     → Code-behind + converters
Data/AppDbContext.cs       → Contexto EF Core
```

## Convenciones

- Todos los textos de la interfaz en español.
- Diseño: colores pastel, bordes redondeados (CornerRadius=16), emojis.
- La racha cuenta días consecutivos con al menos una sesión. Si hoy no hay sesión pero ayer sí, la racha sigue viva.
- Fechas siempre en hora local del sistema.


## Forma de trabajar 
-Haz solo lo que se te pide, no añadas funciones por tu cuenta.

## Memoria
- Al empezar, lee `MEMORY.md` para conocer el estado del proyecto y las decisiones tomadas.
- Al terminar una tarea, actualízalo: estado actual, decisiones importantes (con su porqué) y errores a evitar.
- Mantenlo breve (máximo ~50 líneas): resume o elimina lo que ya no aporte.
- Si algo se convierte en una regla permanente, propón moverlo a `AGENTS.md` en lugar de dejarlo en la memoria.
- No guardes nunca datos sensibles (claves, tokens, datos personales).

## Limites
-Siempre: respeta las regs de fecha y racha.
-Siempre: actualiza MEMORY.md al finalizar cada tarea.
-Pregunta antes: de crear archivos nuevos, cambiar el formato de los datos guardados.
-Nunca: añadir dependencias externas, ni usar servicios en la nube, ni guardar datos fuera del archivo SQLite local.