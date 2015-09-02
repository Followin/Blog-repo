# CoffeeScript

jQuery.fn.insertFormValidation = ->
    $form = $(this)
    $form.validateParse()
    changeFormClass = (inputEl) ->
    
        el = $(inputEl)
        formGroup = el.closest('.form-group')
        if el.is(':focus')  then formGroup.addClass('has-focus') else formGroup.removeClass('has-focus')
        if el.val() == '' then formGroup.addClass('is-empty') else formGroup.removeClass('is-empty')
    


    $form.find('.form-group:has("textarea")').addClass('text-area-group')
    $form.find('textarea').autogrow()         

    $form.find('input[type="radio"]').closest('.form-group').addClass('radio-group')


    inputValGroups = $form.find('.form-group:has("input"), .form-group:has("textarea")')


    for group in inputValGroups 
        input = $(group).find("input[type='text'], input[type='password'], input[type='email'],  textarea")
        input.on('focus', -> changeFormClass(this))
        input.on('blur', -> changeFormClass(this))
        changeFormClass(input)
        
    changeFormClass($("input[type='date']"))


$('form').insertFormValidation()

$("input[type='date']").pickadate(
    selectMonth: true,
    selectYears: 15,
    labelMonthNext: 'Next Month',
    labelMonthPrev: 'Previous Month',
    labelMonthSelect: 'Select a Month',
    labelYearSelect: 'Select a Year',
    format: "mm/dd/yyyy",
    
    container: "#date-picker",
    onClose: -> this.$node.valid(); 
    onOpen: -> this.$node.closest('.form-group').addClass('has-focus');
    onClose: -> this.$node.closest('.form-group').removeClass('has-focus');
    onSet: (value) ->  
        if value.select? 
            this.$node.closest('.form-group').removeClass('is-empty'); 
 )

 
