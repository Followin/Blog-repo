using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using AutoMapper;
using Blog.BLL.Abstract;
using Blog.BLL.DTO;
using Blog.BLL.Models;
using Blog.WEB.Models;

namespace Blog.WEB.Logics
{
    public class HomeLogics
    {
        private const int COMMENT_PAGE_LENGTH = 5;
        private const int ARTICLE_PAGE_LENGTH = 2;

        private IBlogService _service;

        public HomeLogics(IBlogService service)
        {
            _service = service;
        }

        public List<ArticleViewModel> GetArticlesForPage(int page = 1)
        {
            Mapper.CreateMap<CommentDTO, CommentViewModel>();
            Mapper.CreateMap<ArticleDTO, ArticleViewModel>().ForMember(_ => _.Comments, x => x.MapFrom(_ => _.Comments));
            var articles = Mapper.Map<IEnumerable<ArticleViewModel>>(_service.GetAllAtricles())
                .OrderBy(x => x.Id)
                .Skip(page * ARTICLE_PAGE_LENGTH)
                .Take(ARTICLE_PAGE_LENGTH);

            var result = new List<ArticleViewModel>();
            foreach (var article in articles)
            {
                if (article.Text.Length > 400)
                    article.Text = article.Text.Remove(400);
                result.Add(article);
            }

            return result;
        }

        public void SubmitComment(CommentViewModel model, Int32 userId)
        {
            Mapper.CreateMap<CommentViewModel, CommentDTO>();
            var articleDTO = Mapper.Map<CommentDTO>(model);
            articleDTO.UserId = userId;
            _service.AddComment(articleDTO);
        }

        public void Create(ArticleViewModel model)
        {
            Mapper.CreateMap<ArticleViewModel, ArticleDTO>();
            _service.CreateArticle(Mapper.Map<ArticleDTO>(model));
        }

        public CommentsListViewModel GetCommentListForPage(Int32 articleId, Int32 page = 1)
        {
            Mapper.CreateMap<CommentDTO, CommentViewModel>();
            Mapper.CreateMap<UserDTO, UserViewModel>();
            var comments =
                Mapper.Map<IEnumerable<CommentViewModel>>(_service.FindArticle(articleId).Comments).ToList();
            var commentsListViewModel = new CommentsListViewModel
            {
                ArticleId = articleId,
                Comments = comments
                    .OrderByDescending(x => x.Id)
                    .Skip((page - 1) * COMMENT_PAGE_LENGTH)
                    .Take(COMMENT_PAGE_LENGTH).ToList(),

                Info = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = COMMENT_PAGE_LENGTH,
                    TotalItems = comments.Count
                }
            };
            return commentsListViewModel;
        }

        public ArticleViewModel GetArticleViewModel(Int32 articleId)
        {
            Mapper.CreateMap<CommentDTO, CommentViewModel>();
            Mapper.CreateMap<ArticleDTO, ArticleViewModel>().ForMember(_ => _.Comments, x => x.MapFrom(_ => _.Comments));
            return Mapper.Map<ArticleViewModel>(_service.FindArticle(articleId));
        }

        public Int32? EstimateArticle(Int32 articleId, Int32 userId, Int32 mark)
        {
            var result = _service.EstimateArticle(userId, articleId, mark);
            if (result.Status == EstimateStatuses.Success)
                return result.Mark;
            return null;
        }

        public void EditArticle(ArticleViewModel model)
        {
            Mapper.CreateMap<CommentViewModel, CommentDTO>();
            Mapper.CreateMap<ArticleViewModel, ArticleDTO>().ForMember(_ => _.Comments, x => x.MapFrom(_ => _.Comments));
            _service.ModifyArticle(Mapper.Map<ArticleDTO>(model));

        }

    }
}