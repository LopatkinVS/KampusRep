using Kampus.Model.Abstract;

namespace Kampus.Model.Entities
{
    public class Professor : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public float Raiting { get; set; }
        public University University { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
