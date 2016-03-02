[].slice.call(document.querySelectorAll('.menu')).forEach(function(menu) {
    var menuItems = menu.querySelectorAll('.menu__link'),
        setCurrent = function(ev) {
            ev.preventDefault();

            var item = ev.target.parentNode; // li

            // return if already current
            if (classie.has(item, 'menu__item--current')) {
                return false;
            }
            // remove current
            classie.remove(menu.querySelector('.menu__item--current'), 'menu__item--current');
            // set current
            classie.add(item, 'menu__item--current');
        };

    [].slice.call(menuItems).forEach(function(el) {
        el.addEventListener('click', setCurrent);
    });
});

[].slice.call(document.querySelectorAll('.link-copy')).forEach(function(link) {
    link.setAttribute('data-clipboard-text', location.protocol + '//' + location.host + location.pathname + '#' + link.parentNode.id);
    new Clipboard(link);
    link.addEventListener('click', function() {
        classie.add(link, 'link-copy--animate');
        setTimeout(function() {
            classie.remove(link, 'link-copy--animate');
        }, 300);
    });
});

$(document).ready(function () {
  /***************** Waypoints ******************/

  $('.wp1').waypoint(function () {
    $('.wp1').addClass('animated fadeInUp')
  }, {
    offset: '75%'
  })
  $('.wp2').waypoint(function () {
    $('.wp2').addClass('animated fadeInUp')
  }, {
    offset: '75%'
  })
  $('.wp3').waypoint(function () {
    $('.wp3').addClass('animated fadeInRight')
  }, {
    offset: '75%'
  })

  /***************** Initiate Flexslider ******************/
  $('.flexslider').flexslider({
    animation: 'slide'
  })

  /***************** Initiate Fancybox ******************/

  $('.single_image').fancybox({
    padding: 4,
  })

  /***************** Tooltips ******************/
  $('[data-toggle="tooltip"]').tooltip()

  /***************** Nav Transformicon ******************/

  /* When user clicks the Icon */
  $('.nav-toggle').click(function () {
    $(this).toggleClass('active')
    $('.header-nav').toggleClass('open')
    event.preventDefault()
  })
  /* When user clicks a link */
  $('.header-nav li a').click(function () {
    $('.nav-toggle').toggleClass('active')
    $('.header-nav').toggleClass('open')

  })

  /***************** Header BG Scroll ******************/

  $(function () {
    $(window).scroll(function () {
      var scroll = $(window).scrollTop()

      if (scroll >= 20) {
        $('section.navigation').addClass('fixed')
        $('header').css({
          'border-bottom': 'none',
          'padding': '10px 0'
        })
        $('header .member-actions').css({
          'top': '26px',
        })
        $('header .navicon').css({
          'top': '34px',
        })
      } else {
        $('section.navigation').removeClass('fixed')
        $('header').css({
          'border-bottom': 'solid 1px rgba(255, 255, 255, 0.2)',
          'padding': '50px 0'
        })
        $('header .member-actions').css({
          'top': '41px',
        })
        $('header .navicon').css({
          'top': '48px',
        })
      }
    })

    var $grid=$('.grid').isotope({
      itemSelector: '.grid-item',
      layoutMode: 'fitRows'
    })

    $('.filter-button-group').on('click', 'button', function () {
      var filterValue = $(this).attr('data-filter')
      $grid.isotope({ filter: filterValue })
    })

  })
  /***************** Smooth Scrolling ******************/

  $(function () {
    $('a[href*=#]:not([href=#])').click(function () {
      if (location.pathname.replace(/^\//, '') === this.pathname.replace(/^\//, '') && location.hostname === this.hostname) {
        var target = $(this.hash)
        target = target.length ? target : $('[name=' + this.hash.slice(1) + ']')
        if (target.length) {
          $('html,body').animate({
            scrollTop: target.offset().top
          }, 2000)
          return false
        }
      }
    })

  })

});

(function() {

	var bodyEl = document.body,
		docElem = window.document.documentElement,
		support = { transitions: Modernizr.csstransitions },
		// transition end event name
		transEndEventNames = { 'WebkitTransition': 'webkitTransitionEnd', 'MozTransition': 'transitionend', 'OTransition': 'oTransitionEnd', 'msTransition': 'MSTransitionEnd', 'transition': 'transitionend' },
		transEndEventName = transEndEventNames[ Modernizr.prefixed( 'transition' ) ],
		onEndTransition = function( el, callback ) {
			var onEndCallbackFn = function( ev ) {
				if( support.transitions ) {
					if( ev.target != this ) return;
					this.removeEventListener( transEndEventName, onEndCallbackFn );
				}
				if( callback && typeof callback === 'function' ) { callback.call(this); }
			};
			if( support.transitions ) {
				el.addEventListener( transEndEventName, onEndCallbackFn );
			}
			else {
				onEndCallbackFn();
			}
		},
		gridEl = document.getElementById('theGrid'),
		// gridItemsContainer = gridEl.querySelector('.grid'),
		contentItemsContainer = document.querySelector('section.content'),
		gridItems = document.querySelectorAll('.grid__item'),
		contentItems = contentItemsContainer.querySelectorAll('.content__item'),
		closeCtrl = contentItemsContainer.querySelector('.close-button'),
		current = -1,
		lockScroll = false, xscroll, yscroll,
		isAnimating = false,
		menuCtrl = document.getElementById('menu-toggle');

	/**
	 * gets the viewport width and height
	 * based on http://responsejs.com/labs/dimensions/
	 */
	function getViewport( axis ) {
		var client, inner;
		if( axis === 'x' ) {
			client = docElem['clientWidth'];
			inner = window['innerWidth'];
		}
		else if( axis === 'y' ) {
			client = docElem['clientHeight'];
			inner = window['innerHeight'];
		}
		
		return client < inner ? inner : client;
	}
	function scrollX() { return window.pageXOffset || docElem.scrollLeft; }
	function scrollY() { return window.pageYOffset || docElem.scrollTop; }

	function init() {
		initEvents();
	}

	function initEvents() {
		[].slice.call(gridItems).forEach(function(item, pos) {
			// grid item click event
			item.addEventListener('click', function(ev) {
				ev.preventDefault();
				if(isAnimating || current === pos) {
					return false;
				}
				isAnimating = true;
				// index of current item
				current = pos;
				// simulate loading time..
				classie.add(item, 'grid__item--loading');
				setTimeout(function() {
					classie.add(item, 'grid__item--animate');
					// reveal/load content after the last element animates out (todo: wait for the last transition to finish)
					setTimeout(function() { loadContent(item); }, 500);
				}, 1000);
			});
		});

		closeCtrl.addEventListener('click', function() {
			// hide content
			hideContent();
		});

		// keyboard esc - hide content
		document.addEventListener('keydown', function(ev) {
			if(!isAnimating && current !== -1) {
				var keyCode = ev.keyCode || ev.which;
				if( keyCode === 27 ) {
					ev.preventDefault();
					if ("activeElement" in document)
    					document.activeElement.blur();
					hideContent();
				}
			}
		} );
	}

	function loadContent(item) {
		// add expanding element/placeholder 
		var dummy = document.createElement('div');
		dummy.className = 'placeholder';

		// set the width/heigth and position
		dummy.style.WebkitTransform = 'translate3d(' + (item.offsetLeft - 5) + 'px, ' + (item.offsetTop - 5) + 'px, 0px) scale3d(' + item.offsetWidth/gridEl.offsetWidth + ',' + item.offsetHeight/getViewport('y') + ',1)';
		dummy.style.transform = 'translate3d(' + (item.offsetLeft - 5) + 'px, ' + (item.offsetTop - 5) + 'px, 0px) scale3d(' + item.offsetWidth/gridEl.offsetWidth + ',' + item.offsetHeight/getViewport('y') + ',1)';

		// add transition class 
		classie.add(dummy, 'placeholder--trans-in');

        var el2=document.getElementById("top");
		// insert it after all the grid items
		el2.appendChild(dummy);
		
		// body overlay
		classie.add(bodyEl, 'view-single');

		setTimeout(function() {
			// expands the placeholder
			dummy.style.WebkitTransform = 'translate3d(-5px, ' + (scrollY() - 5) + 'px, 0px)';
			dummy.style.transform = 'translate3d(-5px, ' + (scrollY() - 5) + 'px, 0px)';
			// disallow scroll
			window.addEventListener('scroll', noscroll);
		}, 25);

		onEndTransition(dummy, function() {
			// add transition class 
			classie.remove(dummy, 'placeholder--trans-in');
			classie.add(dummy, 'placeholder--trans-out');
			// position the content container
			contentItemsContainer.style.top = scrollY() + 'px';
			// show the main content container
			classie.add(contentItemsContainer, 'content--show');
			// show content item:
			classie.add(contentItems[current], 'content__item--show');
			// show close control
			classie.add(closeCtrl, 'close-button--show');
			// sets overflow hidden to the body and allows the switch to the content scroll
			classie.addClass(bodyEl, 'noscroll');

			isAnimating = false;
            $('.navigation').css("z-index",0);
		});
	}

	function hideContent() {
		var gridItem = gridItems[current], contentItem = contentItems[current];

		classie.remove(contentItem, 'content__item--show');
		classie.remove(contentItemsContainer, 'content--show');
		classie.remove(closeCtrl, 'close-button--show');
		classie.remove(bodyEl, 'view-single');

		setTimeout(function() {
            var el2=document.getElementById("top");
			var dummy = el2.querySelector('.placeholder');

			classie.removeClass(bodyEl, 'noscroll');

			dummy.style.WebkitTransform = 'translate3d(' + gridItem.offsetLeft + 'px, ' + gridItem.offsetTop + 'px, 0px) scale3d(' + gridItem.offsetWidth/gridEl.offsetWidth + ',' + gridItem.offsetHeight/getViewport('y') + ',1)';
			dummy.style.transform = 'translate3d(' + gridItem.offsetLeft + 'px, ' + gridItem.offsetTop + 'px, 0px) scale3d(' + gridItem.offsetWidth/gridEl.offsetWidth + ',' + gridItem.offsetHeight/getViewport('y') + ',1)';

			onEndTransition(dummy, function() {
				// reset content scroll..
				contentItem.parentNode.scrollTop = 0;
                 
				el2.removeChild(dummy);
				classie.remove(gridItem, 'grid__item--loading');
				classie.remove(gridItem, 'grid__item--animate');
				lockScroll = false;
				window.removeEventListener( 'scroll', noscroll );
                $('.navigation').css("z-index",999);
			});
			
			// reset current
			current = -1;
		}, 25);
	}

	function noscroll() {
		if(!lockScroll) {
			lockScroll = true;
			xscroll = scrollX();
			yscroll = scrollY();
		}
		window.scrollTo(xscroll, yscroll);
	}

	init();

})();