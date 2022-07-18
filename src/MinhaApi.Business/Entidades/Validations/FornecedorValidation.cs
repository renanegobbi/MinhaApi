using FluentValidation;
using MinhaApi.Core.Validations.Documentos;

namespace MinhaApi.Business.Entidades.Validations
{
    public class FornecedorValidation : AbstractValidator<Fornecedor>
    {
        public FornecedorValidation()
        {
            RuleFor(f => f.Nome)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
            .Length(2, 100)
            .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(f => f.Cnpj)
                .NotNull()
                .Length(CnpjValidacao.TamanhoCnpj).WithMessage("O campo Cnpj precisa ter 14 dígitos.");

            When(f => f.Cnpj != null, () =>
            {
                RuleFor(f => CnpjValidacao.Validar(f.Cnpj)).Equal(true)
                .WithMessage("O campo Cnpj fornecido é inválido.");
            });
        }
    }
}