namespace Kampus.Model.Entities
{
    public class University
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Professor> Professors { get; set; }
    }
}
