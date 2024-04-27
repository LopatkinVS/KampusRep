using Kampus.Model.Entities;

namespace Kampus.Data.Interfaces
{
    public interface IProfessorRepository
    {
        ICollection<Professor> GetProfessors();
        Professor GetProfessor(int id);
        ICollection<Professor> GetProfessorsByUniversity(int universityId);
        ICollection<Review> GetAllProfessorReviews(int professorId);
        bool ProfessorExists(int professorId);
        bool CreateProfessor(Professor professor);
        bool UpdateProfessor(Professor professor);
        bool DeleteProfessor(Professor professor);
        bool DeleteProfessors(List<Professor> professors);
        bool Save();
    }
}
