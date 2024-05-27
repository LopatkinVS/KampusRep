using Kampus.Model.Abstract;

namespace Kampus.Model.Entities
{
    public class Review : IEntityBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Rank {  get; set; }
        public DateTime CreationTime { get; set; }
        public User User { get; set; }
        public Professor Professor { get; set; }
    }
}
