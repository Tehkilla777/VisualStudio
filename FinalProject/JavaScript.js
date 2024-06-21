function projectAnimation() {
    let x = null;
    const elem = document.getElementById("animate");
    let pos = 0;
    clearInterval(x);
    id = setInterval(frame, 20);
    function frame() {
        if (pos == 350) {
            clearInterval(x);
        } else {
            pos++;
            elem.style.top = pos + "px";
            elem.style.left = pos + "px";
        }
    }
}