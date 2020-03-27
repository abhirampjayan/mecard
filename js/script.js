 

jQuery( document ).ready(function( $ ) {
	"use strict"; // this function is executed in strict mode	
	 
	
	/* ------------------------------------------------------ */
	/*  2. SHRINK HEADER JS
	/* ------------------------------------------------------ */ 
	var shrinkHeader=1;
		$(window).scroll(function(){
		var scroll=getCurrentScroll();
			if(scroll>=shrinkHeader){
				$('.navigation').addClass('shrink');
			}
		else{
			$('.navigation').removeClass('shrink');}
	});
	function getCurrentScroll(){
		return window.pageYOffset;
	}
	
	var sections = $('section')
	  , nav = $('nav')
	  , nav_height = nav.outerHeight();

	$(window).on('scroll', function () {
		var cur_pos = $(this).scrollTop();
		sections.each(function() {
			var top = $(this).offset().top - nav_height,
				bottom = top + $(this).outerHeight();
			if (cur_pos >= top && cur_pos <= bottom) {
				nav.find('a').removeClass('active');
				sections.removeClass('active');
		  
				$(this).addClass('active');
				nav.find('a[href="#'+$(this).attr('id')+'"]').addClass('active');
			}
		});
	});
	/* --------------------------- */
	/*  3. MENU SMOOTH SCROLLING JS
	/* --------------------------- */ 
	$(function() {
         $('a[href*="#"]:not([data-toggle="tab"])').on('click', function() {
             if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
                 var target = $(this.hash);
                 target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
                 if (target.length) {
                     $('html, body').animate({
                         scrollTop: target.offset().top
                     }, 1000);
                     return false;
                 }
             }
         });
     });
	
	/* ------------------------------------------------------ */
	/*  4. OWL SCREENSHOT SLIDER JS
	/* ------------------------------------------------------ */ 
	$(document).ready(function() {
		$("#owl-screenshot").owlCarousel({
			autoPlay: 9000,
			items : 4,
			itemsDesktop : [1199,4],
			itemsDesktopSmall : [979,3],
			itemsTablet: [768,3],
			itemsMobile : [479,2],
			responsive: true,
		});
	});
	/* ------------------------------------------------------ */
	/*  5. OWL REVIEWS SLIDER JS
	/* ------------------------------------------------------ */
	$(document).ready(function() {
		$("#owl-reviews").owlCarousel({
			autoPlay: 5000,
			items : 1,
			itemsDesktop : [1199,1],
			itemsDesktopSmall : [979,1],
			itemsTablet: [768,1],
			itemsMobile : [479,1],
			responsive: true,
		});
	});
	
	 
	 
	 
	 

});	



