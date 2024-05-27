using Kampus.Data.Abstract;
using Kampus.Model.Entities;
using System.Net.Http.Headers;

namespace Kampus.Data.Repositories
{
    public class ProfessorRepository : EntityBaseRepository<Professor, KampusContext>, IProfesorRepository
    {
        public ProfessorRepository(KampusContext context) : base(context) 
        { }
    }

    public class ReviewRepository : EntityBaseRepository<Review, KampusContext>, IReviewRepository
    {
        public ReviewRepository(KampusContext context) : base (context) 
        { }
    }

    public class UniversityRepository : EntityBaseRepository<University, KampusContext>, IUniversityRepository
    {
        public UniversityRepository(KampusContext context) : base (context) 
        { }
    }

    public class UserRepository : EntityBaseRepository<User, KampusContext>, IUserRepository
    {
        public UserRepository(KampusContext context) : base(context) 
        { }
    }
}
