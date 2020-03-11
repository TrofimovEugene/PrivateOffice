const puppeteer = require('puppeteer');

async function getCheckScreen() {
  const browser = await puppeteer.launch({
    headless: false
  });
  const page = await browser.newPage();

  let student = {
    login: 'teststudent',
    password: 'teststudent'
  }

  let user = {
    login: 'test123',
    password: 'test123'
  }

  let course = '2'

  let numbClass = '1'
  const timer = 1000

  try {

    await page.goto('http://www.teachersoffice.somee.com/');
    await page.waitFor(timer);
    await page.setViewport({
      width: 1300,
      height: 700
    })
    await page.screenshot({
      path: 'Главный экран.png'
    });

    await page.click('#nav-student-tab')
    await page.waitFor(timer);
    await page.click('#nav-student > form > div.d-flex.justify-content-end > button')
    await page.waitFor(timer);

    await page.focus('#inputStudentLogin')
    page.keyboard.type(student.login)
    await page.waitFor(timer);
    await page.focus('#inputStudentPassword')
    page.keyboard.type(student.password)
    await page.waitFor(timer);

    await page.click('#nav-student > form > div.d-flex.justify-content-end > button')
    await page.waitFor(timer);

    await page.screenshot({
      path: 'Личный кабинет студента.png'
    });
    await page.waitFor(timer);
    await page.click('#VisitedStudent_ConfirmVisited')
    await page.waitFor(2000);
    await page.screenshot({
      path: 'Отметка студента.png'
    });
    await page.goBack()
    await page.goBack()
    await page.waitFor(2000);

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
    await page.click('#nav-teacher > form > div.d-flex.justify-content-between > input.btn.btn-primary')
    await page.waitFor(timer);

    //подробнее
    await page.click(`body > div > main > div.container-fluid.mt-3 > div.row > div:nth-child(${course}) > div > div > form:nth-child(2) > div.d-flex.bd-highlight > a`)
    await page.waitFor(timer);


    await page.click('body > div > main > div.container.mt-5 > div > div.modal-body > div.d-flex.justify-content-between > a:nth-child(1)')
    await page.waitFor(timer);

    await page.click(`body > div > main > form:nth-child(4) > table > tbody > tr:nth-child(${numbClass}) > td:nth-child(7) > div > a.bd-highlight`)
    await page.waitFor(timer);
    await page.screenshot({
      path: './screens result/Подробнее.png'
    });

    await page.click('#VisitedStudent_Visited')

    await page.screenshot({
      path: './screens result/Отметка посещаемости.png'
    });
    await page.waitFor(timer);

    await browser.close();
    console.log('Тест успешно пройден')

  } catch (error) {
    console.log(error)
  }

}

getCheckScreen();