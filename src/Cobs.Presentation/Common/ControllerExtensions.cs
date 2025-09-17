namespace Cobs.Presentation.Common
{
    public static class ControllerExtensions
    {
        public static int GetCurrentUserId(this ControllerBase controller)
        {
            var claim = controller.User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
                throw new UnauthorizedAccessException("شناسه کاربر در توکن یافت نشد");

            if (!int.TryParse(claim.Value, out var userId))
                throw new UnauthorizedAccessException("شناسه نامعتبر در توکن");

            return userId;
        }
    }
}
