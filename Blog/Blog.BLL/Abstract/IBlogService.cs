using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Blog.BLL.DTO;
using Blog.BLL.Models;

namespace Blog.BLL.Abstract
{
    public interface IBlogService
    {
        IEnumerable<ArticleDTO> GetAllAtricles();
        void AddComment(CommentDTO comment);
        void DeleteComment(Int32 id);
        void DeleteArticle(Int32 id);
        void ModifyArticle(ArticleDTO article);

        ArticleDTO FindArticle(int id);
        void CreateArticle(ArticleDTO article);

        EstimateArticleResult EstimateArticle(Int32 userId, Int32 articleId, Int32 mark);


    }
}
