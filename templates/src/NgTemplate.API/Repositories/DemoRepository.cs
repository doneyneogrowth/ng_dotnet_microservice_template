using NgTemplate.API.Data;
using NgTemplate.API.Entities;

namespace NgTemplate.API.Repositories
{
    public class DemoRepository : GenericRepository<Demo>, IDemoRepository
    {
        public DemoRepository(AppDBContext context)
        : base(context)
        {
        }
    }
}