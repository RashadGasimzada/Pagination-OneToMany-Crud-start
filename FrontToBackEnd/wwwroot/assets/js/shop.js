$(function () {

    var slider = document.getElementById("myRange");
    var output = document.getElementById("demo");
    output.innerHTML = slider.value;
    slider.oninput = function () {
        output.innerHTML = this.value;
    }



    let alone = document.querySelector(".alone");
    let all = document.querySelector(".all");
    let two = document.querySelector(".two");
    let cards = document.querySelectorAll(".hard");
    alone.addEventListener("click", function () {
        
        for (const item of cards) {
            item.style.display="block";
        }
        
    });

    all.addEventListener("click",function(){
        for (const item of cards) {
            item.style.display="flex"
        }
    });

    //my navbar methotds
    let fruit = document.querySelector(".fruit");
    let fruits = document.querySelector(".fruits");
    let bever = document.querySelector(".bever");
    let beverag = document.querySelector(".beverag")

    fruit.addEventListener("mouseover", function () {
            fruits.classList.remove("d-none");
    })
    fruit.addEventListener("click",function(){
        fruits.classList.add("d-none")
    })
    bever.addEventListener("mouseover", function () {
            beverag.classList.remove("d-none");
    })
    bever.addEventListener("click",function(){
        beverag.classList.add("d-none");
    })
    let allCatan=document.querySelector(".allCatan");
    let drobovik=document.querySelector(".drobovik");

    allCatan.addEventListener("mouseover",function(){
        drobovik.classList.remove("d-none");
    })
     allCatan.addEventListener("click",function(){
         drobovik.classList.add("d-none");
     })
  

})