# MEMORY.md - Diario de Estudio

Memoria del proyecto entre sesiones. Máximo ~50 líneas: resume o elimina lo que ya no aporte.

## Estado actual
- v1 funcionando con MySQL: formulario, racha con 🔥, lista de sesiones, base de datos MySQL local.
- Compila sin errores (`dotnet build`).
- Base de datos `estudio` en MySQL (localhost:3306), se crea automáticamente con `EnsureCreated()`.
- Cadena de conexión en `appsettings.json` (no incluido en git).

## Decisiones (y por qué)
- Fecha editable en el formulario: permite registrar días pasados y ver la racha crecer.
- MySQL con Pomelo.EntityFrameworkCore.MySql: base de datos en servidor local.
- CommunityToolkit.Mvvm: genera código de bindings en tiempo de compilación.
- La racha se calcula desde hoy hacia atrás; si hoy no hay sesión pero ayer sí, la racha sigue viva.
- Credenciales en `appsettings.json` para no hardcodearlas en el código.

## Aprendizajes y errores a evitar
- Si `DataContext` es el ViewModel directamente, los bindings deben ser `{Binding Propiedad}` sin prefijo `ViewModel.`. Usar `{Binding ViewModel.X}` causará error "property not found".
- Para actualizar UI desde ViewModel: `App.MainWindow?.DispatcherQueue.TryEnqueue(...)`.
- Pomelo.EntityFrameworkCore.MySql requiere `Microsoft.Extensions.Configuration.Json` para leer `appsettings.json`.
- `Directory.GetCurrentDirectory()` devuelve `C:\WINDOWS\system32` al ejecutar desde VS. Usar `AppContext.BaseDirectory` para encontrar `appsettings.json`.
- `appsettings.json` debe agregarse al `.csproj` con `<CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>` o no se copia al directorio de salida.
- `DatePicker` de WinUI 3 tiene un bug de conversión en bindings TwoWay con `DateTime?` que no se resuelve ni con converters. La solución es usar un `TextBox` con formato `yyyy-MM-dd` y validar con `DateTime.TryParse` al guardar.

## Próximos pasos
- Validar que no se puedan poner fechas futuras.
- Posible: editar o eliminar sesiones.
