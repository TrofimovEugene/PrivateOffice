const puppeteer = require('puppeteer');

async function getCheckScreen() {
  const browser = await puppeteer.launch({
    headless: false
  });
  const page = await browser.newPage();

  let user = {
    login: 'test123',
    password: 'test123'
  }

  let group = '3'

  const timer = 1000
  try {
    // await page.goto('http://www.teachersoffice.somee.com/');
    await page.goto('https://localhost:44326/');
    await page.waitFor(timer);
    await page.setViewport({
      width: 1000,
      height: 700
    })

    //вход  в кабинет
    await page.focus('#inputTeacherLogin')
    page.keyboard.type(user.login)
    await page.waitFor(timer);
    await page.focus('#inputTeacherPassword')
    page.keyboard.type(user.password)
    await page.screenshot({
      path: './screens result/Заполненное окно входа.png'
    });
    await page.waitFor(timer);
    await page.click('#nav-teacher > form > div.d-flex.justify-content-end > input')
    await page.waitFor(timer);

    //удаление группы
    await page.click('body > div > main > div.container-fluid.mt-3 > div.d-flex.align-items-start.flex-column.bd-highlight.mb-3 > a')
    await page.waitFor(timer);
    await page.screenshot({
      path: './screens result/Таблица студентов.png'
    });
    await page.click(`body > div > main > form > table > tbody > tr:nth-child(${group}) > td:nth-child(4) > form > input.btn.btn-outline-danger`)
    await page.waitFor(timer);
    await page.screenshot({
      path: './screens result/Удаление студента.png'
    });
    await page.waitFor(timer);
    await page.waitFor(timer);
    await browser.close();
  } catch (error) {
    console.log(error)
  }
}

getCheckScreen()