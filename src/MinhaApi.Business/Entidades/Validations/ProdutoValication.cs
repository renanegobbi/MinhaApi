using FluentValidation;

namespace MinhaApi.Business.Entidades.Validations
{
    public class ProdutoValidation : AbstractValidator<Produto>
    {
        public ProdutoValidation()
        {
            RuleFor(c => c.FornecedorId)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
                .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que 0.");
            RuleFor(c => c.Descricao)
                .NotNull().WithMessage("O campo Descrição não pode ser nulo.")
                .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");
            RuleFor(c => c.DataFabricacao.Date).LessThan(c => c.DataValidade.Date).WithMessage("A data de fabricação não poderá ser maior ou igual a data de validade.");
        }
    }
}