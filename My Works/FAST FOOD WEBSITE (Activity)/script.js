const header= document.querySelector("header");

window.addEventListener("scroll", function() {
    header.classList.toggle("sticky", window.scrollY > 0);
});

let menu = document.querySelector('#menu-icon');
let scrollT = document.querySelector('.scroll-top');

menu.onclick = () => {
    menu.classList.toggle('bx-x');
    menu.classList.toggle('open');
};

window.onscroll = () => {
    menu.classList.remove('bx-x');
    menu.classList.remove('open');
};

scrollT.addEventListener("click", () => {
    window.scrollTo({top: 0, behavior:"smooth"})
});
