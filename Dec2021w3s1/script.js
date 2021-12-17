console.log("Connected to js")

function changeColorLime(element) {
        element.style.color = "lime"
}

function changeColorWhite(element) {
    element.style.color = "white"
}

function hideMid() {
    var mid = document.getElementById("mid")
    var bar = document.querySelector("#ps-bar")

    mid.style.display = "none"
    bar.style.justifyContent = "space-around"
    
}

