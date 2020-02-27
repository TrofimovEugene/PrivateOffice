const puppeteer = require('puppeteer');

async function getCheckScreen() {
  const browser = await puppeteer.launch({headless: false});
  const page = await browser.newPage();

  let user = {
    firstName:'Test',
    secondName:'Test',
    patronymic:'Test',
    login:'test123',
    password:'test123' 
  }
  
  try {

  await page.goto('http://www.teachersoffice.somee.com/');
  await page.waitFor(1000);
  await page.setViewport({width: 1000, height: 700})
  await page.screenshot({path: 'Главный экран.png'});

  await page.click('#nav-teacher > form > div.d-flex.justify-content-between > input.btn.btn-primary')
  await page.screenshot({path: 'Проверка входа учителя.png'});

  await page.click('#nav-student-tab')
  await page.waitFor(1000);
  await page.click('#nav-student > form > div.d-flex.justify-content-end > button.btn.btn-primary')
  await page.screenshot({path: 'Проверка входа студента.png'});

  await page.click('#nav-teacher-tab')
  await page.waitFor(1000);
  await page.click('#nav-teacher > form > div.d-flex.justify-content-between > input.btn.btn-secondary')
  await page.waitFor(1000);
  await page.screenshot({path: 'Окно регистрации.png'});

  await page.focus('#Teacher_FirstName')
  page.keyboard.type(user.firstName)
  await page.waitFor(1000);
  await page.focus('#Teacher_SecondName')
  page.keyboard.type(user.secondName)
  await page.waitFor(1000);
  await page.focus('#Teacher_Patronymic')
  page.keyboard.type(user.patronymic)
  await page.waitFor(1000);
  await page.focus('#Teacher_Login')
  page.keyboard.type(user.login)
  await page.waitFor(1000);
  await page.focus('#Teacher_Password')
  page.keyboard.type(user.password)
  await page.screenshot({path: 'Заполненное окно регистрации.png'});
  await page.waitFor(1000);
  await page.click('#addTeacher > div > div > div.modal-footer.d-flex.justify-content-between > input.btn.btn-primary')
  await page.waitFor(1000);

  await page.focus('#inputTeacherLogin')
  page.keyboard.type(user.login)
  await page.waitFor(1000);
  await page.focus('#inputTeacherPassword')
  page.keyboard.type(user.password)
  await page.screenshot({path: 'Заполненное окно входа.png'});
  await page.waitFor(1000);
  await page.click('#nav-teacher > form > div.d-flex.justify-content-between > input.btn.btn-primary')

  await page.waitFor(1000);
  await page.screenshot({path: 'Личный кабинет учителя.png'});

  await browser.close();
  console.log('Тест успешно пройден')

  } catch (error) { 
      console.log(error)
  }
  
}

getCheckScreen();