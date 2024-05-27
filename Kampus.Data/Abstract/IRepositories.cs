using Kampus.Model.Entities;

namespace Kampus.Data.Abstract
{
    public interface IProfesorRepository : IEntityBaseRepository<Professor> { }
    public interface IReviewRepository : IEntityBaseRepository<Review> { }
    public interface IUniversityRepository : IEntityBaseRepository<University> { }
    public interface IUserRepository : IEntityBaseRepository<User> { }
}
