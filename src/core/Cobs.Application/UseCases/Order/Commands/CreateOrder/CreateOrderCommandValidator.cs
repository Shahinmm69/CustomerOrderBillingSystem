namespace Cobs.Application.UseCases.Order.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Product)
                .NotEmpty()
                .WithMessage("نام محصول اجباری است")
                .MaximumLength(100)
                .WithMessage("نام محصول نمی‌تواند بیشتر از 100 کاراکتر باشد");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("تعداد باید بیشتر از صفر باشد");

            RuleFor(x => x.TotalAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("مبلغ سفارش نمی‌تواند منفی باشد");
        }
    }
}
