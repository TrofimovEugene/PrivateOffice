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
  
//подробнее
  await page.click('body > div > main > div.container-fluid.mt-3 > div.row > div:nth-child(1) > div > div > form:nth-child(1) > a')
  await page.waitFor(timer);
  await page.screenshot({path: './screens result/Подробнее.png'});

//удаление занятия
await page.click('body > div > main > div.container.mt-5 > div > div.modal-body > div.d-flex.justify-content-between > a:nth-child(1)')
await page.waitFor(timer);

await page.click('body > div > main > div.mt-3.ml-1.mb-1 > form:nth-child(4) > table > tbody > tr > td:nth-child(8) > form > input.btn.btn-outline-danger')
await page.waitFor(timer);

await page.screenshot({path: './screens result/Удаление занятия.png'});
await page.waitFor(timer);

await browser.close();
  } catch (error) { 
    console.log(error)
}
}

getCheckScreen()