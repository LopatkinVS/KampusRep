using Kampus.Model.Abstract;

namespace Kampus.Model.Entities
{
    public class Professor : IEntityBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Raiting { get; set; }
        public University University { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
