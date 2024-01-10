using FluentValidation;
using ProjectN.Parameter;

namespace ProjectN.Validator
{
    /// <summary>
    /// Card Parameter 的驗證器
    /// </summary>
    public class CardParameterValidator : AbstractValidator<CardParameter>
    {
        /// <summary>
        /// 驗證器的建構式: 在這裡註冊我們要驗證的規則
        /// </summary>
        public CardParameterValidator()
        {
            RuleFor(card => card.Attack)
                .GreaterThanOrEqualTo(0);

            RuleFor(card => card.Health)
                .GreaterThanOrEqualTo(0);

            RuleFor(card => card.Cost)
                .GreaterThanOrEqualTo(0);

            RuleFor(card => card.Description)
                .NotNull()
                .MaximumLength(30);

            this.RuleFor(card => card.Name)
                .NotEmpty()
                .MaximumLength(15);


        }
    }
}
