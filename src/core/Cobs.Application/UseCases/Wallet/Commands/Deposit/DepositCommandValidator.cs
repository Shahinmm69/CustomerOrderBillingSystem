namespace Cobs.Application.UseCases.Wallet.Commands.Deposit
{
    public class DepositCommandValidator : AbstractValidator<DepositCommand>
    {
        public DepositCommandValidator()
        {
            RuleFor(x => x.WalletId)
                .GreaterThan(0)
                .WithMessage("شناسه کیف پول معتبر نیست.");

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("مقدار واریز باید بزرگتر از صفر باشد.");
        }
    }
}
