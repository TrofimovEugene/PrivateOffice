const puppeteer = require('puppeteer');

async function getCheckScreen() {
  const browser = await puppeteer.launch({headless: false});
  const page = await browser.newPage();

  let user = {
    login:'test123',
    password:'test123' 
  }

  let course = {
      universiry: 'ИрГУПС',
      group: '6',
      nameCourse: 'Тест',
      startCourse:'12 12 2019',
      endCourse: '12 04 2020',
      time: '140'
  }

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

  //создание курса
  await page.screenshot({path: './screens result/Курсы.png'});
await page.waitFor(timer);
  await page.click('body > div:nth-child(1) > main > div.container-fluid.mt-3 > div.row > div > button.btn-outline-primary')
  await page.waitFor(timer);
  await page.focus('#InputUniveristy')
  page.keyboard.type(course.universiry)
  await page.waitFor(timer);
  await page.focus('#InputNameCourse')
  page.keyboard.type(course.nameCourse)
  await page.waitFor(timer);
  await page.select('select[name="idgroup"]', course.group);
  await page.waitFor(timer);
  await page.focus('#InputStartCourse')
  page.keyboard.type(course.startCourse)
  await page.waitFor(timer);
  await page.focus('#InputEndCourse')
  page.keyboard.type(course.endCourse)
  await page.waitFor(timer);
  await page.focus('#InputTime')
  page.keyboard.type(course.time)
  await page.screenshot({path: './screens result/Форма добавления курса.png'});
  await page.click('#exampleModalCenter > div > form > div > div.modal-footer > input.btn.btn-primary')
  await page.waitFor(timer);
  await page.screenshot({path: './screens result/Страница курсов.png'});
  await page.waitFor(timer);

  await browser.close();
  console.log('Тест успешно пройден')

  } catch (error) { 
      console.log(error)
  }
  
}

getCheckScreen();