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
      group:'6',
      login:'teststudent',
      password:'teststudent'
  }

  let numberCourse = '1'

  let numberStudent = '1'

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
await page.click(`body > div > main > div.container-fluid.mt-3 > div.row > div:nth-child(${numberCourse}) > div > div > form:nth-child(2) > div.d-flex.bd-highlight > a`)
  await page.waitFor(timer);
  await page.screenshot({path: './screens result/Подробнее.png'});

  //редактирование студента
  await page.waitFor(timer);
  await page.click('body > div > main > div.container.mt-5 > div > div.modal-body > div.d-flex.justify-content-between > a:nth-child(3)')
  await page.waitFor(timer);

  await page.click(`#tab1 > tbody > tr:nth-child(${numberStudent}) > td:nth-child(4) > form > a`)
  await page.waitFor(timer);

  await page.screenshot({path: './screens result/студент.png'});
  await page.waitFor(timer);

  await page.click('body > div > main > div.container-fluid.mt-3 > div > button')
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

    let searchLogin= await page.$('#InputLogin');
    await page.waitFor(timer);
    await searchLogin.click({clickCount: 10});
    await page.waitFor(timer);
    await searchLogin.press('Backspace'); 
    await page.waitFor(timer);
    page.keyboard.type(student.login)
    await page.waitFor(timer);

    let searchPassword= await page.$('#InputPassword');
    await page.waitFor(timer);
    await searchPassword.click({clickCount: 10});
    await page.waitFor(timer);
    await searchPassword.press('Backspace'); 
    await page.waitFor(timer);
    page.keyboard.type(student.password)
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