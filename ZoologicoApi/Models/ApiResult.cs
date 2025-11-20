using Microsoft.AspNetCore.Http;

namespace ZoologicoApi.Models
{
   public class ApiResult<T>
    {
        // 2. Usamos T para tipificar la propiedad Data.
        public bool Success { get; set; }
        public T? Data { get; set; } // <--- La T asegura que Data sea el tipo correcto (Animal, List<Raza>, etc.)
        public string? Message { get; set; }
        public string? ErrorMessage { get; set; }
        public int StatusCode { get; set; }


        // --- Métodos de Fábrica Estáticos ---

        // Método de éxito: Recibe un dato de tipo T
        public static ApiResult<T> SuccessResult(T data, string message = "Operación exitosa", int statusCode = 200)
        {
            return new ApiResult<T>
            {
                Success = true,
                Data = data,
                Message = message,
                StatusCode = statusCode
            };
        }

        // Método de error: No necesita recibir el tipo T en los parámetros
        public static ApiResult<T> ErrorResult(string errorMessage, int statusCode = 500)
        {
            return new ApiResult<T>
            {
                Success = false,
                ErrorMessage = errorMessage,
                StatusCode = statusCode
            };
        }
    }

    // Opcional pero útil: Una clase no genérica para cuando no devuelves datos (ej: un DELETE 204)
    public class ApiResult : ApiResult<object>
    {
    }
}