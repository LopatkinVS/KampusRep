using Kampus.Model.Entities;

namespace Kampus.Data.Interfaces
{
    public interface IUniversityRepository
    {
        ICollection<University> GetUniversities();
        University GetUniversityById(int id);
        ICollection<Professor> GetAllProffesorsFormUniversity(int id);
        bool CreateUniversity(University university);
        bool Save();
    }
}
