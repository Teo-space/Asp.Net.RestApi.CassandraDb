namespace UseCases;

public record CommandArticleDelete(Guid ArticleId, Guid ArticleVersionId)
{
    public class Validator : AbstractValidator<CommandArticleDelete>
    {
        public Validator()
        {
            RuleFor(x => x.ArticleId).NotNull();
            RuleFor(x => x.ArticleVersionId).NotNull();
        }
    }
}

