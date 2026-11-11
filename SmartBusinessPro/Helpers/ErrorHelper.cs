using Microsoft.Data.SqlClient;

namespace SmartBusinessPro.Helpers
{
    /// <summary>
    /// Convierte excepciones técnicas en mensajes entendibles para el usuario.
    /// </summary>
    public static class ErrorHelper
    {
        public static string Traducir(Exception ex)
        {
            // Buscar una SqlException dentro de la cadena de excepciones
            Exception? actual = ex;
            while (actual != null)
            {
                if (actual is SqlException sql)
                    return TraducirSql(sql);
                actual = actual.InnerException;
            }

            if (ex is InvalidOperationException && ex.Message.Contains("App.config"))
                return "Falta la cadena de conexión en App.config.\n\n" + ex.Message;

            return "Error inesperado: " + ex.Message;
        }

        private static string TraducirSql(SqlException sql)
        {
            string detalle = $"\n\nCódigo SQL: {sql.Number}\nDetalle: {sql.Message}";

            return sql.Number switch
            {
                // Servidor no encontrado / inaccesible
                -1 or 2 or 53 or 40 or 258 or 10060 or 11001 =>
                    "No se pudo conectar al servidor SQL Server. Verifique el nombre del servidor/instancia, " +
                    "que el servicio SQL Server esté iniciado y que el firewall permita la conexión." + detalle,

                // Tiempo de espera agotado
                -2 =>
                    "Se agotó el tiempo de espera al conectar con el servidor." + detalle,

                // La base de datos no existe o no hay permiso
                4060 =>
                    "No se pudo abrir la base de datos indicada. Verifique que 'SmartBusinessPro' exista " +
                    "y que el usuario tenga permisos." + detalle,

                // Login incorrecto
                18456 =>
                    "Falló el inicio de sesión. Usuario o contraseña incorrectos." + detalle,

                // Objeto o columna no existe
                208 =>
                    "No existe la tabla consultada (Productos / Categorias). Ejecute el script SQL." + detalle,
                207 =>
                    "No existe alguna de las columnas esperadas en las tablas." + detalle,

                _ => "Error de SQL Server." + detalle
            };
        }
    }
}
