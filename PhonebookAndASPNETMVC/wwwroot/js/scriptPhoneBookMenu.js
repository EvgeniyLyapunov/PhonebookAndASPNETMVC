document.addEventListener('DOMContentLoaded', function() {
  let mainMenu = document.querySelector('.main__menu-list');
  function openMenu() {
    mainMenu.classList.add('is-opened');
  }
  setTimeout(openMenu, 300);

  let iconDescr = document.querySelector('.icon-description');

  let openedBook = document.querySelector('#openedBook');
  openedBook.addEventListener('mouseover', function() {
    iconDescr.textContent = "see contacts";
  }, false);
  openedBook.addEventListener('mouseout', function() {
    iconDescr.textContent = null;
  }, false);

  let addNewContact = document.querySelector('#addNewContact');
  addNewContact.addEventListener('mouseover', function() {
    iconDescr.textContent = "add a new contact";
  }, false);
  addNewContact.addEventListener('mouseout', function() {
    iconDescr.textContent = null;
  }, false);

  let search = document.querySelector('#search');
  search.addEventListener('mouseover', function() {
    iconDescr.textContent = "search in the phone book";
  }, false);
  search.addEventListener('mouseout', function() {
    iconDescr.textContent = null;
  }, false);
});