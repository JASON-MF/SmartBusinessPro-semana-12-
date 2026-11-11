# SmartBusinessPro – Contexto de datos con Entity Framework Core (Windows Forms)

**Repositorio GitHub:** `PEGA_AQUI_EL_ENLACE_DE_TU_REPOSITORIO`
**Integrantes / Autor:** `TU_NOMBRE`

## 1. Análisis de requerimientos

| Requerimiento | Implementación |
|---|---|
| Clase `SmartBusinessContext : DbContext` | `Data/SmartBusinessContext.cs` |
| `DbSet<Producto>` y `DbSet<Categoria>` | Propiedades `Productos` y `Categorias` del contexto |
| Cadena de conexión a `SmartBusinessPro` | `App.config` → `SmartBusinessConnection` |
| Proveedor SQL Server con `UseSqlServer()` | `OnConfiguring` del contexto |
| Recuperar productos | `MainForm.btnCargar_Click` |
| Mostrar nombre, precio, existencia y categoría en `DataGridView` | `dgvProductos` + modelo `ProductoVista` |
| Gestión de errores (servidor no disponible / conexión incorrecta) | `Helpers/ErrorHelper.cs` + `try/catch` en el formulario |

## 2. Estructura del proyecto

```
SmartBusinessPro.sln
Scripts/SmartBusinessPro.sql      <- crea la BD, tablas y datos de ejemplo
SmartBusinessPro/
 ├─ App.config                    <- cadena de conexión
 ├─ Program.cs
 ├─ Models/  Categoria.cs, Producto.cs, ProductoVista.cs
 ├─ Data/    SmartBusinessContext.cs
 ├─ Helpers/ ErrorHelper.cs
 └─ Forms/   MainForm.cs, MainForm.Designer.cs
```

Paquetes NuGet: `Microsoft.EntityFrameworkCore.SqlServer` (8.0.11) y `System.Configuration.ConfigurationManager` (8.0.1).

## 3. Configuración – cadena de conexión

```xml
<add name="SmartBusinessConnection"
     connectionString="Server=localhost\SQLEXPRESS;Database=SmartBusinessPro;Trusted_Connection=True;TrustServerCertificate=True;Connect Timeout=5;"
     providerName="Microsoft.Data.SqlClient" />
```

| Componente | Significado |
|---|---|
| `Server=localhost\SQLEXPRESS` | Servidor e instancia de SQL Server (`(localdb)\MSSQLLocalDB` para LocalDB). |
| `Database=SmartBusinessPro` | Base de datos a la que se conecta. |
| `Trusted_Connection=True` | Autenticación de Windows (sin usuario/contraseña). |
| `TrustServerCertificate=True` | Acepta el certificado del servidor local; necesario porque el cifrado está activado por defecto en versiones recientes del proveedor. |
| `Connect Timeout=5` | Segundos de espera antes de informar que no hay conexión. |

## 4. Cómo ejecutar

1. Abrir **SSMS** y ejecutar `Scripts/SmartBusinessPro.sql`.
2. Abrir `SmartBusinessPro.sln` en **Visual Studio 2022** (con la carga de trabajo *Desarrollo de escritorio con .NET*).
3. Ajustar `Server=` en `App.config` según tu instancia.
4. Presionar **F5**. Clic en *Probar conexión* y luego en *Cargar productos*.

## 5. Pruebas realizadas

| # | Prueba | Cambio realizado | Resultado esperado | Resultado obtenido |
|---|---|---|---|---|
| 1 | Conexión válida | Ninguno | Mensaje verde con servidor, BD y total de productos; el grid muestra 12 productos | *(completar + captura)* |
| 2 | Servidor incorrecto | `Server=servidorquenoexiste` | Mensaje "No se pudo conectar al servidor…" (error 53 / -1 / 258) tras ~5 s | *(completar + captura)* |
| 3 | Base de datos incorrecta | `Database=SmartBusinessPro_X` | Mensaje "No se pudo abrir la base de datos…" (error 4060) | *(completar + captura)* |
| 4 | Credenciales incorrectas | Autenticación SQL con clave errónea | Mensaje "Falló el inicio de sesión" (error 18456) | *(completar + captura)* |

Capturas: carpeta `Evidencias/`.

## 6. Errores encontrados y soluciones

| Error | Causa | Solución |
|---|---|---|
| *A connection was successfully established… certificate chain not trusted* | El cifrado es obligatorio por defecto y el certificado local no es de confianza | Agregar `TrustServerCertificate=True` a la cadena |
| *Network-related or instance-specific error (53 / -1)* | Nombre de servidor/instancia incorrecto o servicio detenido | Verificar el nombre en SSMS y que el servicio *SQL Server (SQLEXPRESS)* esté en ejecución |
| *Cannot open database "X" requested by the login (4060)* | La BD no existe o el usuario no tiene permisos | Ejecutar el script SQL y revisar `Database=` |
| *Invalid object name 'Productos' (208)* | Tablas no creadas | Ejecutar `Scripts/SmartBusinessPro.sql` |
| *No se encontró la cadena de conexión en App.config* | Nombre distinto o archivo sin copiar | Usar el nombre `SmartBusinessConnection` |

## 7. Conclusión

Centralizar el acceso a datos en `SmartBusinessContext` evita repetir la configuración de conexión en cada formulario: los formularios solo crean el contexto y consultan los `DbSet`, lo que reduce errores, facilita el mantenimiento y permite cambiar de servidor o base de datos en un único punto. Mantener la cadena de conexión en `App.config` separa la configuración del código: se puede modificar sin recompilar, usar valores distintos por equipo o ambiente y evitar contraseñas incrustadas en el código fuente. Además, el manejo centralizado de errores permite informar al usuario con mensajes claros cuando el servidor no está disponible o la conexión es incorrecta.
