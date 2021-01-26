 var seconds = 5000;
        function RedirectAfterDelayFn() {
        $.ajax({
            type: 'GET',
            url: '@Url.Action("DisplayBoard", "Displays")',
            success: function (data) {

                var pathArray = [];
                var durationArray = [];

                $.each(data, function (i, v) {
                    pathArray.push([v.ModelResourcePath]);
                })

                $.each(data, function (i, w) {
                    durationArray.push([w.ModelDuration]);
                })
                var index = 1;
                var el = document.getElementById("rotator");



                setTimeout(function rotate() {

                    if (index === pathArray.length) {
                        index = 0;
                    }

                    el.src = pathArray[index];
                    index = index + 1;
                    setTimeout(rotate, seconds);
                    alert(el.src);

                }, seconds);

            }
        })

    }

	Styles
	    /*div {
        text-align: center;
        width: 100%;
        vertical-align: middle;
        height: 100%;
        display: table-cell;
    }

    .iframe {
        width: 200px;
    }

    div,
    body,
    html {
        height: 100%;
        width: 100%;
    }

    body {
        display: table;
    }*/