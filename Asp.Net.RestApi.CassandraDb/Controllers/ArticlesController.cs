using Cassandra.Data.Linq;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using UseCases;

namespace Asp.Net.RestApi.CassandraDb.Controllers;

[ApiController]
[Route("[controller]")]
public class ArticlesController(
    ILogger<ArticlesController> logger, 
    ICassandraDbRepository cassandraDbRepository
    ) 
    : ControllerBase
{

    [HttpGet]//7ac78a01-a165-ac5f-2fd3-356eaf233808,    fec78a01-17ff-3158-31bd-bf77d52232b7
    public Article GetArticle([FromQuery] QueryArticleDisplay request)
    {
        cassandraDbRepository.Connect();
        logger.LogInformation($"{this.HttpContext.Request.Method} {this.HttpContext.Request.Path}");
        return cassandraDbRepository.Articles()
            .Where(x => x.ArticleId == request.ArticleId && x.ArticleVersionId == request.ArticleVersionId)
            .FirstOrDefault()
            .Execute();
    }

    [HttpGet("Versions/")]//7ac78a01-a165-ac5f-2fd3-356eaf233808
    public IEnumerable<Article> GetArticleVersions([FromQuery] QueryArticleVersions request)
    {
        cassandraDbRepository.Connect();
        logger.LogInformation($"{this.HttpContext.Request.Method} {this.HttpContext.Request.Path}");
        return cassandraDbRepository.Articles()
            .Where(x => x.ArticleId == request.ArticleId).Execute();
    }

    [HttpPost]
    public Article CreateArticle([FromForm] CommandArticleCreate request)
    {
        cassandraDbRepository.Connect();
        logger.LogInformation($"{this.HttpContext.Request.Method} {this.HttpContext.Request.Path}");
        var article = Article.Create(request.Title, request.Description, request.Text);
        cassandraDbRepository.Articles().Insert(article).Execute();
        return article;
    }

    [HttpPost("Version/")]
    public Article CreateArticleVersion([FromForm] CommandArticleCreateVersion request)
    {
        cassandraDbRepository.Connect();
        logger.LogInformation($"{this.HttpContext.Request.Method} {this.HttpContext.Request.Path}");
        var article = cassandraDbRepository.Articles()
            .Where(x => 
                x.ArticleId == request.ArticleId 
            &&  x.ArticleVersionId == request.ArticleId)
            .FirstOrDefault()
            .Execute();
        if(article == null)
        {
            return default(Article);
        }
        var articleVersion = article.CreateVersion(request.Title, request.Description, request.Text);
        cassandraDbRepository.Articles().Insert(articleVersion).Execute();
        return articleVersion;
    }



    [HttpDelete]
    public Article DeleteArticle([FromForm] CommandArticleDelete request)
    {
        cassandraDbRepository.Connect();
        logger.LogInformation($"{this.HttpContext.Request.Method} {this.HttpContext.Request.Path}");
        var article = cassandraDbRepository.Articles()
                .Where(x =>
                            x.ArticleId == request.ArticleId
                        &&  x.ArticleVersionId == request.ArticleId)
                .FirstOrDefault()
                .Execute();
        if(article != null)
        {
            cassandraDbRepository.Articles()
                .Where(x => x.ArticleId == request.ArticleId && x.ArticleVersionId == request.ArticleId)
                .Delete()
                .Execute();
        }

        return article;
    }





}