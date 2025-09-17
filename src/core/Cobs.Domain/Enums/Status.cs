namespace Cobs.Domain.Enums
{
    public enum Status
    {
        [Description("در حال انتظار")]
        Pending = 1,

        [Description("پراخت شده")]
        Paid = 2
    }
}
