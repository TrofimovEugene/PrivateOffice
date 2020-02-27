const puppeteer = require('puppeteer');

async function getCheckScreen() {
  const browser = await puppeteer.launch({headless: false});
  const page = await browser.newPage();

  let student = {
    login: 'teststudent',
    password: 'teststudent'
  }
  
  try {

  await page.goto('http://www.teachersoffice.somee.com/');
  await page.waitFor(1000);
  await page.setViewport({width: 1300, height: 700})
  await page.screenshot({path: 'Главный экран.png'});

  await page.click('#nav-student-tab')
  await page.waitFor(1000);
  await page.click('#nav-student > form > div.d-flex.justify-content-end > button')
  await page.waitFor(1000);

  await page.focus('#inputStudentLogin')
  page.keyboard.type(student.login)
  await page.waitFor(1000);
  await page.focus('#inputStudentPassword')
  page.keyboard.type(student.password)
  await page.waitFor(1000);

  await page.click('#nav-student > form > div.d-flex.justify-content-end > button')
  await page.waitFor(1000);

  await page.screenshot({path: 'Личный кабинет студента.png'});
  await page.waitFor(1000);
  // await page.$$eval("input[type='checkbox']", checks => checks[1].checked='true');
  await page.click('#VisitedStudent_ConfirmVisited')
  await page.waitFor(2000);
  await page.screenshot({path: 'Отметка студента.png'});

  await browser.close();
  console.log('Тест успешно пройден')

  } catch (error) { 
      console.log(error)
  }
  
}

getCheckScreen();