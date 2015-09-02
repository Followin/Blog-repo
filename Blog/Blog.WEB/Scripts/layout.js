(function() {
  var bigArticle, headerMinified, isEmpty, loadArticles, page, _inCallback;

  isEmpty = function(el) {
    return !$.trim(el.html());
  };

  headerMinified = false;

  $('#articles').on('click', '.minus', function(e) {
    var articleId, markElem;
    articleId = $(this).closest('.article').find('input[name="articleId"]').val();
    markElem = $(this).closest('.article').find('.markNum');
    $.ajax({
      type: "GET",
      url: "/Home/MarkArticle?articleId=" + articleId + "&mark=-1",
      success: function(data, statusText) {
        if (data !== "") {
          return markElem.html(data);
        }
      },
      error: function(data) {
        var response;
        response = JSON.parse(data.responseText);
        return window.location.href = response.LoginUrl;
      }
    });
    return e.stopPropagation();
  });

  $('#articles').on('click', '.plus', function(e) {
    var articleId, markElem;
    articleId = $(this).closest('.article').find('input[name="articleId"]').val();
    markElem = $(this).closest('.article').find('.markNum');
    $.ajax({
      type: "GET",
      url: "/Home/MarkArticle?articleId=" + articleId + "&mark=1",
      success: function(data, statusText) {
        if (data !== "") {
          return markElem.html(data);
        }
      },
      error: function(data) {
        var response;
        response = JSON.parse(data.responseText);
        return window.location.href = response.LoginUrl;
      }
    });
    return e.stopPropagation();
  });

  window.onscroll = function() {
    var offset, scrolled;
    scrolled = window.pageYOffset || document.documentElement.scrollTop;
    if (scrolled > 5 && !headerMinified) {
      offset = document.getElementsByClassName('paper-block')[0].getBoundingClientRect().top;
      $('#main').css('margin-top', offset + scrolled + 'px');
      $('header').addClass('header-mini');
      $('header h2').fadeOut('fast');
      $('.addArticleButton').css('top', '90%');
      return headerMinified = true;
    } else {
      if (headerMinified && scrolled < 5) {
        $('header').removeClass('header-mini');
        $('header h2').fadeIn('fast');
        $('#main').css('margin-top', 0);
        $('.addArticleButton').css('top', '');
        return headerMinified = false;
      }
    }
  };

  bigArticle = null;

  $('body').click(function(e) {
    var target;
    target = e.target;
    while (target !== e.delegateTarget) {
      if (target === bigArticle) {
        return;
      }
      target = target.parentNode;
    }
    if (target === e.delegateTarget) {
      $(bigArticle).find('.commentsBlock').fadeOut();
      $(bigArticle).find('.fullText').fadeOut();
      $(bigArticle).find('.textPreview').fadeIn();
      $(bigArticle).animate({
        top: '0',
        minHeight: '300px'
      }, 1);
      $(bigArticle).removeClass('big-article');
      return bigArticle = null;
    }
  });

  $('#articles').on('click', '.article', function() {
    var articleId, commentsBlock, offset, textBlock, top;
    if (bigArticle !== null) {
      return;
    }
    bigArticle = this;
    top = this.getBoundingClientRect().top;
    offset = 50 - top;
    $(this).animate({
      top: offset + 'px',
      minHeight: document.documentElement.clientHeight * 0.8 + "px"
    }, 1);
    $(this).addClass('big-article');
    articleId = $(this).find('input[name="articleId"]').val();
    commentsBlock = $(this).find('.commentsBlock');
    textBlock = $(this).find('p.textPreview');
    if (isEmpty(commentsBlock)) {
      $.ajax({
        type: "GET",
        url: "/Home/GetArticleText?articleId=" + articleId,
        success: function(data, statusText) {
          if (data !== '') {
            return textBlock.after($("<p class='fullText' display='none'>" + data + "</p>"));
          }
        }
      });
      $.ajax({
        type: "GET",
        url: "/Home/CommentsBlock?articleId=" + articleId,
        success: function(data, statusText) {
          if (data !== '') {
            commentsBlock.append(data);
            return commentsBlock.find('form').insertFormValidation();
          }
        }
      });
    }
    commentsBlock.fadeIn();
    textBlock.css('display', 'none');
    return $(this).find('.fullText').fadeIn();
  });

  page = 0;

  _inCallback = false;

  loadArticles = function() {
    if (page > -1 && !_inCallback) {
      _inCallback = true;
      page++;
      $('img#ajax-loader-gif').css('display', 'block');
      return $.ajax({
        type: "GET",
        url: "/Home/Index/" + page,
        success: function(data, statusText) {
          if (data !== '') {
            $('#articles').append(data);
          } else {
            page = -1;
          }
          _inCallback = false;
          return $('img#ajax-loader-gif').css('display', 'none');
        }
      });
    }
  };

  $(window).on('scroll', function() {
    if ($(window).scrollTop() === $(document).height() - $(window).height()) {
      return loadArticles();
    }
  });

}).call(this);

//# sourceMappingURL=layout.js.map
