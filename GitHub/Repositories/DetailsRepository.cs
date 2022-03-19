using GitHub.Data;
using GitHub.Models;
using GitHub.Repositories.Repository;

namespace GitHub.Repositories
{
    public class DetailsRepository: Repository<Details>, IDetailsRepository
    {
        private readonly AppDbContext _context;
        public DetailsRepository(AppDbContext context):base(context)
        {
            _context = context;
        }

    }
}
