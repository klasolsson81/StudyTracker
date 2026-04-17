namespace StudyTracker.Domain.Models;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Class { get; set; } = null!;

    public ICollection<StudySession> Sessions { get; set; } = new List<StudySession>();
}
