# CoffeeScript

isEmpty = (el) ->
    !$.trim(el.html())


headerMinified = false

$('#articles').on('click', '.minus', (e)->
    articleId = $(this).closest('.article').find('input[name="articleId"]').val()
    markElem = $(this).closest('.article').find('.markNum')
    $.ajax(
        type: "GET",
        url: "/Home/MarkArticle?articleId=#{articleId}&mark=-1"
        success: (data, statusText) ->
            if data != "" then markElem.html(data)
        error: (data) -> 
            response = JSON.parse(data.responseText)
            window.location.href = response.LoginUrl
    )
    e.stopPropagation()
)


$('#articles').on('click', '.plus', (e)->
    articleId = $(this).closest('.article').find('input[name="articleId"]').val()
    markElem = $(this).closest('.article').find('.markNum')
    $.ajax(
        type: "GET"
        url: "/Home/MarkArticle?articleId=#{articleId}&mark=1"
        success: (data, statusText) ->
            if data != "" then markElem.html(data)
        error: (data) -> 
            response = JSON.parse(data.responseText)
            window.location.href = response.LoginUrl
    )
    e.stopPropagation()
)


window.onscroll = ->
    scrolled = window.pageYOffset || document.documentElement.scrollTop
    if scrolled > 5 and not headerMinified
        offset = document.getElementsByClassName('paper-block')[0].getBoundingClientRect().top
        
        $('#main').css('margin-top', offset+scrolled+'px')
        $('header').addClass('header-mini')
        $('header h2').fadeOut('fast')
        $('.addArticleButton').css('top', '90%')
        headerMinified = true
     else 
        if headerMinified and scrolled < 5
            $('header').removeClass('header-mini')
            $('header h2').fadeIn('fast')
            $('#main').css('margin-top', 0)
            $('.addArticleButton').css('top', '')
            headerMinified = false

bigArticle = null            


$('body').click((e) ->
    target = e.target
    while target != e.delegateTarget
        if target == bigArticle
            return;
        target = target.parentNode
    if target == e.delegateTarget
                $(bigArticle).find('.commentsBlock').fadeOut()
                $(bigArticle).find('.fullText').fadeOut()
                $(bigArticle).find('.textPreview').fadeIn()
                $(bigArticle).animate({top: '0', minHeight: '300px'},1)
                $(bigArticle).removeClass('big-article')
                bigArticle = null
)                                         
                                                                        
                                                                                                                                                
$('#articles').on('click', '.article', ->
    if bigArticle != null
        return
    bigArticle = this
    top = this.getBoundingClientRect().top
    offset = 50 - top;
    $(this).animate({top: offset + 'px', minHeight: document.documentElement.clientHeight * 0.8 + "px" },1)
    $(this).addClass('big-article')
    articleId = $(this).find('input[name="articleId"]').val()
    commentsBlock = $(this).find('.commentsBlock')
    textBlock = $(this).find('p.textPreview')
    
    if isEmpty(commentsBlock)
        $.ajax(
            type: "GET"
            url: "/Home/GetArticleText?articleId=#{articleId}"
            success: (data,statusText) ->
                if data != ''
                    textBlock.after($("<p class='fullText' display='none'>#{data}</p>"))
        )
        $.ajax(
            type: "GET"
            url: "/Home/CommentsBlock?articleId=#{articleId}"
            success: (data,statusText)->
                if data != ''
                    commentsBlock.append(data)
                    commentsBlock.find('form').insertFormValidation()
        )
        
    commentsBlock.fadeIn()
    textBlock.css('display', 'none')
    $(this).find('.fullText').fadeIn()
)   

page = 0
_inCallback = false

loadArticles = ->
    if page > -1 && !_inCallback
        _inCallback = true
        page++
        $('img#ajax-loader-gif').css('display', 'block')
        
        $.ajax(
            type: "GET",
            url: "/Home/Index/#{page}"
            success: (data, statusText) ->
                if data != ''
                    $('#articles').append(data)
                else
                    page = -1
                _inCallback = false
                $('img#ajax-loader-gif').css('display', 'none')
         )
        
    
$(window).on('scroll', ->
    
    if $(window).scrollTop() == $(document).height() - $(window).height() 
       loadArticles();
)

        
