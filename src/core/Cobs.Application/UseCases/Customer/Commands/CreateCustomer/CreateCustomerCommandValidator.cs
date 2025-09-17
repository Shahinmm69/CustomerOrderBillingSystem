namespace Cobs.Application.UseCases.Customer.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        private readonly ICobsDbContext _context;
        public CreateCustomerCommandValidator(ICobsDbContext context)
        {
            _context = context;

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("نام اجباری است")
                .MaximumLength(100)
                .WithMessage("نام نمی‌تواند بیشتر از 100 کاراکتر باشد");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("نام خانوادگی اجباری است")
                .MaximumLength(100)
                .WithMessage("نام خانوادگی نمی‌تواند بیشتر از 100 کاراکتر باشد");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("ایمیل اجباری است")
                .EmailAddress()
                .WithMessage("ایمیل معتبر وارد کنید")
                .MaximumLength(256)
                .WithMessage("ایمیل نمی‌تواند بیشتر از ۲۵۶ کاراکتر باشد.")
                .MustAsync(BeUniqueEmail)
                .WithMessage("ایمیل وارد شده قبلاً ثبت شده است.");

            RuleFor(x => x.Role)
                .IsInEnum()
                .WithMessage("نقش معتبر نیست");
        }
        async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return !await _context.Customers.AnyAsync(c => c.Email == email, cancellationToken);
        }
    }
}