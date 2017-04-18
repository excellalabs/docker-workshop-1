using Articles.Entities;

namespace ConsoleApplication.Infrastructure.Repositories
{
    public class ArticlesRepository : BaseRepository<Article>, IArticlesRepository
    {
        public ArticlesRepository(ArticlesContext context)
            : base(context)
        { }
    }
}