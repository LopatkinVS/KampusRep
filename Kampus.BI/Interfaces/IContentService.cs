using Kampus.Model.Entities;
using System.Net;

namespace Kampus.BI.Interfaces
{
    public interface IContentService
    {
        void CreateUniversity(University university);
        void UpdateUniversity(University university);
        void DeleteUniversity(int universityId);
        University GetUniversity(int unverityId);
        University GetUniversity(string universityName);
        List<University> GetUniversityes();
        List<Professor> GetAllUniversityProfessor(int universityId);
        void CreateProfessor(Professor professor);
        void UpdateProfessor(Professor professor);
        void DeleteProfessor(int professorId);
        void CountProfessorRating(int professorId);
        Professor GetProfessor(int professorId);
        Professor GetProfessor(string professorName);
        List<Professor> GetProfessors();
        List<Review> GetProfessorReviews(int professorId);
        List<Review> GetProfessorReviews(string professorName);
    }
}
