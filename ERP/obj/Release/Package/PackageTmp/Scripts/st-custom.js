

(function () {



    $('.fa-search').on('click', function () {

        $('.search').fadeIn(500, function () {

            $(this).toggleClass('search-toggle');

        });

    });



    $('.search-close').on('click', function () {

        $('.search').fadeOut(500, function () {

            $(this).removeClass('search-toggle');

        });

    });



}());


$(window).resize(function () {
	   if ($(this).width() < 769) {

        $('body').addClass('body-small');

    } else {

        $('body').removeClass('body-small');

    }
});


$(document).ready(function () {



   

    if ($(this).width() < 769) {

        $('body').addClass('body-small');

    } else {

        $('body').removeClass('body-small');

    }




    $('#side-menu').metisMenu();




    $('.navbar-minimalize').click(function () {

        $("body").toggleClass("mini-navbar");





    });

              $(".nano").nanoScroller();

});



// Panels

(function ($) {



    $(function () {

        $('.panel')

                .on('panel:toggle', function () {

                    var $this,

                            direction;



                    $this = $(this);

                    direction = $this.hasClass('panel-collapsed') ? 'Down' : 'Up';



                    $this.find('.panel-body, .panel-footer')[ 'slide' + direction ](200, function () {

                        $this[ (direction === 'Up' ? 'add' : 'remove') + 'Class' ]('panel-collapsed')

                    });

                })

                .on('panel:dismiss', function () {

                    var $this = $(this);



                    if (!!($this.parent('div').attr('class') || '').match(/col-(xs|sm|md|lg)/g) && $this.siblings().length === 0) {

                        $row = $this.closest('.row');

                        $this.parent('div').remove();

                        if ($row.children().length === 0) {

                            $row.remove();

                        }

                    } else {

                        $this.remove();

                    }

                })

                .on('click', '[data-panel-toggle]', function (e) {

                    e.preventDefault();

                    $(this).closest('.panel').trigger('panel:toggle');

                })

                .on('click', '[data-panel-dismiss]', function (e) {

                    e.preventDefault();

                    $(this).closest('.panel').trigger('panel:dismiss');

                })

                /* Deprecated */

                .on('click', '.panel-actions a.fa-caret-up', function (e) {

                    e.preventDefault();

                    var $this = $(this);



                    $this

                            .removeClass('fa-caret-up')

                            .addClass('fa-caret-down');



                    $this.closest('.panel').trigger('panel:toggle');

                })

                .on('click', '.panel-actions a.fa-caret-down', function (e) {

                    e.preventDefault();

                    var $this = $(this);



                    $this

                            .removeClass('fa-caret-down')

                            .addClass('fa-caret-up');



                    $this.closest('.panel').trigger('panel:toggle');

                })

                .on('click', '.panel-actions a.fa-times', function (e) {

                    e.preventDefault();

                    var $this = $(this);



                    $this.closest('.panel').trigger('panel:dismiss');

                });

    });



})(jQuery);



//tooltips

$(function () {

    $('[data-toggle="tooltip"]').tooltip();

    $('[data-toggle="popover"]').popover()

});



