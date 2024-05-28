using Kampus.BI.Interfaces;
using Kampus.Data.Abstract;
using Kampus.Model.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Reflection.Metadata.Ecma335;

namespace Kampus.BI.Services
{
    public class ContentService : IContentService
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly IProfesorRepository _profsorRepository;
        private readonly IReviewRepository _reviewRepository;

        public ContentService(IUniversityRepository universityRepository,
            IProfesorRepository profesorRepository,
            IReviewRepository reviewRepository)
        {
            _universityRepository = universityRepository;
            _profsorRepository = profesorRepository;
            _reviewRepository = reviewRepository;
        }

        public void CreateUniversity(University university)
        {
            _universityRepository.Create(university);
            _universityRepository.Save();
        }

        public void UpdateUniversity(University university)
        {
            _universityRepository.Update(university);
            _universityRepository.Save();
        }

        public void DeleteUniversity(int universityId)
        {
            List<Professor> professorsToDelete = GetAllUniversityProfessor(universityId);
            List<Review> ReviewToDelete = new List<Review>();

            foreach (var professor in professorsToDelete)
            {
                ReviewToDelete.AddRange(GetProfessorReviews(professor.Id));
            }

            _universityRepository.Delete(GetUniversity(universityId));
            _profsorRepository.DeleteRange(professorsToDelete);
            _reviewRepository.DeleteRange(ReviewToDelete);
            _profsorRepository.Save();
            _reviewRepository.Save();
            _universityRepository.Save();
        }

        public University GetUniversity(int unverityId)
        {
            return _universityRepository.GetSingle(unverityId);
        }

        public University GetUniversity(string universityName)
        {
            return _universityRepository.GetSingle(universityName);
        }

        public List<University> GetUniversityes()
        {
            return _universityRepository.GetAll().ToList();
        }

        public List<Professor> GetAllUniversityProfessor(int universityId)
        { 
            return _universityRepository.GetSingle(universityId).Professors.ToList();
        }

        public void CreateProfessor(Professor professor)
        {
            _profsorRepository.Create(professor);
            _profsorRepository.Save();
        }

        public void UpdateProfessor(Professor professor)
        {
            _profsorRepository.Update(professor);
            _profsorRepository.Save();
        }

        public void DeleteProfessor(int professorId)
        {
            var reviewsToDelete = GetProfessorReviews(professorId);

            _reviewRepository.DeleteRange(reviewsToDelete);
            _profsorRepository.Delete(GetProfessor(professorId));
            _reviewRepository.Save();
            _profsorRepository.Save();
        }

        public void CountProfessorRating(int professorId)
        {
            var professor = _profsorRepository.GetSingle(professorId);
            var reviews = GetProfessorReviews(professorId);
            float sum = 0;

            foreach (var review in reviews)
                sum += review.Rank;

            professor.Raiting = sum /reviews.Count(); 
        }

        public Professor GetProfessor(int professorId)
        {
            return _profsorRepository.GetSingle(professorId);
        }

        public Professor GetProfessor(string professorName)
        {
            return _profsorRepository.GetSingle(professorName);
        }

        public List<Professor> GetProfessors()
        {
            return _profsorRepository.GetAll().ToList();
        }

        public List<Review> GetProfessorReviews(int professorId)
        {
            return _profsorRepository.GetSingle(professorId).Reviews.ToList(); 
        }

        public List<Review> GetProfessorReviews(string professorName)
        {
            return _profsorRepository.GetSingle(professorName).Reviews.ToList();
        }
    }
}
