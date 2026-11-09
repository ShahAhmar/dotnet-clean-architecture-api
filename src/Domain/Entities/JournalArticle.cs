namespace Domain.Entities;

public class JournalArticle
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Abstract { get; set; } = string.Empty;
    public string Doi { get; set; } = string.Empty;
    public string AuthorEmail { get; set; } = string.Empty;
    public string Status { get; set; } = "Submitted"; // Submitted, UnderReview, Accepted, Published
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? PublishedAt { get; set; }

    public void MarkAsPublished(string assignedDoi)
    {
        Status = "Published";
        Doi = assignedDoi;
        PublishedAt = DateTime.UtcNow;
    }
}

