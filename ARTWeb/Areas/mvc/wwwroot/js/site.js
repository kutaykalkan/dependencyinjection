$(document).ready(function() {
  // Create bxSlider options object
  var bx_options = {
    controls: false,
    pagerSelector: '#pager',
    onSliderLoad: function(index) {
      // Append pager to current slide
      $('#pager').appendTo('.bxslide[aria-hidden="false"] .container');
      $('a.bx-pager-link').removeClass('active');
      $('a[data-slide-index="'+ index +'"').addClass('active');
    },
    onSlideAfter: function($elem, oldIndex, newIndex) {
      $('#pager').appendTo('.bxslide[aria-hidden="false"] .container');
      $('a.bx-pager-link').removeClass('active');
      $('a[data-slide-index="'+ newIndex +'"').addClass('active');
    },
    startSlide: 0,
    responsive: false
  };

  var bg_slider = $('.bxslider').bxSlider(bx_options); // Initialise bxSlider

  $(window).resize(function(event) {
    var current = bg_slider.getCurrentSlide();
    bx_options.startSlide = current;
    bg_slider.reloadSlider(bx_options); // On resize, reload slider to maintain dimensions
  });

  $('#login-form').submit(function(e) {
    e.preventDefault();

    // Initialize inputs object
    var inputs = {
      name: $('input[name="user_name"]').val(),
      password: $('input[name="password"]').val(),
      remember: $('input[name="remember"]').is(":checked") || undefined
    };

    // Reset form error display 
    $('.error-focus').removeClass('error-focus');
    $('#user_name_error, #password_error').hide();

    // Validate inputs and display errors if found
    if (!inputs.name) {
      $('input[name="user_name"]').addClass('error-focus');
      $('#user_name_error').show().text('* This field is required.');
      return false;
    } else {
      $('#user_name_error').hide();
    }

    if (!inputs.password) {
      $('input[name="password"]').addClass('error-focus');
      $('#password_error').show().text('* This field is required.');
      return false;
    } else {
      $('#password_error').hide();
    }

    // Use form data
    alert("Form submitted: " +
      inputs.name + " " +
      inputs.password + " " +
      inputs.remember);
    return false;
  });
});
