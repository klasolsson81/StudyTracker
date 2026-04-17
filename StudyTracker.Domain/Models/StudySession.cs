namespace StudyTracker.Domain.Models;

public class StudySession
{
    public int Id { get; set; }
    public string Subject { get; set; } = null!;
    public DateTime Date { get; set; }
    public int DurationMinutes { get; set; }

    // Skala 1-10. Motivation före passet.
    public int MotivationBefore { get; set; }

    // Skala 1-10. Motivation efter passet — skillnaden visar hur studierna påverkade måendet.
    public int MotivationAfter { get; set; }

    public string? Notes { get; set; }

    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;
}
