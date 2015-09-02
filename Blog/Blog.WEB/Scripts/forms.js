(function() {
  jQuery.fn.insertFormValidation = function() {
    var $form, changeFormClass, group, input, inputValGroups, _i, _len;
    $form = $(this);
    $form.validateParse();
    changeFormClass = function(inputEl) {
      var el, formGroup;
      el = $(inputEl);
      formGroup = el.closest('.form-group');
      if (el.is(':focus')) {
        formGroup.addClass('has-focus');
      } else {
        formGroup.removeClass('has-focus');
      }
      if (el.val() === '') {
        return formGroup.addClass('is-empty');
      } else {
        return formGroup.removeClass('is-empty');
      }
    };
    $form.find('.form-group:has("textarea")').addClass('text-area-group');
    $form.find('textarea').autogrow();
    $form.find('input[type="radio"]').closest('.form-group').addClass('radio-group');
    inputValGroups = $form.find('.form-group:has("input"), .form-group:has("textarea")');
    for (_i = 0, _len = inputValGroups.length; _i < _len; _i++) {
      group = inputValGroups[_i];
      input = $(group).find("input[type='text'], input[type='password'], input[type='email'],  textarea");
      input.on('focus', function() {
        return changeFormClass(this);
      });
      input.on('blur', function() {
        return changeFormClass(this);
      });
      changeFormClass(input);
    }
    return changeFormClass($("input[type='date']"));
  };

  $('form').insertFormValidation();

  $("input[type='date']").pickadate({
    selectMonth: true,
    selectYears: 15,
    labelMonthNext: 'Next Month',
    labelMonthPrev: 'Previous Month',
    labelMonthSelect: 'Select a Month',
    labelYearSelect: 'Select a Year',
    format: "mm/dd/yyyy",
    container: "#date-picker",
    onClose: function() {
      return this.$node.valid();
    },
    onOpen: function() {
      return this.$node.closest('.form-group').addClass('has-focus');
    },
    onClose: function() {
      return this.$node.closest('.form-group').removeClass('has-focus');
    },
    onSet: function(value) {
      if (value.select != null) {
        return this.$node.closest('.form-group').removeClass('is-empty');
      }
    }
  });

}).call(this);

//# sourceMappingURL=forms.js.map
