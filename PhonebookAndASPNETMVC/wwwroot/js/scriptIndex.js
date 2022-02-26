document.addEventListener('DOMContentLoaded', function() {
  let mainMenu = document.querySelector('.main__menu-list');
  function openMenu() {
    mainMenu.classList.add('is-opened');
  }
  setTimeout(openMenu, 300);

  let menuLink = document.querySelector('.phone-book-menu');
    menuLink.addEventListener('click', function(e) {
      e.preventDefault();
      mainMenu.classList.remove('is-opened');
      mainMenu.classList.add('is-closed');

      setTimeout(function() {
        window.location.href = "Home/PhonebookMenu";
      }, 1000);
    }, true);

  let iconDescr = document.querySelector('.icon-description');
  
  let phoneBook = document.querySelector('#phoneBook');
  phoneBook.addEventListener('mouseover', function() {
    iconDescr.textContent = "to read the phone book";
  }, false);
  phoneBook.addEventListener('mouseout', function() {
    iconDescr.textContent = null;
  }, false);

  let phone = document.querySelector('#phone');
  phone.addEventListener('mouseover', function() {
    iconDescr.textContent = "you need a nice phone";
  }, false);
  phone.addEventListener('mouseout', function() {
    iconDescr.textContent = null;
  }, false);

  let globus = document.querySelector('#globus');
  globus.addEventListener('mouseover', function() {
    iconDescr.textContent = "tell the world";
  }, false);
  globus.addEventListener('mouseout', function() {
    iconDescr.textContent = null;
  }, false);
});

