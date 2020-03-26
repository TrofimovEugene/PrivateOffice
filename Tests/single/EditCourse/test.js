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

  let numberCourse = '1'

  let course = {
    universiry: 'ИрГУПС',
    group: '2',
    nameCourse: 'Тест',
    startCourse: '12 12 2019',
    endCourse: '12 02 2020',
    time: '140'
  }

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

    //редактирование курса
    await page.click(`body > div > main > div.container-fluid.mt-3 > div.row > div:nth-child(${numberCourse}) > div > div > form:nth-child(2) > div.d-flex.bd-highlight > a`)
    await page.waitFor(timer);
    await page.screenshot({
      path: './screens result/Курс.png'
    });
    await page.waitFor(timer);
    await page.click('body > div > main > div.container.mt-5 > div > div.modal-body > div.d-flex.justify-content-between > button')
    await page.waitFor(timer);

    let searchUniver = await page.$('#InputUniveristy');
    await page.waitFor(timer);
    await searchUniver.click({
      clickCount: 10
    });
    await page.waitFor(timer);
    await searchUniver.press('Backspace');
    await page.waitFor(timer);
    page.keyboard.type(course.universiry)
    await page.waitFor(timer);

    let searchName = await page.$('#InputNameCourse');
    await page.waitFor(timer);
    await searchName.click({
      clickCount: 10
    });
    await page.waitFor(timer);
    await searchName.press('Backspace');
    await page.waitFor(timer);
    page.keyboard.type(course.nameCourse)
    await page.waitFor(timer);

    await page.select('select[name="idgroup"]', course.group);
    await page.waitFor(timer);

    let searchStart = await page.$('#InputStartCourse');
    await page.waitFor(timer);
    await searchStart.click({
      clickCount: 10
    });
    await page.waitFor(timer);
    await searchStart.press('Backspace');
    await page.waitFor(timer);
    page.keyboard.type(course.startCourse)
    await page.waitFor(timer);

    let searchEnd = await page.$('#InputEndCourse');
    await page.waitFor(timer);
    await searchEnd.click({
      clickCount: 10
    });
    await page.waitFor(timer);
    await searchEnd.press('Backspace');
    await page.waitFor(timer);
    page.keyboard.type(course.endCourse)
    await page.waitFor(timer);

    await page.select('select[name="idgroup"]', course.group);
    await page.waitFor(timer);

    let searchTime = await page.$('#InputTime');
    await page.waitFor(timer);
    await searchTime.click({
      clickCount: 10
    });
    await page.waitFor(timer);
    await searchTime.press('Backspace');
    await page.waitFor(timer);
    page.keyboard.type(course.time)
    await page.waitFor(timer);

    await page.screenshot({
      path: './screens result/Форма добавления курса.png'
    });
    await page.click('#editCourse > div > div > form > div.modal-footer > input.btn.btn-primary')
    await page.waitFor(timer);
    await page.screenshot({
      path: './screens result/Страница курсов.png'
    });
    await page.waitFor(timer);

    await browser.close();
    console.log('Тест успешно пройден')

  } catch (error) {
    console.log(error)
  }

}

getCheckScreen();