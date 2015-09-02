using System;
using System.Web.Mvc;
using System.Web.Security;
using Blog.BLL.Abstract;
using Blog.BLL.Services;
using Blog.WEB.Filters;
using Blog.WEB.Infrastructure;
using Blog.WEB.Logics;
using Blog.WEB.Models;

namespace Blog.WEB.Controllers
{
    public class HomeController : Controller
    {

        private const int COMMENT_PAGE_LENGTH = 5;
        private const int ARTICLE_PAGE_LENGTH = 2;

        private IBlogService _service;
        private HomeLogics logics;

        public HomeController(IBlogService service, IAuthService auth)
        {
            _service = service;
            logics = new HomeLogics(_service);
        }


        public Int32 GetCurrentUserId()
        {
            if (User.Identity.IsAuthenticated)
            {
                var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                var id = Convert.ToInt32(ticket.UserData);
                return id;
            }
            return 0;
        }


        
        public ActionResult Index(int? id)
        {
            var page = id ?? 0;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_articleList", logics.GetArticlesForPage(page));
            }

            return View(logics.GetArticlesForPage(page));


        }


        [MyAuthorize]
        public ActionResult SubmitComment(CommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                logics.SubmitComment(model, GetCurrentUserId());
            }
            return CommentsListPart(model.ArticleId.Value);
        }


        [MyAuthorize(Roles="Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                logics.Create(model);
            }
            else
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        public PartialViewResult CommentsListPart(Int32 articleId, int page = 1)
        {
            return PartialView("_commentsListPart", logics.GetCommentListForPage(articleId, page));
        }

        public PartialViewResult CommentsBlock(Int32 articleId)
        {

            return PartialView("_comments", logics.GetArticleViewModel(articleId));
        }

        public String GetArticleText(Int32 articleId)
        {
            if (Request.IsAjaxRequest())
            {
                return _service.FindArticle(articleId).Text;
            }
            return "";
        }

        [MyAuthorize]
        public JsonResult MarkArticle(Int32 articleId, int mark)
        {

            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
            var id = Convert.ToInt32(ticket.UserData);
            var newMark = logics.EstimateArticle(articleId, id, mark);
            if (newMark.HasValue)
                return Json(newMark.Value, JsonRequestBehavior.AllowGet);
            else return Json("", JsonRequestBehavior.AllowGet);
        }

        [MyAuthorize(Roles="Admin")]
        public ActionResult Edit(Int32 id)
        {
            var model = logics.GetArticleViewModel(id);
            return View(model);
        }

        [HttpPost]
        [MyAuthorize(Roles="Admin")]
        public ActionResult Edit(ArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                logics.EditArticle(model);
            }
            return RedirectToAction("Index", "Home");
        }


    }
}
