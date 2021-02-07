
/*********************** MAIL ****************************/

function Erase() {
    document.getElementById('c_fname').value = '';
    document.getElementById('c_lname').value = '';
    document.getElementById('c_email').value = '';
    document.getElementById('c_subject').value = '';
    document.getElementById('c_message').value = '';
}
window.onload = Erase;



/***************************** ANIMATION SECTION PICTURE ********************/

const listePicture = document.querySelectorAll('.section-picture-card');
const PictureRow = document.getElementById('section-picture');
const pictureContainer = document.getElementById('titre');

const tlpicture = new TimelineMax();

tlpicture
    .from(PictureRow, { y: -200, opacity: 0, duration: 0.7 })
    .staggerFrom(listePicture, 1, { opacity: 0 }, 0.2, '-=0.5')


const controller = new ScrollMagic.Controller();

const scene1 = new ScrollMagic.Scene({
    triggerElement: pictureContainer,
    /*triggerHook: 0.5,*/
    reverse: false
})
    .setTween(tlpicture)
    .addTo(controller)




/***************************** ANIMATION SECTION LAST BOOKS SECTION INDEX PAGE ********************/

const listeBooks = document.querySelectorAll('.section-book-card');
const BookRow = document.getElementById('section-last-books');
const titreContainer = document.getElementById('title-section-last-books');

const tlbooks = new TimelineMax();

tlbooks
    .from(BookRow, { y: -200, opacity: 0, duration: 0.7 })
    .staggerFrom(listeBooks, 1, { opacity: 0 }, 0.2, '-=0.5')




const scene2 = new ScrollMagic.Scene({
    triggerElement: titreContainer,
    triggerHook: 0.5,
    reverse: false
})
    .setTween(tlbooks)
    .addTo(controller)



//***************************************************








