namespace Cobs.Application.Notification.CreateInvoice
{
    public record CreateInvoiceNotification : INotification
    {
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
