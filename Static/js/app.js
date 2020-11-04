$(function(){
    let type = 0;
    let mode = 0;
    let lc = 0;
    
    function reloadSig() {
        if (new Date().getTime() >= lc + 500) {
            lc = new Date().getTime();

            let url = "score?";

            url += "&username=" + encodeURIComponent(($("input[name=username]").val() || "tryonelove"));
            url += "&limit=1";
            url += "&mode=" + mode;
            url += "&type=" + type;

            let fullUrl = window.location.origin + "/" + url;
            $("#previewarea img").remove();

            let newImage = $("<img />", {
                src: url
            });

            $("#previewarea").append(newImage);
        }
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
        switch (this.classList[3]) {
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