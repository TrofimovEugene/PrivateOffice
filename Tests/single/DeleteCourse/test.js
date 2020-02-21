const puppeteer = require('puppeteer');

async function getCheckScreen() {
  const browser = await puppeteer.launch({headless: false});
  const page = await browser.newPage();

  let user = {
    login:'test123',
    password:'test123' 
  }

  const timer = 1000
  
  try {

  await page.goto('https://localhost:44326/');
  await page.waitFor(timer);
  await page.setViewport({width: 1000, height: 700})


  //вход  в кабинет
  await page.focus('#inputTeacherLogin')
  page.keyboard.type(user.login)
  await page.waitFor(timer);
  await page.focus('#inputTeacherPassword')
  page.keyboard.type(user.password)
  await page.screenshot({path: './screens result/Заполненное окно входа.png'});
  await page.waitFor(timer);
  await page.click('#nav-teacher > form > div.d-flex.justify-content-between > input.btn.btn-primary')
  await page.waitFor(timer);

  //удаление курса
  await page.click('body > div > main > div.container-fluid.mt-3 > div.row > div:nth-child(1) > div > div > form:nth-child(2) > input.btn.btn-outline-primary')
  await page.waitFor(timer);
  await page.screenshot({path: './screens result/Удаление курсов.png'});
  await page.waitFor(timer);

  await browser.close();
  console.log('Тест успешно пройден')

  } catch (error) { 
      console.log(error)
  }
  
}

getCheckScreen();