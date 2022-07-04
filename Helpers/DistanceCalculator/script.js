let x1 = document.getElementById("x1");
let y1 = document.getElementById("y1");
let x2 = document.getElementById("x2");
let y2 = document.getElementById("y2");
let calcBtn = document.getElementById("calcBtn");
let clearBtn = document.getElementById("clearBtn");
let display = document.getElementById("display");

calcBtn.addEventListener("click", (event) => {
    display.innerHTML = `Coords: (${Math.abs(x1.value-x2.value)};${Math.abs(y1.value-y2.value)}) H: ${calcDistance(x1.value, y1.value, x2.value, y2.value)}`;
});

clearBtn.addEventListener("click", (event) => {
    x1.value = "";
    y1.value = "";
    x2.value = "";
    y2.value = "";
});

const calcDistance = (x1, y1, x2, y2) => {

    distanceX = Math.abs(x1-x2);
    distanceY = Math.abs(y1-y2);

    if (distanceX > distanceY) {
        return 14 * distanceY + 10 * (distanceX - distanceY);
    }
    return 14 * distanceX + 10 * (distanceY - distanceX);
}