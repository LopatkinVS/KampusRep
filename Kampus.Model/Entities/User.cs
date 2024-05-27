using Kampus.Model.Abstract;

namespace Kampus.Model.Entities
{
    public class User : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
