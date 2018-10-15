var range = $('.input-range'),
  value = $('.range-value');

value.html(range.attr('value') + ' &#8362;');

range.on('input', function() {
    value.html(this.value + ' &#8362;');
});