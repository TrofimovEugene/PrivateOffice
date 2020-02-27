const puppeteer = require('puppeteer');

async function getCheckScreen() {
  const browser = await puppeteer.launch({headless: false});
  const page = await browser.newPage();

  let user = {
    login:'test123',
    password:'test123' 
  }

  let course = '2'

  let numbClass = '1'

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
  await page.click(`body > div > main > div.container-fluid.mt-3 > div.row > div:nth-child(${course}) > div > div > form:nth-child(2) > div.d-flex.bd-highlight > a`)
  await page.waitFor(timer);
  await page.screenshot({path: './screens result/Подробнее.png'});

await page.click('body > div > main > div.container.mt-5 > div > div.modal-body > div.d-flex.justify-content-between > a:nth-child(1)')
await page.waitFor(timer);

await page.click(`body > div > main > form:nth-child(4) > table > tbody > tr:nth-child(${numbClass}) > td:nth-child(7) > div > a.bd-highlight`)
await page.waitFor(timer);

// await page.$$eval("input[type='checkbox']", checks => checks.forEach(c => c.checked = true));
await page.click('#VisitedStudent_Visited')

await page.screenshot({path: './screens result/Отметка посещаемости.png'});
await page.waitFor(timer);

await browser.close();
  } catch (error) { 
    console.log(error)
}
}

getCheckScreen()