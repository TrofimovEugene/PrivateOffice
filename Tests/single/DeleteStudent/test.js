const puppeteer = require('puppeteer');

async function getCheckScreen() {
  const browser = await puppeteer.launch({headless: false});
  const page = await browser.newPage();

  let user = {
    login:'test123',
    password:'test123' 
  }

  let course = '1'

  let student = '1'

  const timer = 1000
  try {
    await page.goto('http://www.teachersoffice.somee.com/');
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
  
//подробнее
  await page.click(`body > div > main > div.container-fluid.mt-3 > div.row > div:nth-child(${course}) > div > div > form.d-flex.bd-highlight.mb-1 > input.btn.btn-outline-danger.ml-auto.p-1.bd-highlight`)
  await page.waitFor(timer);
  await page.screenshot({path: './screens result/Подробнее.png'});

  //добавление студента
  await page.waitFor(timer);
  await page.click('body > div > main > div.container.mt-5 > div > div.modal-body > div.d-flex.justify-content-between > a:nth-child(3)')
  await page.waitFor(timer);
  await page.screenshot({path: './screens result/Студенты.png'});
  await page.waitFor(timer);

  await page.click(`#tab1 > tbody > tr:nth-child(${student}) > td:nth-child(4) > form > input.btn.btn-outline-danger`)
  await page.waitFor(timer);
  
  await page.screenshot({path: './screens result/Студенты.png'});
  await page.goBack()
  await page.goBack()
  await page.waitFor(timer);
  await browser.close();
  } catch (error) { 
    console.log(error)
}
}

getCheckScreen()