using FluentValidation;
using TestWebPenjualan.Domain.Dtos.Product;

namespace TestWebPenjualan.Domain.Validators;

public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductDtoValidator()
    {
        RuleFor(dto => dto.ProductCode)
            .NotEmpty()
            .WithMessage("Kode Barang harus diisi.");

        RuleFor(dto => dto.Name)
            .NotEmpty()
            .WithMessage("Nama Barang harus diisi.");

        RuleFor(dto => dto.UnitTypeId)
           .NotEmpty()
           .WithMessage("Satuan harus dipilih.");

        RuleFor(dto => dto.CategoryId)
           .NotEmpty()
           .WithMessage("Kategori harus dipilih.");

        RuleFor(dto => dto.BrandId)
           .NotEmpty()
           .WithMessage("Merek harus dipilih.");

        RuleFor(dto => dto.Price)
           .NotEmpty()           
           .WithMessage("Harga jual harus diisi.");
    }
}
