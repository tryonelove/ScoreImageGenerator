$(function(){
    let type = 1;
    let mode = 0;
    reloadSig();
    
    function reloadSig() {
            console.log("Reloading sig.");
            
            let url = "http://"+window.location.hostname + ":3228/score?";
            
            url += "username=" + encodeURIComponent(($("input[name=username]").val() || "tryonelove"));
            url += "&limit=1";
            url += "&mode=" + mode;
            url += "&type=" + type;

            $("#previewarea img").remove();
            console.log(url);

            let newImage = $("<img />", {
                    "src": url,
                    "class": "col-12 col-md-8 col-lg-4 preview lazy"
            });
            
            $("#previewarea").append(newImage);
    }

    $("#generate-button").click(function(e) {
        e.preventDefault();
        e.stopPropagation();
        reloadSig();
    });

    $(".mode-block .mode").on("click", function(){
        switch (this.classList[2]) {
            case "std": mode = 0; break;
            case "taiko": mode = 1; break;
            case "ctb": mode = 2; break;
            case "mania": mode = 3; break;
        }
        console.log("Set mode to " + mode);
    });

    $(".type-block .type").on("click", function(){
        console.log(this.classList)
        switch (this.classList[4]) {
            case "recent": type = 0; break;
            case "best": type = 1; break;
        }
        console.log("Set type to " + type);

    });
    
    $('label.mode').click(function() {
        $('label').removeClass('selected');
        $(this).addClass('selected');
    });

    $('label.type').click(function() {
        $('label').removeClass('selected-button');
        $(this).addClass('selected-button');
    });
})