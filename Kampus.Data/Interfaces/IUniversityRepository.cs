using Kampus.Model.Entities;

namespace Kampus.Data.Interfaces
{
    public interface IUniversityRepository
    {
        ICollection<University> GetUniversities();
        University GetUniversityById(int id);
        ICollection<Professor> GetAllProffesorsFormUniversity(int id);
        bool UniversityExists(int universityId);
        bool CreateUniversity(University university);
        bool UpdateUniversity(University university);
        bool DeleteUniversity(University university);
        bool Save();
    }
}
