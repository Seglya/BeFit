$('#choose').click(function (e) {
    e.stopPropagation();
    $(this).siblings('select').css('width', $(this).outerWidth(true)).toggle();
});

$('#ExerciseList').change(function (e) {
    e.stopPropagation();
    var val = this.value || 'Select exercise';
    $(this).siblings('#choose').text(val);
    $(this).hide();
});

$('select').find('option').on({
    'mouseover': function () {
        $('.hover').removeClass('hover');
        $(this).addClass('hover');
    },
    'mouseleave': function () {
        $(this).removeClass('hover');
    }
});