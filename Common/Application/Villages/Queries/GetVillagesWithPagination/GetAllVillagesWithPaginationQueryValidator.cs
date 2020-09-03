namespace Application.Villages.Queries.GetVillagesWithPagination
{
    using FluentValidation;

    public class GetAllVillagesWithPaginationQueryValidator : AbstractValidator<GetAllVillagesWithPaginationQuery>
    {
        public GetAllVillagesWithPaginationQueryValidator()
        {
            RuleFor(x=>x.DistrictId)
                .NotNull()
                .NotEmpty().WithMessage("DistrictId is required.");

            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
        }
    }
}
