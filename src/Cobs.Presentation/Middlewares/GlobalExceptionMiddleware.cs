namespace Cobs.Presentation.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                await WriteErrorResponse(context, StatusCodes.Status404NotFound, ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                await WriteErrorResponse(context, StatusCodes.Status401Unauthorized, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                await WriteErrorResponse(context, StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                await WriteErrorResponse(context, StatusCodes.Status409Conflict, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await WriteErrorResponse(context, StatusCodes.Status500InternalServerError,
                    "خطای سرور رخ داده است.");
            }
        }

        private static async Task WriteErrorResponse(HttpContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { Message = message }));
        }
    }
}
