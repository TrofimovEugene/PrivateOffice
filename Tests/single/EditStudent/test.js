const puppeteer = require('puppeteer');

async function getCheckScreen() {
  const browser = await puppeteer.launch({headless: false});
  const page = await browser.newPage();

  let user = {
    login:'test123',
    password:'test123' 
  }

  let student ={
      name:'testname',
      surname:'testsurname',
      group:'2',
      login:'teststudent',
      password:'teststudent'
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

  //добавление студента
  await page.waitFor(timer);
  await page.click('body > div > main > div.container.mt-5 > div > div.modal-body > div.d-flex.justify-content-between > a:nth-child(3)')
  await page.waitFor(timer);

  await page.click('#tab1 > tbody > tr > td:nth-child(4) > form > a')
  await page.waitFor(timer);

  await page.click('body > div > main > div.container.mt-5 > div > form > div > div.d-flex.justify-content-between > button')
  await page.waitFor(timer);
  
  
  let searchFirstName= await page.$('#InputFirstName');
    await page.waitFor(timer);
    await searchFirstName.click({clickCount: 10});
    await page.waitFor(timer);
    await searchFirstName.press('Backspace'); 
    await page.waitFor(timer);
    page.keyboard.type(student.name)
    await page.waitFor(timer);

    let searchSecondName= await page.$('#InputSecondName');
    await page.waitFor(timer);
    await searchSecondName.click({clickCount: 10});
    await page.waitFor(timer);
    await searchSecondName.press('Backspace'); 
    await page.waitFor(timer);
    page.keyboard.type(student.surname)
    await page.waitFor(timer);

    await page.select('select[name="idgroup"]', student.group);
    await page.waitFor(timer);
  await page.waitFor(timer);
  await page.click('#editStudent > div > div > form > div.modal-footer > input')
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