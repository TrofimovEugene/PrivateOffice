const puppeteer = require('puppeteer');

async function getCheckScreen() {
  const browser = await puppeteer.launch({headless: false});
  const page = await browser.newPage();
  
  try {

  await page.goto('https://localhost:44326/');
  await page.waitFor(1000);
  await page.setViewport({width: 1000, height: 700})
  await page.screenshot({path: 'Главный экран.png'});

  await page.click('#nav-teacher > form > div.d-flex.justify-content-between > input.btn.btn-primary')
  await page.screenshot({path: 'Проверка входа учителя.png'});

  await page.click('#nav-student-tab')
  await page.screenshot({path: 'Окно входа студента.png'});

  await page.click('#nav-teacher-tab')
  await page.waitFor(1000);
  await page.click('#nav-teacher > form > div.d-flex.justify-content-between > input.btn.btn-secondary')
  await page.waitFor(1000);
  await page.screenshot({path: 'Окно регистрации.png'});

  await page.focus('#Teacher_FirstName')
  page.keyboard.type('Test')
  await page.waitFor(1000);
  await page.focus('#Teacher_SecondName')
  page.keyboard.type('Test')
  await page.waitFor(1000);
  await page.focus('#Teacher_Patronymic')
  page.keyboard.type('Test')
  await page.waitFor(1000);
  await page.focus('#Teacher_Login')
  page.keyboard.type('test123')
  await page.waitFor(1000);
  await page.focus('#Teacher_Password')
  page.keyboard.type('test123')
  await page.screenshot({path: 'Заполненное окно регистрации.png'});
  await page.waitFor(1000);
  await page.click('#addTeacher > div > div > div.modal-footer.d-flex.justify-content-between > input.btn.btn-primary')
  await page.waitFor(1000);

  await page.focus('#inputTeacherLogin')
  page.keyboard.type('test123')
  await page.waitFor(1000);
  await page.focus('#inputTeacherPassword')
  page.keyboard.type('test123')
  await page.screenshot({path: 'Заполненное окно входа.png'});
  await page.waitFor(1000);
  await page.click('#nav-teacher > form > div.d-flex.justify-content-between > input.btn.btn-primary')
  await page.waitFor(1000);
  await page.screenshot({path: 'Личный кабинет учителя.png'});

  await browser.close();
  console.log('Тест успешно пройден')

  } catch (error) {
    console.log('Error:', error);
  }
  
}

getCheckScreen();