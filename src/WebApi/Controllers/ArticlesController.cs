using Microsoft.AspNetCore.Mvc;
using Domain.Entities;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ArticlesController : ControllerBase
{
    private static readonly List<JournalArticle> Articles = new()
    {
        new JournalArticle
        {
            Title = "Automated AI Security & Vulnerability Remediation in Distributed Systems",
            Abstract = "An empirical study on real-time vulnerability detection via LLM agents.",
            AuthorEmail = "ahmar@example.com",
            Doi = "10.1000/182",
            Status = "Published",
            PublishedAt = DateTime.UtcNow
        }
    };

    [HttpGet]
    public ActionResult<IEnumerable<JournalArticle>> GetArticles()
    {
        return Ok(Articles);
    }

    [HttpPost]
    public ActionResult<JournalArticle> SubmitArticle([FromBody] JournalArticle article)
    {
        article.Id = Guid.NewGuid();
        article.CreatedAt = DateTime.UtcNow;
        article.Status = "Submitted";
        Articles.Add(article);
        return CreatedAtAction(nameof(GetArticles), new { id = article.Id }, article);
    }
}

