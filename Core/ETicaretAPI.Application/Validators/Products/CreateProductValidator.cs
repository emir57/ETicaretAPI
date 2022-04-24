using ETicaretAPI.Application.ViewModels.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen ürün adını boş geçmeyiniz")
                .MaximumLength(150)
                .MinimumLength(1)
                    .WithMessage("Lütfen ürün adını 1 ile 150 karakter arasında giriniz.");

            RuleFor(p => p.Stock)
                .NotNull()
                .NotEmpty()
                    .WithMessage("Lütfen stok bilgisini boş geçmeyiniz.")
                .Must(s => s >= 0)
                    .WithMessage("Stok bilgisi negatif olamaz.");

            RuleFor(p => p.Price)
                .NotNull()
                .NotEmpty()
                    .WithMessage("Lütfen fiyat bilgisini boş geçmeyiniz.")
                .Must(s => s >= 0)
                    .WithMessage("Fiyat bilgisi negatif olamaz.");
        }
    }
}
