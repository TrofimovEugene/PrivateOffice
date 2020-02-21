const puppeteer = require('puppeteer');

async function getCheckScreen() {
  const browser = await puppeteer.launch({headless: false});
  const page = await browser.newPage();

  let lesson = {
      topic: 'Тест',
      type:'1',
      day:'Вторник',
      date:'12032020',
      cabinet:'D505',
      starttime:'13:50',
      endtime:'15:20',
      replay:'каждую неделю'
  }

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

//редактирование занятия
await page.click('body > div > main > div.container.mt-5 > div > div.modal-body > div.d-flex.justify-content-between > a:nth-child(1)')
await page.waitFor(timer);

await page.click('body > div > main > div.mt-3.ml-1.mb-1 > form:nth-child(4) > table > tbody > tr > td:nth-child(7) > a')
await page.waitFor(timer);

await page.click('body > div > main > button')
await page.waitFor(timer);

let searchName= await page.$('#InputUniveristy');
    await page.waitFor(timer);
    await searchName.click({clickCount: 10});
    await page.waitFor(timer);
    await searchName.press('Backspace'); 
    await page.waitFor(timer);
    page.keyboard.type(lesson.topic)
    await page.waitFor(timer);

    await page.focus('#InputDateClass')
page.keyboard.type(lesson.date)
await page.waitFor(timer);

let searchCab= await page.$('#InputCabinet');
await page.waitFor(timer);
await searchCab.click({clickCount: 10});
await page.waitFor(timer);
await searchCab.press('Backspace'); 
await page.waitFor(timer);
page.keyboard.type(lesson.cabinet)
await page.waitFor(timer);

await page.focus('input[name="Class.ReplayClasses"]')
page.keyboard.type(lesson.replay)
await page.waitFor(timer);


await page.screenshot({path: './screens result/Добавление занятия.png'});
await page.waitFor(timer);

await page.click('#editClass > div > div > div.modal-body > form > div.modal-footer.m-0.p-0.pt-2 > input.btn.btn-primary')
await page.waitFor(timer);

await browser.close();
  } catch (error) { 
    console.log(error)
}
}

getCheckScreen()