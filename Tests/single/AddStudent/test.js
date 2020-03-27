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

  let student = {
    name: 'testname',
    surname: 'testsurname',
    group: '8',
    login: 'teststudent',
    password: 'teststudent'
  }

  let course = '1'
  let numberGroup = '1'

  const timer = 1000
  try {
    await page.goto('http://www.teachersoffice.somee.com/');
    // await page.goto('https://localhost:44326/');

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

    //подробнее
    await page.click('body > div > main > div.container-fluid.mt-3 > div.d-flex.align-items-start.flex-column.bd-highlight.mb-3 > a')
    await page.waitFor(timer);
    await page.screenshot({
      path: './screens result/Подробнее.png'
    });

    //добавление студента
    await page.waitFor(timer);
    await page.click(`body > div > main > form > table > tbody > tr:nth-child(3) > td:nth-child(3) > form > a:nth-child(1)`)
   
    await page.waitFor(timer);
    await page.screenshot({
      path: './screens result/Таблица студентов.png'
    });
    await page.waitFor(timer);

    await page.click('body > div > main > div.container-fluid.mt-3 > div > button')
    await page.waitFor(timer);

    await page.focus('#NameStudent')
    page.keyboard.type(student.name)
    await page.waitFor(timer);
    await page.focus('#SurnameStudent')
    page.keyboard.type(student.surname)
    await page.waitFor(timer);
    await page.select('select[name="idgroup"]', student.group);
    await page.waitFor(timer);
    await page.focus('input[name="Student.Login"]')
    page.keyboard.type(student.login)
    await page.waitFor(timer);
    await page.focus('input[name="Student.Password"]')
    page.keyboard.type(student.password)
    await page.waitFor(timer);
    await page.click('#addStudent > div > div > div.modal-footer >  input.btn.btn-primary')
    await page.waitFor(timer);
    await page.screenshot({
      path: './screens result/Студенты.png'
    });
    await page.waitFor(timer);
    await browser.close();
  } catch (error) {
    console.log(error.message)
  }
}

getCheckScreen()