using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blog.BLL.Abstract;
using Blog.BLL.DTO;
using Blog.BLL.Models;
using Blog.DAL.Abstract;
using Blog.DAL.Entities;

namespace Blog.BLL.Services
{
    public class BlogService : IBlogService
    {
        private IUnitOfWork _db;
        private ICommentFilter _filter;

        public BlogService(IUnitOfWork db, ICommentFilter filter)
        {
            _db = db;
            _filter = filter;
        }
        public IEnumerable<ArticleDTO> GetAllAtricles()
        {
            Mapper.CreateMap<Comment, CommentDTO>();
            Mapper.CreateMap<User, UserDTO>();
            Mapper.CreateMap<Article, ArticleDTO>()
                .ForMember(d => d.Comments, c => c.MapFrom(a => a.Comments));
            Mapper.CreateMap<UserArticleMark, UserArticleMarkDTO>();
            return Mapper.Map<IEnumerable<Article>, List<ArticleDTO>>(_db.Articles.GetAll());
        }

        public void AddComment(CommentDTO comment)
        {
            Mapper.CreateMap<CommentDTO, Comment>();
            _db.Comments.Create(Mapper.Map<CommentDTO, Comment>(comment));
            _db.Save();
        }


        public void DeleteComment(Int32 id)
        {
            _db.Comments.Delete(id);
        }

        public void DeleteArticle(Int32 id)
        {
            _db.Articles.Delete(id);
        }

        public void ModifyArticle(ArticleDTO article)
        {
            Mapper.CreateMap<CommentDTO, Comment>();
            Mapper.CreateMap<ArticleDTO, Article>().ForMember(x => x.Comments, _ => _.MapFrom(x => x.Comments));
            _db.Articles.Update(Mapper.Map<Article>(article));
            _db.Save();
        }

        public ArticleDTO FindArticle(int id)
        {
            Mapper.CreateMap<Article, ArticleDTO>().ForMember(x => x.Comments, _ => _.MapFrom(x => x.Comments));
            Mapper.CreateMap<Comment, CommentDTO>();
            Mapper.CreateMap<User, UserDTO>();
            var comments = _db.Comments.GetAll().ToList();
            return Mapper.Map<ArticleDTO>(_db.Articles.Get(id));
        }

        public void CreateArticle(ArticleDTO article)
        {
            Mapper.CreateMap<ArticleDTO, Article>();
            article.DateTime = DateTime.Now;
            _db.Articles.Create(Mapper.Map<Article>(article));
            _db.Save();
        }

        public EstimateArticleResult EstimateArticle(int userId, int articleId, int mark)
        {
            var result = new EstimateArticleResult {Status = EstimateStatuses.Success};
            var user = _db.Users.Get(userId);
            var article = _db.Articles.Get(articleId);
            if (user != null && article != null)
            {
                var existingMark = user.UserArticleMarks.FirstOrDefault(_ => _.ArticleId == articleId);
                if (existingMark != null)
                {
                    result.Status = EstimateStatuses.Existing;
                    return result;
                }
                _db.UserArticleMarks.Create(new UserArticleMark {ArticleId = articleId, UserId = userId, Mark = mark});
                _db.Save();
                result.Mark = article.UserArticleMarks.Sum(x => x.Mark);
                
            }
            else
            {
                result.Status = EstimateStatuses.Error;
                return result;
            }
            
            return result;
        }
    }
}
