
(function(){
  "use strict";


$(function ($) {

    //BG SLIDESHOW WITH ZOOM EFFECT
    $.mbBgndGallery.buildGallery({
                containment:"body",
                timer:1000,
                effTimer:15000,
                controls:false, 
                grayScale:false,
                shuffle:true,
                preserveWidth:false,
                preserveTop: true,
                effect:"zoom",
    //effect:{enter:{transform:"scale("+(1+ Math.random()*2)+")",opacity:0},exit:{transform:"scale("+(Math.random()*2)+")",opacity:0}},

                // If your server allow directory listing you can use:
                // (however this doesn't work locally on your computer)


                // else:

                 images:[
                 "../Content/Assets/pizza_1.jpg",
                 "../Content/Assets/pizza_2.jpg",
                 "../Content/Assets/pizza_3.jpg"
                 ],    

                onStart:function(){},
                onPause:function(){},
                onPlay:function(opt){},
                onChange:function(opt,idx){},
                onNext:function(opt){},
                onPrev:function(opt){}
            });


   
});
// $(function ($)  : ends

})();
