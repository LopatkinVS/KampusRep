using Kampus.BI.Services.Abstract;
using Kampus.Data.Interfaces;
using Kampus.Model.Entities;

namespace Kampus.BI.Services
{
    public class ServiceUser : IServiceUser
    {
        private readonly IProfessorRepository _professorRepository;

        public ServiceUser(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }

        public bool Create(Professor entity)
        {
            throw new NotImplementedException();
        }
    }
}
