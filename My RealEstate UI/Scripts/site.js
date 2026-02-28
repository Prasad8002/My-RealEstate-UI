/* ============================================================
   DWELLO – site.js
   Author  : Senior UI Developer
   Stack   : ASP.NET MVC (.NET Framework)
   ============================================================ */

(function ($) {
    'use strict';

    /* ----------------------------------------------------------
       1. MOBILE NAVBAR TOGGLE
    ---------------------------------------------------------- */
    $('#hamburgerBtn').on('click', function () {
        var $menu = $('#mobileMenu');
        $menu.toggleClass('open');
    });

    // Close mobile menu when clicking outside
    $(document).on('click', function (e) {
        if (!$(e.target).closest('.navbar').length) {
            $('#mobileMenu').removeClass('open');
        }
    });

    /* ----------------------------------------------------------
       2. TESTIMONIAL SLIDER
    ---------------------------------------------------------- */
    var $track     = $('#testimonialTrack');
    var $cards     = $track.find('.testimonial-card');
    var totalCards = $cards.length;
    var current    = 0;
    var visibleCount = getVisibleCount();

    function getVisibleCount() {
        var w = $(window).width();
        if (w < 768)  return 1;
        if (w < 1024) return 2;
        return 3;
    }

    function goToSlide(index) {
        var maxIndex = totalCards - visibleCount;
        if (index < 0) index = maxIndex;
        if (index > maxIndex) index = 0;
        current = index;

        // Calculate percentage per card
        var pct = (100 / visibleCount) * current;
        $track.css('transform', 'translateX(-' + pct + '%)');
    }

    // Only init slider if we have more cards than visible
    function initSlider() {
        visibleCount = getVisibleCount();
        if (totalCards <= visibleCount) {
            // No scrolling needed — reset
            $track.css('transform', 'translateX(0)');
            return;
        }

        // Set grid so cards behave like a slider row
        var cardWidthPct = (100 / visibleCount).toFixed(4) + '%';
        $track.css({
            'display'               : 'flex',
            'grid-template-columns' : 'none',
            'gap'                   : '20px'
        });

        $cards.css({
            'min-width': 'calc(' + cardWidthPct + ' - 14px)',
            'flex'     : '0 0 calc(' + cardWidthPct + ' - 14px)'
        });

        goToSlide(current);
    }

    $('#nextBtn').on('click', function () { goToSlide(current + 1); });
    $('#prevBtn').on('click', function () { goToSlide(current - 1); });

    // Auto-play every 5 seconds
    var autoPlay = setInterval(function () {
        goToSlide(current + 1);
    }, 5000);

    // Pause on hover
    $('#testimonialSlider').on('mouseenter', function () {
        clearInterval(autoPlay);
    }).on('mouseleave', function () {
        autoPlay = setInterval(function () {
            goToSlide(current + 1);
        }, 5000);
    });

    // Swipe support (touch devices)
    var touchStartX = 0;
    $('#testimonialSlider')[0].addEventListener('touchstart', function (e) {
        touchStartX = e.changedTouches[0].screenX;
    }, { passive: true });

    $('#testimonialSlider')[0].addEventListener('touchend', function (e) {
        var diff = touchStartX - e.changedTouches[0].screenX;
        if (Math.abs(diff) > 50) {
            goToSlide(diff > 0 ? current + 1 : current - 1);
        }
    }, { passive: true });

    /* ----------------------------------------------------------
       3. SEARCH BAR – ACTIVE STATE
    ---------------------------------------------------------- */
    $('.search-bar__field select').on('focus', function () {
        $(this).closest('.search-bar__field').addClass('focused');
    }).on('blur', function () {
        $(this).closest('.search-bar__field').removeClass('focused');
    });

    /* ----------------------------------------------------------
       4. SCROLL-REVEAL (lightweight, no plugin required)
    ---------------------------------------------------------- */
    var $revealEls = $('.feature-card, .property-card, .testimonial-card, .stat');

    function revealOnScroll() {
        var scrollTop    = $(window).scrollTop();
        var windowHeight = $(window).height();

        $revealEls.each(function () {
            var elTop = $(this).offset().top;
            if (elTop < scrollTop + windowHeight - 60) {
                $(this).addClass('revealed');
            }
        });
    }

    // Add CSS for reveal transition via style injection (avoids extra file)
    $('<style>')
        .prop('type', 'text/css')
        .html([
            '.feature-card, .property-card, .testimonial-card, .stat {',
            '    opacity: 0;',
            '    transform: translateY(20px);',
            '    transition: opacity 0.5s ease, transform 0.5s ease;',
            '}',
            '.feature-card.revealed, .property-card.revealed, .testimonial-card.revealed, .stat.revealed {',
            '    opacity: 1;',
            '    transform: translateY(0);',
            '}'
        ].join('\n'))
        .appendTo('head');

    // Add staggered delay to sibling cards
    $('.feature-card, .property-card').each(function (i) {
        $(this).css('transition-delay', (i % 4) * 0.1 + 's');
    });

    $(window).on('scroll', revealOnScroll);
    revealOnScroll(); // Run once on load

    /* ----------------------------------------------------------
       5. RESIZE HANDLER
    ---------------------------------------------------------- */
    var resizeTimer;
    $(window).on('resize', function () {
        clearTimeout(resizeTimer);
        resizeTimer = setTimeout(function () {
            initSlider();
        }, 200);
    });

    /* ----------------------------------------------------------
       6. INIT
    ---------------------------------------------------------- */
    $(document).ready(function () {
        initSlider();
    });

})(jQuery);
